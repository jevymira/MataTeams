using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Infrastructure;

namespace Teams.API.Features.Projects.GetAllProjects;

public sealed record GetAllProjectsQuery() : IRequest<GetAllProjectsResponse>;
    
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
    string Name,
    IEnumerable<GetAllProjectsTeamRoleViewModel> ProjectRoles);

public sealed record GetAllProjectsTeamRoleViewModel(
    string Id,
    string RoleName,
    int VacantPositionCount,
    IEnumerable<GetAllProjectsTeamMemberViewModel> Members);

public sealed record GetAllProjectsTeamMemberViewModel(
    string UserId,
    string Name);
    
public static class GetAllProjectsEndpoint
{
    public static void Map(RouteGroupBuilder builder) => builder
        .MapGet("", GetAllProjectsAsync)
        .WithSummary("Get all projects, with vacant roles and members per team.");

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
        IQueryable<Project> projects = context.Projects;

        /*
        if (!string.IsNullOrEmpty(request.SearchQuery))
        {
            projects = projects
                .Where(p =>
                    p.Roles.Any(r =>
                        r.Skills.Any(s =>
                            s.Name.Equals(request.SearchQuery))));
        }
        */

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
                    t.Name,
                    p.Roles.Select(r => new GetAllProjectsTeamRoleViewModel(
                        r.Id.ToString(),
                        r.Role.Name,
                        r.PositionCount - t.Members.Count(m => m.ProjectRoleId == r.Id),
                        t.Members
                            .Where(m => m.ProjectRoleId == r.Id)
                            .Join(
                                context.Users,
                                m => m.UserId,
                                u => u.Id,
                                (m , u) => new
                                {
                                    TeamMemberUserId = m.UserId,
                                    UserName = u.FirstName + " " +  u.LastName
                                })
                            .Select(m => new GetAllProjectsTeamMemberViewModel(
                                m.TeamMemberUserId.ToString(),
                                m.UserName))))
                ))
            )).ToListAsync(cancellation);

        return new GetAllProjectsResponse(response);
    }
}