using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.API.Services;
using Teams.Infrastructure;

namespace Teams.API.Features.Projects.RequestToJoinTeam;

public sealed record JoinTeamRequest
{
    public required string ProjectRoleId { get; init; }
    // public required string Message { get; init }
}

public sealed record RequestToJoinTeamCommand : IRequest<RequestToJoinTeamRequest>
{
    public required string TeamId { get; init; }
    public required string ProjectRoleId { get; init; }
}

public sealed record RequestToJoinTeamRequest
{
    public required string Id { get; init; }
    public required string TeamId { get; init; }
    public required string UserId { get; init; }
    public required string ProjectRoleId { get; init; }
    public required string Status { get; init; }
}

public static class RequestToJoinTeam
{
    public static void MapEndpoint(RouteGroupBuilder builder) => builder
        .MapPost("{teamId}/requests", RequestToJoinTeamAsync)
        .WithSummary("Create a request by the user to join a specified team, for a project role.")
        .RequireAuthorization();
    
    private static async Task<Results<Created<RequestToJoinTeamRequest>, NotFound<string>>> RequestToJoinTeamAsync(
        string teamId,
        JoinTeamRequest request,
        IMediator mediator)
    {
        var command = new RequestToJoinTeamCommand
        {
            TeamId = teamId,
            ProjectRoleId = request.ProjectRoleId,
        };
            
        try
        {
            var response = await mediator.Send(command);
            return TypedResults.Created($"team-membership-requests/{response.Id}", response);
        }
        catch (KeyNotFoundException ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }
}

internal sealed class RequestToJoinTeamCommandHandler(
    TeamDbContext context,
    IIdentityService identityService)
    : IRequestHandler<RequestToJoinTeamCommand, RequestToJoinTeamRequest>
{
    public async Task<RequestToJoinTeamRequest> Handle(
        RequestToJoinTeamCommand command, CancellationToken cancellationToken)
    {
        var team = await context.Teams
            .AsNoTracking()
            .Where(t => t.Id == new Guid(command.TeamId))
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new KeyNotFoundException($"Team with ID {command.TeamId} not found.");
        
        // Load in project aggregate root.
        var project = await context.Projects
            .Include(p => p.Teams)
            .FirstOrDefaultAsync(p => p.Id == team.ProjectId,
                cancellationToken)
            ?? throw new KeyNotFoundException($"Project with ID {team.ProjectId} not found.");
        
        var projectRoleExists = await context.ProjectRoles
            .AnyAsync(r => r.Id == new Guid(command.ProjectRoleId), cancellationToken);
        
        if (!projectRoleExists) throw new KeyNotFoundException($"Project role with ID {command.ProjectRoleId} not found.");

        // EXTRACTABLE
        var userId = await context.Users
            .Where(u => u.IdentityGuid == identityService.GetUserIdentity())
            .Select(u => u.Id)
            .SingleOrDefaultAsync(cancellationToken);
        
        // Modify through the aggregate root.
        var membershipRequest = project.AddTeamMembershipRequest(
            new Guid(command.TeamId),
            userId,
            new Guid(command.ProjectRoleId));

        await context.SaveChangesAsync(cancellationToken);
        
        return new RequestToJoinTeamRequest
        {
            Id = membershipRequest.Id.ToString(),
            TeamId = membershipRequest.TeamId.ToString(),
            UserId = membershipRequest.UserId.ToString(),
            ProjectRoleId = membershipRequest.ProjectRoleId.ToString(),
            Status = membershipRequest.Status.ToString()
        };
    }
}