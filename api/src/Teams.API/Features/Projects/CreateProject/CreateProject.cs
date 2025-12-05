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

public sealed record CreateProjectCommand() : IRequest<string>
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

public class CreateProjectEndpoint
{
    public static void Map(RouteGroupBuilder group) => group
        .MapPost("", CreateProjectAsync)
        .RequireAuthorization()
        .WithSummary("Create a new project. Define roles and associated skills for each.");

    private static async Task<Created> CreateProjectAsync(
        CreateProjectRequest request, ISender sender, IHttpContextAccessor accessor)
    {
        var projectId = await sender.Send(new CreateProjectCommand
        {
            Name = request.Name,
            Description = request.Description,
            Type = request.Type,
            Status = request.Status,
            Roles = request.Roles,
            OwnerIdentityGuid = accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        });

        return TypedResults.Created($"api/projects/{projectId}");
    }
}

internal sealed class CreateProjectCommandHandler(TeamDbContext context, IPublishEndpoint publishEndpoint)
    : IRequestHandler<CreateProjectCommand, string>
{

    public async Task<string> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        Enum.TryParse<ProjectStatus>(request.Status, true, out var status);

        var owner = await context.Users
            .FirstOrDefaultAsync(m => m.IdentityGuid == request.OwnerIdentityGuid, cancellationToken);
        
        var project = new Project(
            request.Name,
            request.Description,
            ProjectType.FromName(request.Type),
            status, 
            owner!.Id);

        //skills for publishing to stupid ahh bus
        var allSkillIds = new List<String>();
        
        foreach (var role in request.Roles)
        {
            var projectRole = project.AddProjectRole(Guid.Parse(role.RoleId), role.PositionCount);
            var skills = await context.Skills
                .Where(s => role.SkillIds.Contains(s.Id.ToString()))
                .ToListAsync(cancellationToken);
            
            foreach (var skill in skills)
            {
                projectRole.AddProjectSkill(skill);
                allSkillIds.Add(skill.Id.ToString());
            }
        }
        
        context.Projects.Add(project);

        await context.SaveChangesAsync(cancellationToken);
        
        await publishEndpoint.Publish(new ProjectCreated(
            project.Id, 
            project.Name,
            project.Description,
            project.Type.Name,
            project.Status.ToString(),
            owner.Id,
            allSkillIds
        ), cancellationToken);

        return project.Id.ToString();
    }
}
