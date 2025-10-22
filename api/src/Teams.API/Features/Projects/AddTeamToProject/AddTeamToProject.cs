using MediatR;
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
        .RequireAuthorization();

    private static async Task<Results<Created, NotFound, ForbidHttpResult>> AddTeamToProjectAsync(
        string projectId,
        IMediator mediator)
    {
        try
        {
            var teamId = await mediator.Send(new AddTeamToProjectCommand(projectId));
            return (teamId is not null)
                ? TypedResults.Created($"/api/projects/{projectId}/teams/{teamId}")
                : TypedResults.Forbid();
        }
        catch (KeyNotFoundException)
        {
            return TypedResults.NotFound();
        }
    }
}

internal sealed class AddTeamToProjectCommandHandler(
    TeamDbContext context,
    IIdentityService identityService)
    : IRequestHandler<AddTeamToProjectCommand, string?>
{
    public async Task<string?> Handle(
        AddTeamToProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await context.Projects
            .Include(project => project.Teams)
            .FirstOrDefaultAsync(p => p.Id == new Guid(request.ProjectId), cancellationToken)
            ?? throw new KeyNotFoundException();
        
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.IdentityGuid == identityService.GetUserIdentity(),
                cancellationToken);
        
        var team = project.AddTeamToProject(user!.Id);
        await context.SaveChangesAsync(cancellationToken);
        return team?.Id.ToString();
    }
}