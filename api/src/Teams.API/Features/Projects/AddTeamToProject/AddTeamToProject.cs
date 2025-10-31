using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.API.Services;
using Teams.Infrastructure;

namespace Teams.API.Features.Projects.AddTeamToProject;

public sealed record AddTeamToProjectCommand(string ProjectId) : IRequest<string?>;

public static class AddTeamToProjectEndpoint
{
    public static void Map(RouteGroupBuilder group) => group
        .MapPost("{projectId}/teams", AddTeamToProjectAsync)
        .WithSummary("Add a team to a project.")
        .RequireAuthorization()
        .Produces(StatusCodes.Status403Forbidden);

    private static async Task<Results<Created, NotFound<string>, Conflict<string>>> AddTeamToProjectAsync(
        string projectId,
        IMediator mediator)
    {
        try
        {
            var teamId = await mediator.Send(new AddTeamToProjectCommand(projectId));
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
            .Include(project => project.Teams)
            .Include(p => p.Owner)
            .FirstOrDefaultAsync(p => p.Id == new Guid(request.ProjectId), cancellation);

        if (project is null)
        {
            return null;
        }
        
        var authorizationResult = await authorizationService.AuthorizeAsync(identityService.GetUser(), project, [new IsOwnerRequirement()]);
        if (!authorizationResult.Succeeded)
        {
            throw new UnauthorizedAccessException("User lacks permission to add a team to this project.");
        }
        
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.IdentityGuid == identityService.GetUserIdentity(),
                cancellation);
        
        var team = project.AddTeamToProject(user!.Id);
        await context.SaveChangesAsync(cancellation);
        return team?.Id.ToString();
    }
}