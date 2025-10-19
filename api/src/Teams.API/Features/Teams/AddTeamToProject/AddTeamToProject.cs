using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teams.API.Services;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Infrastructure;

namespace Teams.API.Features.Teams.AddTeamToProject;

public sealed record AddTeamToProjectCommand : IRequest<string?>
{
    public required string ProjectId { get; init; }
}

public static class AddTeamToProjectEndpoint
{
    public static void Map(RouteGroupBuilder group) => group
        .MapPost("", AddTeamToProjectAsync)
        .WithSummary("Add a team to a project.")
        .RequireAuthorization();

    private static async Task<Results<Created, ForbidHttpResult>> AddTeamToProjectAsync(
        [FromRoute] string projectId,
        IMediator mediator) 
    {
        var teamId = await mediator.Send(new AddTeamToProjectCommand { ProjectId = projectId });
        return (teamId is not null) 
            ? TypedResults.Created($"/api/projects/{projectId}/teams/{teamId}")
            : TypedResults.Forbid();
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
        var userIdentityGuid = identityService.GetUserIdentity();
        
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.IdentityGuid == userIdentityGuid, cancellationToken);

        var projects = await context.Projects
            .Where(p => p.OwnerId == user!.Id)
            .ToListAsync(cancellationToken);
        
        if (!projects.Any(p => p.OwnerId == user!.Id))
        {
            return null;
        }

        var team = new Team(Guid.CreateVersion7(), user!.Id);
        context.Teams.Add(team);
        await context.SaveChangesAsync(cancellationToken);

        return team.Id.ToString();
    }
}