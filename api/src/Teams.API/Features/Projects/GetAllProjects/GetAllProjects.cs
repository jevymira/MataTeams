using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.Infrastructure;

namespace Teams.API.Features.Projects.GetAllProjects;

public sealed record GetAllProjectsQuery : IRequest<GetAllProjectsResponse>;
    
public sealed record GetAllProjectsResponse(
    List<ProjectViewModel> Projects
    // PageIndex
    // TotalPages
    // HasPreviousPage
    // HasNextPage
);

public sealed record ProjectViewModel(
    string Id,
    string Name,
    string Description,
    string Type,
    string Status,
    IEnumerable<ProjectRoleViewModel> Roles,
    IEnumerable<GetAllProjectsTeamViewModel> Teams
);

public sealed record ProjectRoleViewModel(
    string Id,
    string RoleId,
    string RoleName,
    int PositionCount,
    IEnumerable<ProjectRoleSkillViewModel> Skills
);

public sealed record ProjectRoleSkillViewModel(
    string SkillId,
    string SkillName
);

public sealed record GetAllProjectsTeamViewModel(
    string Id,
    string Name
);
    
public static class GetAllProjectsEndpoint
{
    public static void Map(RouteGroupBuilder builder) => builder
        .MapGet("", GetAllProjectsAsync);

    private static async Task<Ok<GetAllProjectsResponse>> GetAllProjectsAsync(IMediator mediator)
    {
        var response = await mediator.Send(new GetAllProjectsQuery());
        return TypedResults.Ok(response);
    }
}

internal sealed class GetAllProjectsQueryHandler(
    TeamDbContext context)
    : IRequestHandler<GetAllProjectsQuery, GetAllProjectsResponse>
{
    public async Task<GetAllProjectsResponse> Handle(
        GetAllProjectsQuery request,
        CancellationToken cancellation)
    {
        var projects = await context.Projects
            .Select(p => new ProjectViewModel(
                p.Id.ToString(),
                p.Name,
                p.Description,
                p.Type.ToString(),
                p.Status.ToString(),
                p.Roles.Select(r => new ProjectRoleViewModel(
                    r.Id.ToString(),
                    r.RoleId.ToString(),
                    r.Role.Name,
                    r.PositionCount,
                    r.Skills.Select(s => new ProjectRoleSkillViewModel(
                        s.Id.ToString(),
                        s.Name
                    )))),
                p.Teams.Select(t => new GetAllProjectsTeamViewModel(
                    t.Id.ToString(),
                    t.Name
                ))
            ))
            .ToListAsync<ProjectViewModel>(cancellation);

        return new GetAllProjectsResponse(projects);
    }
}