using MassTransit.Initializers;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
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
        .WithSummary("")
        .RequireAuthorization();
    
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

internal sealed class RespondToMemberShipRequestHandler(TeamDbContext context)
    : IRequestHandler<RespondToMembershipRequestCommand, RespondToMembershipRequestResponse>
{
    public async Task<RespondToMembershipRequestResponse> Handle(
        RespondToMembershipRequestCommand request,
        CancellationToken cancellationToken)
    {
        var membershipRequest = await context.TeamMembershipRequests
            .FirstOrDefaultAsync(r => r.Id == new Guid(request.MembershipRequestId),
                cancellationToken)
            .Select(r => r)
            ?? throw new KeyNotFoundException($"Request with ID {request.MembershipRequestId} not found");

        var project = await context.Projects
            .Include(p => p.Teams)
                .ThenInclude(t => t.MembershipRequests)
            .FirstOrDefaultAsync(p => p.Teams.Any(t => t.Id == membershipRequest.TeamId), cancellationToken);

        project!.RespondToMembershipRequest(new Guid(request.MembershipRequestId),
            Enum.Parse<TeamMembershipRequestStatus>(request.NewStatus));
        
        await context.SaveChangesAsync(cancellationToken);

        return new RespondToMembershipRequestResponse
        {
            TeamId = membershipRequest.TeamId.ToString(),
            UserId = membershipRequest.UserId.ToString(),
            ProjectRoleId = membershipRequest.ProjectRoleId.ToString(),
            Status = membershipRequest.Status.ToString()
        };
    }
}