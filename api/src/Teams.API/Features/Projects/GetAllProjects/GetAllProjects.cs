using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Infrastructure;

namespace Teams.API.Features.Projects.GetAllProjects;

public sealed record GetAllProjectsQuery(string? SearchQuery) : IRequest<GetAllProjectsResponse>;
    
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
        .MapGet("", GetAllProjectsAsync)
        .WithSummary("Get all projects. Optionally, query for a single skill (at this point) by name.");

    private static async Task<Ok<GetAllProjectsResponse>> GetAllProjectsAsync(string? query, IMediator mediator)
    {
        var response = await mediator.Send(new GetAllProjectsQuery(query));
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
        IQueryable<Project> projects = context.Projects;

        if (!string.IsNullOrEmpty(request.SearchQuery))
        {
            projects = projects
                // .Include(p => p.Roles)
                //     .ThenInclude(r => r.Skills)
                .Where(p =>
                    p.Roles.Any(r =>
                        r.Skills.Any(s =>
                            s.Name.Equals(request.SearchQuery))));
        }
        
        var response = await projects
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
            )).ToListAsync(cancellation);

        return new GetAllProjectsResponse(response);
    }
}