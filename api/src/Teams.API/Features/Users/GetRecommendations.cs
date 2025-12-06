using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.API.Services;
using Teams.Infrastructure;

namespace Teams.API.Features.Users;

public sealed record GetRecommendationsQuery : IRequest<GetRecommendationsResponse>;

public sealed record GetRecommendationsResponse(
    List<GetRecommendationsProjectViewModel> Items);

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
    string Name
);

public static class GetRecommendationsEndpoint
{
    public static void Map(RouteGroupBuilder builder) => builder
        .MapGet("me/recommendations", GetRecommendationsAsync)
        .RequireAuthorization();

    private static async Task<Ok<GetRecommendationsResponse>> GetRecommendationsAsync(
        IMediator mediator)
    {
        var response = await mediator.Send(new GetRecommendationsQuery());
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
        
        var recommendations = await context.Recommendations
            .Where(r => r.User.Id == userId)
            .OrderByDescending(r => r.MatchPercentage)
            .Select(r => new GetRecommendationsProjectViewModel(
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
                                s.Name )))),
                r.Project.Teams
                    .Select(t => new GetRecommendationsTeamViewModel(
                        t.Id.ToString(),
                        t.Name))))
            .ToListAsync(cancellationToken);

        return new GetRecommendationsResponse(recommendations);
    }
}