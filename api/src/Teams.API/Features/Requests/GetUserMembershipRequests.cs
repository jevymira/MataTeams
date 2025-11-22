using MassTransit.Initializers;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.API.Services;
using Teams.Infrastructure;

namespace Teams.API.Features.Requests;

public sealed record GetUserMembershipRequestsQuery() : IRequest<GetUserMembershipRequestsResponse>;

public sealed record GetUserMembershipRequestsResponse(
    IReadOnlyList<GetUserMembershipsRequestViewModel> Items
);

public sealed record GetUserMembershipsRequestViewModel(
    string Id,
    string Status,
    string ProjectRoleId,
    string ProjectRoleName,
    string TeamId,
    string TeamName,
    string ProjectId,
    string ProjectName
);
    
public static class GetUserMembershipRequestsEndpoint
{
    public static void Map(RouteGroupBuilder builder) => builder
        .MapGet("me/requests", GetUserMembershipRequestsAsync)
        .WithSummary("Get all membership requests for the authenticated user.")
        .RequireAuthorization();

    private static async Task<Ok<GetUserMembershipRequestsResponse>> GetUserMembershipRequestsAsync(
        IMediator mediator)
    {
        var response = await mediator.Send(new GetUserMembershipRequestsQuery());
        return TypedResults.Ok(response);
    }
}

internal sealed class GetUserMembershipRequestsQueryHandler(
    TeamDbContext context,
    IIdentityService identityService)
    : IRequestHandler<GetUserMembershipRequestsQuery, GetUserMembershipRequestsResponse>
{
    public async Task<GetUserMembershipRequestsResponse> Handle(
        GetUserMembershipRequestsQuery query, CancellationToken cancellation)
    {
        var userId = await context.Users
            .SingleAsync(u => u.IdentityGuid == identityService.GetUserIdentity(),
                cancellation)
            .Select(u => u.Id);

        var result = await context.TeamMembershipRequests
            .Where(r => r.UserId == userId)
            .Join(context.ProjectRoles,
                mr => mr.ProjectRoleId,
                r => r.Id,
                (mr, r) => new
                {
                    MembershipRequest = mr,
                    ProjectRole = r
                })
            .Join(context.Teams,
                intermediate => intermediate.MembershipRequest.TeamId,
                t => t.Id,
                (intermediate, t) => new
                {
                    MembershipRequest = intermediate.MembershipRequest,
                    ProjectRole = intermediate.ProjectRole,
                    Team = t
                })
            .Join(context.Projects,
                intermediate => intermediate.Team.ProjectId,
                p => p.Id,
                (intermediate, p) => new GetUserMembershipsRequestViewModel(
                    intermediate.MembershipRequest.Id.ToString(),
                    intermediate.MembershipRequest.Status.ToString(),
                    intermediate.ProjectRole.Id.ToString(),
                    intermediate.ProjectRole.Role.Name,
                    intermediate.Team.Id.ToString(),
                    intermediate.Team.Name,
                    p.Id.ToString(),
                    p.Name
                ))
            .ToListAsync(cancellation);
        
        
        return new GetUserMembershipRequestsResponse(result);
    }
}
