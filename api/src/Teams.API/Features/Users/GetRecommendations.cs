using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.API.Services;
using Teams.Infrastructure;

namespace Teams.API.Features.Users;

public sealed record GetRecommendationsQuery(int PageSize, string? LastId, decimal? LastMatchPercent) : IRequest<GetRecommendationsResponse>;

public sealed record GetRecommendationsResponse(
    List<GetRecommendationsProjectViewModel> Items,
    string? LastId,
    decimal? LastMatchPercent);

public sealed record GetRecommendationsProjectViewModel(
    string Id,
    string Name,
    string Description,
    string Type,
    string Status,
    IEnumerable<GetRecommendationsProjectRoleViewModel> Roles,
    IEnumerable<GetRecommendationsTeamViewModel> Teams);

public sealed record GetRecommendationsProjectRoleViewModel(
    string Id,
    string RoleId,
    string RoleName,
    int PositionCount,
    IEnumerable<GetRecommendationsSkillViewModel> Skills);

public sealed record GetRecommendationsSkillViewModel(
    string Id,
    string Name);

public sealed record GetRecommendationsTeamViewModel(
    string Id,
    string Name,
    IEnumerable<GetRecommendationsTeamRoleViewModel> ProjectRoles);

public sealed record GetRecommendationsTeamRoleViewModel(
    string Id,
    string RoleName,
    int VacantPositionCount,
    IEnumerable<GetRecommendationsTeamMemberViewModel> Members);

public sealed record GetRecommendationsTeamMemberViewModel(
    string UserId,
    string Name);

public static class GetRecommendationsEndpoint
{
    public static void Map(RouteGroupBuilder builder) => builder
        .MapGet("me/recommendations", GetRecommendationsAsync)
        .RequireAuthorization()
        .WithSummary("In descending order of skill overlap. Provides optional keyset pagination. " +
        "Anything page other than the first requires `LastId` and `LastMatchPercent` as query strings. " +
        "These properties are returned in the response.")
        .WithDescription("Being keyset paginated, random access of pages is not supported. " +
        "Neither is backward navigation supported, as is typical. " +
        "The only support is for a feed that loads forward.");

    private static async Task<Ok<GetRecommendationsResponse>> GetRecommendationsAsync(
        int pageSize,
        string? lastRecommendationId,
        decimal? lastRecommendationMatchPercent,
        IMediator mediator)
    {
        var response = await mediator.Send(new GetRecommendationsQuery(pageSize, lastRecommendationId, lastRecommendationMatchPercent));
        return TypedResults.Ok(response);
    }
}

internal sealed class GetRecommendationsEndpointHandler(
    TeamDbContext context,
    IIdentityService identityService)
    : IRequestHandler<GetRecommendationsQuery, GetRecommendationsResponse>
{
    public async Task<GetRecommendationsResponse> Handle(
        GetRecommendationsQuery request,
        CancellationToken cancellationToken)
    {
        var userId = await context.Users
            .Where(u => u.IdentityGuid == identityService.GetUserIdentity())
            .Select(u => u.Id)
            .SingleAsync(cancellationToken);

        var query = context.Recommendations.Where(r => r.User.Id == userId);

        // Only apply the keyset condition when both values exist.
        if (request.LastId != null && request.LastMatchPercent != null)
        {
            query = query.Where(r =>
                r.MatchPercentage < request.LastMatchPercent ||
                (r.MatchPercentage == request.LastMatchPercent
                    && r.Id > Guid.Parse(request.LastId))
            ); // To prevent duplicates when LastIndex recommendation shares a percent with other recommendation(s).
        }

        query = query
            .OrderByDescending(r => r.MatchPercentage)
            .ThenBy(r => r.Id)
            .Take(request.PageSize);

        var items = await query.Select(r => new GetRecommendationsProjectViewModel(
            r.Project.Id.ToString(),
            r.Project.Name,
            r.Project.Description,
            r.Project.Type.ToString(),
            r.Project.Status.ToString(),
            r.Project.Roles
                .Select(r => new GetRecommendationsProjectRoleViewModel(
                    r.Id.ToString(),
                    r.RoleId.ToString(),
                    r.Role.Name,
                    r.PositionCount,
                    r.Skills
                        .Select(s => new GetRecommendationsSkillViewModel(
                            s.Id.ToString(),
                            s.Name)))),
            r.Project.Teams
                .Select(t => new GetRecommendationsTeamViewModel(
                    t.Id.ToString(),
                    t.Name,
                    r.Project.Roles.Select(r => new GetRecommendationsTeamRoleViewModel(
                        r.Id.ToString(),
                        r.Role.Name,
                        r.PositionCount - t.Members.Count(m => m.ProjectRoleId == r.Id),
                        t.Members
                            .Where(m => m.ProjectRoleId == r.Id)
                            .Join(
                                context.Users,
                                m => m.UserId,
                                u => u.Id,
                                (m, u) => new
                                {
                                    TeamMemberUserId = m.UserId,
                                    UserName = u.FirstName + " " + u.LastName
                                })
                            .Select(m => new GetRecommendationsTeamMemberViewModel(
                                m.TeamMemberUserId.ToString(),
                                m.UserName))))))))
            .ToListAsync(cancellationToken);

        var lastId = query.Last().Id;

        var lastMatchPercent = await context.Recommendations
            .Where(r => r.Id == lastId)
            .Select(r => r.MatchPercentage)
            .SingleOrDefaultAsync(cancellationToken);

        return new GetRecommendationsResponse(items, lastId.ToString(), lastMatchPercent);
    }
}