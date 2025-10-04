using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.Infrastructure;

namespace Teams.API.Features.Projects;

public static class GetProjectById
{
    public sealed record Query(int Id) : IRequest<Response>;
    
    public sealed record Response
    {
        /// <summary>
        /// The unique identifier of the project.
        /// </summary> 
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Type { get; set; }
        public required string Status { get; set; }
        public required List<ResponseRole> Roles { get; set; }
    }

    public sealed record ResponseRole
    {
        public required int ProjectRoleId { get; set; }
        public required int RoleId { get; set; }
        public required string Name { get; set; }
        public required List<ResponseRoleSkill> Skills { get; set; }
    }

    public sealed record ResponseRoleSkill
    {
        public required int ProjectRoleSkillId { get; set; }
        public required int SkillId { get; set; }
        public required string Name { get; set; }
        public required string Proficiency { get; set; }
    }

    public static void MapEndpoint(RouteGroupBuilder group) => group 
        .MapGet("/{id}", GetProjectAsync)
        .WithSummary("Get a project by its unique identifier.");
    
    private static async Task<Results<Ok<Response>, NotFound>> GetProjectAsync(
        int id, ISender sender)
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
                        .ThenInclude(s => s.Skill)
                .FirstOrDefaultAsync(p => p.Id == request.Id);

            if (project is null)
            {
                throw new KeyNotFoundException();
            }

            return new Response
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Type = project.Type.ToString(),
                Status = project.Status.ToString(),
                Roles =  project.Roles
                    .Select(r => new ResponseRole
                    {
                        ProjectRoleId = r.Id,
                        RoleId = r.RoleId,
                        Name = r.Role.Name,
                        Skills = r.Skills
                            .Select(s => new ResponseRoleSkill
                            {
                               ProjectRoleSkillId = s.Id,
                               SkillId = s.SkillId,
                               Name = s.Skill.Name,
                               Proficiency = s.Proficiency.ToString()
                            })
                            .ToList()
                    })
                    .ToList()
            };
        } 
    }
}