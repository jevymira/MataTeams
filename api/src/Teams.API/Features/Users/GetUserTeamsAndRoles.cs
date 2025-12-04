using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.API.Services;
using Teams.Infrastructure;

namespace Teams.API.Features.Users;

public sealed record GetUserTeamsAndRolesQuery : IRequest<GetUserTeamsAndRolesResponse>;

public sealed record GetUserTeamsAndRolesResponse(IEnumerable<GetUserTeamsAndRolesViewModel> Items);

public sealed record GetUserTeamsAndRolesViewModel(
    string ProjectId,
    string ProjectName,
    string TeamId,
    string TeamName,
    string ProjectRoleId,
    string RoleName
);

public static class GetUserTeamsAndRolesEndpoint
{
    public static void Map(RouteGroupBuilder builder) => builder
        .MapGet("me/roles", GetUserTeamsAndRolesEndpointAsync)
        .RequireAuthorization()
        .WithSummary("Get the authenticated user's project roles " +
                     "across all teams of which they are a member.");

    private static async Task<Ok<GetUserTeamsAndRolesResponse>> GetUserTeamsAndRolesEndpointAsync(IMediator mediator)
    {
        var response = await mediator.Send(new GetUserTeamsAndRolesQuery());
        return TypedResults.Ok(response);
    }
}

internal sealed class GetUserTeamsAndRolesQueryHandler(
        TeamDbContext dbContext,
        IIdentityService identityService)
        : IRequestHandler<GetUserTeamsAndRolesQuery, GetUserTeamsAndRolesResponse>
{
    public async Task<GetUserTeamsAndRolesResponse> Handle(
        GetUserTeamsAndRolesQuery request,
        CancellationToken cancellationToken)
    {
        var userId = await dbContext.Users
            .Where(u => u.IdentityGuid == identityService.GetUserIdentity())
            .Select(u => u.Id)
            .SingleOrDefaultAsync(cancellationToken);

        var models = await dbContext.TeamMembers
            .Where(m => m.UserId == userId)
            .Join(dbContext.Teams,
                tm => tm.TeamId,
                t => t.Id,
                (tm, t) => new
                {
                    TeamMember = tm,
                    Team = t
                })
            .Join(dbContext.ProjectRoles,
                intermediate => intermediate.TeamMember.ProjectRoleId,
                pr => pr.Id,
                (intermediate, pr) => new
                {
                    TeamMember = intermediate.TeamMember,
                    Team = intermediate.Team,
                    ProjectRole = pr
                })
            .Join(dbContext.Projects,
                intermediate => intermediate.ProjectRole.ProjectId,
                p => p.Id,
                (intermediate, p) => new GetUserTeamsAndRolesViewModel(
                    p.Id.ToString(),
                    p.Name,
                    intermediate.Team.Id.ToString(),
                    intermediate.Team.Name,
                    intermediate.ProjectRole.Id.ToString(),
                    intermediate.ProjectRole.Role.Name
                ))
            .ToListAsync(cancellationToken);

        return new GetUserTeamsAndRolesResponse(models);
    }
}

