using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Infrastructure;

namespace Teams.API.Features.Projects.GetAllTeamMembershipRequests;

public sealed record GetAllTeamMembershipRequestsQuery : IRequest<IEnumerable<TeamMembershipRequestViewModel>>
{
    public required string TeamId { get; init; }
    public string? Status { get; init; }
}

public sealed record TeamMembershipRequestViewModel
{
    public required string TeamId { get; init; }
    public required string UserId { get; init; }
    // public required string UserName { get; init; }
    public required string ProjectRoleId { get; init; }
    public required string Status { get; init; }
}

public static class GetAllTeamMembershipRequests
{
    public static void MapEndpoint(RouteGroupBuilder builder) => builder
        .MapGet("{teamId}/requests", GetAllTeamMembershipRequestsAsync);

    private static async Task<Results<Ok<IEnumerable<TeamMembershipRequestViewModel>>, NotFound<string>>> GetAllTeamMembershipRequestsAsync(
        string teamId,
        string? requestStatus,
        IMediator mediator)
    {
        try
        {
            var response = await mediator.Send(new GetAllTeamMembershipRequestsQuery
            {
                TeamId = teamId,
                Status = requestStatus
            });
            return TypedResults.Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }
}

internal sealed class GetAllTeamMembershipRequestsHandler(TeamDbContext context)
    : IRequestHandler<GetAllTeamMembershipRequestsQuery, IEnumerable<TeamMembershipRequestViewModel>>
{
    public async Task<IEnumerable<TeamMembershipRequestViewModel>> Handle(
        GetAllTeamMembershipRequestsQuery request,
        CancellationToken cancellationToken)
    {
        return await context.TeamMembershipRequests
            .Where(r => (request.Status == null) 
                || r.Status == Enum.Parse<TeamMembershipRequestStatus>(request.Status))
            .Select(r => new TeamMembershipRequestViewModel
            {
                TeamId = r.TeamId.ToString(),
                UserId = r.UserId.ToString(),
                ProjectRoleId = r.ProjectRoleId.ToString(),
                Status = r.Status.ToString()
            })
            .ToListAsync(cancellationToken);
    }
}