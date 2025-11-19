using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.Infrastructure;

namespace Teams.API.Features.Projects;

public static class GetProjectById
{
    public sealed record Query(string Id) : IRequest<Response>;
    
    public sealed record Response
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Type { get; set; }
        public required string Status { get; set; }
        public required List<ResponseRole> Roles { get; set; }
        public required List<GetProjectByIdTeamViewModel>  Teams { get; set; }
    }

    public sealed record ResponseRole
    {
        public required string ProjectRoleId { get; set; }
        public required string RoleId { get; set; }
        public required string RoleName { get; set; }
        
        /// <summary>
        // The maximum number of positions for a role,
        // each to be "filled" by a team member.
        /// </summary>
        public required int PositionCount { get; set; }
        public required List<ResponseRoleSkill> Skills { get; set; }
    }

    public sealed record ResponseRoleSkill
    {
        public required string ProjectRoleSkillId { get; set; }
        public required string SkillId { get; set; }
        public required string SkillName { get; set; }
    }

    public sealed record GetProjectByIdTeamViewModel(string Id, string Name);

    public static void MapEndpoint(RouteGroupBuilder group) => group
        .MapGet("/{id}", GetProjectAsync)
        .WithSummary("Get a project by its unique identifier.");
    
    private static async Task<Results<Ok<Response>, NotFound>> GetProjectAsync(
        string id, ISender sender)
    {
        try
        {
            var project = await sender.Send(new Query(id));
            return TypedResults.Ok(project);
        }
        catch
        {
            return TypedResults.NotFound();
        }
    }

    internal sealed class QueryHandler(TeamDbContext context) : IRequestHandler<Query, Response>
    {
        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var project = await context.Projects
                .Include(p => p.Roles)
                    .ThenInclude(r => r.Role)
                .Include(p => p.Roles)
                    .ThenInclude(r => r.Skills)
                .Include(p => p.Teams)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            if (project is null) throw new KeyNotFoundException();

            return new Response
            {
                Id = project.Id.ToString(),
                Name = project.Name,
                Description = project.Description,
                Type = project.Type.ToString(),
                Status = project.Status.ToString(),
                Roles =  project.Roles
                    .Select(r => new ResponseRole
                    {
                        ProjectRoleId = r.Id.ToString(),
                        RoleId = r.RoleId.ToString(),
                        RoleName = r.Role.Name,
                        PositionCount = r.PositionCount,
                        Skills = r.Skills
                            .Select(s => new ResponseRoleSkill
                            {
                               ProjectRoleSkillId = s.Id.ToString(),
                               SkillId = s.Id.ToString(),
                               SkillName = s.Name
                            })
                            .ToList()
                    })
                    .ToList(),
                Teams = project.Teams
                    .Select(t => new GetProjectByIdTeamViewModel(t.Id.ToString(), t.Name))
                    .ToList()
            };
        } 
    }
}