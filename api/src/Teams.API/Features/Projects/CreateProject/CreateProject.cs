using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.API.Services;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Domain.SharedKernel;
using Teams.Infrastructure;
using Teams.Contracts;

namespace Teams.API.Features.Projects.CreateProject;

public static class CreateProject
{
    public sealed record Command(
        string Name,
        string Description,
        string Type,
        string Status,
        List<Role> Roles
    ) : IRequest<Response>;

    public sealed record Response(
        string Id,
        string Name,
        string Description,
        string Type,
        string Status,
        List<RoleViewModel> Roles
    );

    public sealed record Role(
        string RoleId,
        int PositionCount,
        List<string> SkillIds
    );

    public sealed record RoleViewModel(
        string ProjectRoleId,
        string RoleId,
        string RoleName,
        int PositionCount,
        List<RoleSkillViewModel> Skills
    );

    public sealed record RoleSkillViewModel(
        string SkillId,
        string SkillName
    );
    
    /*
    public sealed record TeamViewModel(
        string Id,
        string Name,
        IEnumerable<TeamRoleViewModel> ProjectRoles
    );

    public sealed record TeamRoleViewModel(
        string Id,
        string RoleName,
        int VacantPositionCount,
        IEnumerable<TeamMemberViewModel> Members
    );

    public sealed record TeamMemberViewModel(
        string UserId,
        string Name
    );
    */
    
    public static void MapEndpoint(RouteGroupBuilder builder) => builder
        .MapPost("", CreateAsync)
        .RequireAuthorization()
        .WithSummary("Create a new project. Define roles and associated skills for each.");

    public static async Task<Created<Response>> CreateAsync(
        IMediator mediator,
        Command command)
    {
        var response = await mediator.Send(command);
        return TypedResults.Created($"api/projects/{response.Id}", response);
    }

    internal sealed class CommandHandler(
        IIdentityService identityService,
        TeamDbContext dbContext,
        IPublishEndpoint publishEndpoint
    ) : IRequestHandler<Command, Response>
    {
        public async Task<Response> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            Enum.TryParse<ProjectStatus>(request.Status, true, out var status);

            var owner = await dbContext.Users
                .FirstOrDefaultAsync(m => m.IdentityGuid == identityService.GetUserIdentity(),
                    cancellationToken);
        
            var project = new Project(
                request.Name,
                request.Description,
                ProjectType.FromName(request.Type),
                status, 
                owner!.Id);

            //skills for publishing to bus
            var allSkillIds = new List<String>();
        
            foreach (var role in request.Roles)
            {
                var skills = await dbContext.Skills
                    .Where(s => role.SkillIds.Contains(s.Id.ToString()))
                    .ToListAsync(cancellationToken);
            
                var projectRole = project.AddProjectRole(Guid.Parse(role.RoleId), role.PositionCount, skills);
            
                foreach (var skill in skills)
                {
                    allSkillIds.Add(skill.Id.ToString());
                }
            }
        
            dbContext.Projects.Add(project);

            await dbContext.SaveChangesAsync(cancellationToken);
        
            await publishEndpoint.Publish(new ProjectCreated(
                project.Id, 
                project.Name,
                project.Description,
                project.Type.Name,
                project.Status.ToString(),
                owner.Id,
                allSkillIds
            ), cancellationToken);

            var proj = dbContext.Projects
                .Include(x => x.Roles)
                    .ThenInclude(x => x.Role)
                .Include(x => x.Roles)
                    .ThenInclude(x => x.Skills)
                .Single(x => x.Id == project.Id);
            
            var roles = proj.Roles.Select(r => new RoleViewModel(
                r.Id.ToString(),
                r.RoleId.ToString(),
                r.Role.Name,
                r.PositionCount,
                r.Skills.Select(s => new RoleSkillViewModel(
                    s.Id.ToString(),
                    s.Name
                )).ToList()
            ))
            .ToList();
            
            var response =  new Response(
                proj.Id.ToString(),
                proj.Name,
                proj.Description,
                proj.Type.Name,
                proj.Status.ToString(),
                roles
            );

            return response;
        }
    }
}

/*
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

    private static async Task<Created<string>> CreateProjectAsync(
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

        return TypedResults.Created<string>($"api/projects/{projectId}", projectId);
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

        //skills for publishing to bus
        var allSkillIds = new List<String>();
        
        foreach (var role in request.Roles)
        {
            var skills = await context.Skills
                .Where(s => role.SkillIds.Contains(s.Id.ToString()))
                .ToListAsync(cancellationToken);
            
            var projectRole = project.AddProjectRole(Guid.Parse(role.RoleId), role.PositionCount, skills);
            
            foreach (var skill in skills)
            {
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
*/
