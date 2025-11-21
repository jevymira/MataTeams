using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.API.Services;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Infrastructure;

namespace Teams.API.Features.Projects.AddTeamToProject;

public sealed record AddTeamToProjectRequest(string TeamName, string? ProjectRoleId);

public sealed record AddTeamToProjectCommand(string TeamName, string? ProjectRoleId, string ProjectId) : IRequest<string?>;

public static class AddTeamToProjectEndpoint
{
    public static void Map(RouteGroupBuilder group) => group
        .MapPost("{projectId}/teams", AddTeamToProjectAsync)
        .WithSummary("Add a team to a project. Optionally, include the project role the creator is to assume.")
        .RequireAuthorization()
        .Produces(StatusCodes.Status403Forbidden);

    private static async Task<Results<Created, NotFound<string>, Conflict<string>>> AddTeamToProjectAsync(
        string projectId,
        AddTeamToProjectRequest request,
        IMediator mediator)
    {
        try
        {
            var teamId = await mediator.Send(new AddTeamToProjectCommand(request.TeamName, request.ProjectRoleId, projectId));
            return (teamId is not null)
                ? TypedResults.Created($"/api/projects/{projectId}/teams/{teamId}")
                : TypedResults.NotFound($"Project not found with ID {projectId}.");
        }
        catch (InvalidOperationException ex)
        {
            return TypedResults.Conflict(ex.Message);
        }
    }
}

internal sealed class AddTeamToProjectCommandHandler(
    IAuthorizationService authorizationService,
    TeamDbContext context,
    IIdentityService identityService)
    : IRequestHandler<AddTeamToProjectCommand, string?>
{
    public async Task<string?> Handle(
        AddTeamToProjectCommand request,
        CancellationToken cancellation)
    {
        var project = await context.Projects
            .Include(p => p.Roles)
            .Include(project => project.Teams)
                .ThenInclude(t => t.MembershipRequests)
            .Include(p => p.Teams)
                .ThenInclude(t => t.Members)
            .Include(p => p.Owner)
            .FirstOrDefaultAsync(p => p.Id == new Guid(request.ProjectId), cancellation);

        if (project is null) return null;
        
        var authorizationResult = await authorizationService.AuthorizeAsync(identityService.GetUser(), project, [new IsOwnerRequirement()]);
        if (!authorizationResult.Succeeded)
        {
            throw new UnauthorizedAccessException("User lacks permission to add a team to this project.");
        }
        
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.IdentityGuid == identityService.GetUserIdentity(),
                cancellation);
        
        var team = project.AddTeamToProject(request.TeamName, user!.Id);
        if (!string.IsNullOrEmpty(request.ProjectRoleId))
        {
            var req = project.AddTeamMembershipRequest(team!.Id, user.Id, new Guid(request.ProjectRoleId));
            await context.SaveChangesAsync(cancellation);
            project.RespondToMembershipRequest(user.Id, req.Id, TeamMembershipRequestStatus.Approved);
        }
        await context.SaveChangesAsync(cancellation);
        return team?.Id.ToString();
    }
}