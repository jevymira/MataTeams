using System.Security.Claims;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Domain.SharedKernel;
using Teams.Infrastructure;
using Teams.Contracts;

namespace Teams.API.Features.Projects.CreateProject;

public sealed record CreateProjectRequest(
    string Name,
    string Description,
    string Type, 
    string Status,
    List<CreateProjectRequestRole> Roles);

public sealed record CreateProjectRequestRole(
    string RoleId,
    // The maximum number of positions for a role,
    // each to be "filled" by a team member.
    int PositionCount,
    List<string> SkillIds);

public sealed record CreateProjectCommand() : IRequest<CreateProjectResponse>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Type { get; init; }
    public required string Status { get; init; }
    public required List<CreateProjectRequestRole> Roles { get; init; }
    
    /// <remarks>
    /// To retrieve the corresponding user's id within this context.
    /// </remarks>
    public required string OwnerIdentityGuid { get; init; }
}

public sealed record CreateProjectResponse
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Type { get; init; }
    public required string Status { get; init; }
    public required List<Role> Roles { get; init; }

    public sealed record Role
    {
        public required string ProjectRoleId { get; init; }
        public required string RoleId { get; init; }
        public required string RoleName { get; init; }
        public required int PositionCount { get; init; }
        public required List<Skill> Skills { get; init; }
    }

    public sealed record Skill
    {
        public required string ProjectRoleSkillId { get; init; }
        public required string SkillId { get; init; }
        public required string SkillName { get; init; }
    }
}

public class CreateProjectEndpoint
{
    public static void Map(RouteGroupBuilder group) => group
        .MapPost("", CreateProjectAsync)
        .RequireAuthorization()
        .WithSummary("Create a new project. Define roles and associated skills for each.");

    private static async Task<Results<Created<CreateProjectResponse>, BadRequest<string>>> CreateProjectAsync(
        CreateProjectRequest request, ISender sender, IHttpContextAccessor accessor)
    {
        var result = await sender.Send(new CreateProjectCommand
        {
            Name = request.Name,
            Description = request.Description,
            Type = request.Type,
            Status = request.Status,
            Roles = request.Roles,
            OwnerIdentityGuid = accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        });

        return TypedResults.Created($"api/projects/{result.Id}", result);
    }
}

internal sealed class CreateProjectCommandHandler(TeamDbContext context, IPublishEndpoint publishEndpoint)
    : IRequestHandler<CreateProjectCommand, CreateProjectResponse>
{

    public async Task<CreateProjectResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        Enum.TryParse<ProjectStatus>(request.Status, true, out var status);

        var owner = await context.Users
            .FirstOrDefaultAsync(m => m.IdentityGuid == request.OwnerIdentityGuid, cancellationToken);

        var projectId = Guid.CreateVersion7();
        var projectRoleId = Guid.CreateVersion7();
        
        var project = new Project(
            projectId,
            request.Name,
            request.Description,
            ProjectType.FromName(request.Type),
            status, 
            owner!.Id);
        
        foreach (var role in request.Roles)
        {
            var projectRole = project.AddProjectRole(projectRoleId, Guid.Parse(role.RoleId), role.PositionCount);
            var skills = await context.Skills
                .Where(s => role.SkillIds.Contains(s.Id.ToString()))
                .ToListAsync(cancellationToken);
            
            foreach (var skill in skills)
            {
                projectRole.AddProjectSkill(skill);
            }
        }
        
        context.Projects.Add(project);

        await context.SaveChangesAsync(cancellationToken);

        // To load in Role.
        project = await context.Projects
            .AsNoTracking()
            .Include(p => p.Roles)
                .ThenInclude(p => p.Role)
            .Include(p => p.Roles)
                .ThenInclude(p => p.Skills)
            .FirstOrDefaultAsync(m => m.Id == project.Id, cancellationToken);
        
        await publishEndpoint.Publish(new ProjectCreated(
            projectId, 
            project.Name,
            project.Description,
            project.Type.Name,
            project.Status.ToString(),
            owner.Id
        ), cancellationToken);

        return new CreateProjectResponse
        {
            Id = project.Id.ToString(),
            Name = project.Name,
            Description = project.Description,
            Type = project.Type.ToString(),
            Status = project.Status.ToString(),
            Roles = project.Roles
                .Select(r => new CreateProjectResponse.Role
                {
                    ProjectRoleId = r.Id.ToString(),
                    RoleId = r.RoleId.ToString(),
                    RoleName = r.Role.Name,
                    PositionCount = r.PositionCount,
                    Skills = r.Skills
                        .Select(s => new CreateProjectResponse.Skill
                        {
                            ProjectRoleSkillId = s.Id.ToString(),
                            SkillId = s.Id.ToString(),
                            SkillName = s.Name
                        })
                        .ToList()
                })
                .ToList()
        };
    }
}
