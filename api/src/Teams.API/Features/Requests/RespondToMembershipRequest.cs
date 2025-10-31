using MassTransit.Initializers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.API.Services;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Infrastructure;

namespace Teams.API.Features.Requests;

public sealed record RespondToMembershipRequestDto
{
    public required string NewStatus { get; init; }
}

public sealed record RespondToMembershipRequestCommand : IRequest<RespondToMembershipRequestResponse>
{
    public required string MembershipRequestId { get; init; }
    public required string NewStatus { get; init; }
    // public required string Message { get; init; }
}

public sealed record RespondToMembershipRequestResponse
{
    public required string TeamId { get; init; }
    public required string UserId { get; init; }
    // public required string UserName { get; init; }
    public required string ProjectRoleId { get; init; }
    public required string Status { get; init; }
}

public static class RespondToMembershipRequest
{
    public static void MapEndpoint(RouteGroupBuilder builder) => builder
        .MapPatch("{requestId}/status", RespondToMembershipRequestAsync)
        .WithSummary("Respond to a Pending membership request, to mark as Approved or Rejected. On approval, adds user to the team.")
        .RequireAuthorization()
        .Produces(StatusCodes.Status403Forbidden);
    
    private static async Task<Results<Ok<RespondToMembershipRequestResponse>, NotFound<string>>> RespondToMembershipRequestAsync(
        string requestId,
        RespondToMembershipRequestDto request,
        IMediator mediator)
    {
        var command = new RespondToMembershipRequestCommand
        {
            MembershipRequestId = requestId,
            NewStatus = request.NewStatus
        };

        try
        {
            var response = await mediator.Send(command);
            return TypedResults.Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }
}

internal sealed class RespondToMemberShipRequestHandler(
    IAuthorizationService authorizationService,
    TeamDbContext context,
    IIdentityService identityService)
    : IRequestHandler<RespondToMembershipRequestCommand, RespondToMembershipRequestResponse>
{
    public async Task<RespondToMembershipRequestResponse> Handle(
        RespondToMembershipRequestCommand request,
        CancellationToken cancellationToken)
    {
        var membershipRequest = await context.TeamMembershipRequests
            .Where(r => r.Id == new Guid(request.MembershipRequestId))
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new KeyNotFoundException($"Request with ID {request.MembershipRequestId} not found");
        
        var project = await context.Projects
            .Include(p => p.Roles)
            .Include(p => p.Teams)
                .ThenInclude(t => t.MembershipRequests)
            .Include(p => p.Teams)
                .ThenInclude(t => t.Members)
            .Include(p => p.Teams)
                .ThenInclude(t => t.Leader)
            .FirstOrDefaultAsync(p => p.Teams.Any(t => t.Id == membershipRequest.TeamId), cancellationToken);

        var team = project.Teams.FirstOrDefault(t => t.Id == membershipRequest.TeamId);
        
        var authorizationResult = await authorizationService.AuthorizeAsync(identityService.GetUser(), team, [new IsLeaderRequirement()]);
        if (!authorizationResult.Succeeded)
        {
            throw new UnauthorizedAccessException("User lacks permission to manage membership requests for this team.");
        }
        
        var userId = await context.Users
            .Where(u => u.IdentityGuid == identityService.GetUserIdentity())
            .Select(u => u.Id)
            .SingleOrDefaultAsync(cancellationToken);
        
        var updatedMembershipRequest = project!.RespondToMembershipRequest(
            userId,
            new Guid(request.MembershipRequestId),
            Enum.Parse<TeamMembershipRequestStatus>(request.NewStatus));
        
        await context.SaveChangesAsync(cancellationToken);

        return new RespondToMembershipRequestResponse
        {
            TeamId = updatedMembershipRequest.TeamId.ToString(),
            UserId = updatedMembershipRequest.UserId.ToString(),
            ProjectRoleId = updatedMembershipRequest.ProjectRoleId.ToString(),
            Status = updatedMembershipRequest.Status.ToString()
        };
    }
}