using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.API.Services;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Domain.SharedKernel;
using Teams.Infrastructure;

// ReSharper disable NotAccessedPositionalProperty.Global

namespace Teams.API.Features.Projects.EditProject;

public static class EditProject
{
    public sealed record Request(
        string Name,
        string Description,
        // [AllowedValues("ARCS, Faculty, Club, Class, Personal")]
        string Type,
        string Status,
        List<Role> Roles,
        List<string> TeamIds
    );
    
    public sealed record Response(
        string Id,
        string Name,
        string Description,
        string Type,
        string Status,
        IReadOnlyList<RoleViewModel> Roles,
        IReadOnlyList<TeamViewModel> Teams
    );

    public sealed record Command(
        string Id,
        string Name,
        string Description,
        string Type,
        string Status,
        List<Role> Roles,
        List<string> TeamIds
    ) : IRequest<Response>;
    
    public sealed record Role(
        string? Id, // Null when denoting new Role.
        string RoleId,
        int PositionCount,
        List<string> SkillIds
    );

    public sealed record RoleViewModel(
        string Id,
        string RoleId,
        string RoleName,
        int PositionCount,
        List<SkillViewModel> Skills
    );
    
    public sealed record TeamViewModel(string Id, string Name);
    
    public sealed record SkillViewModel(string Id, string Name);

    public static void Map(RouteGroupBuilder group) => group
        .MapPut("{projectId}", EditProjectAsync)
        .WithSummary("Overwrite properties of a project, edit project roles, and remove project teams. " +
                     "Restricted to project owner.")
        .WithDescription("New roles have null IDs. " +
                         "Include the IDs of teams to be retained; those absent will be removed." +
                         "Teams can be removed regardless of whether they still have members.")
        .RequireAuthorization();

    private static async Task<Ok<Response>> EditProjectAsync(
        string projectId,
        Request request,
        IMediator mediator
    )
    {
        var command = new Command(
            projectId,
            request.Name,
            request.Description,
            request.Type,
            request.Status,
            request.Roles,
            request.TeamIds
        );
            
        var response = await mediator.Send(command);
        return TypedResults.Ok(response);
    }

    internal sealed class CommandHandler(
        IAuthorizationService authorizationService,
        IIdentityService identityService,
        TeamDbContext dbContext
    ) : IRequestHandler<Command, Response>
    {
        public async Task<Response> Handle(
            Command command,
            CancellationToken cancellationToken)
        {
            var project = await dbContext.Projects
                .Include(p => p.Roles)
                .Include(p => p.Roles)
                    .ThenInclude(r => r.Role)
                .Include(p => p.Roles)
                    .ThenInclude(r => r.Skills)
                .Include(p => p.Teams)
                    .ThenInclude(t => t.Members)
                .Include(project => project.Teams)
                    .ThenInclude(t => t.MembershipRequests)
                .Include(p => p.Owner)
                .SingleOrDefaultAsync(p => p.Id == new Guid(command.Id), cancellationToken)
                ?? throw new KeyNotFoundException($"Project with ID {command.Id} not found");

            // Check user authorization to edit project.
            var authorizationResult = await authorizationService.AuthorizeAsync(
                identityService.GetUser(),
                project,
                [new IsOwnerRequirement()]);
            if (!authorizationResult.Succeeded)
            {
                throw new UnauthorizedAccessException("User lacks the permissions to edit this project.");
            }
            
            // Mutate the project object through its public interface.
            project.Rename(command.Name);
            project.ChangeDescription(command.Description);
            project.SetType(ProjectType.FromName(command.Type));
            Enum.TryParse<ProjectStatus>(command.Status, true, out var status);
            project.SetStatus(status);
            
            var rolesToRemove = project.Roles
                .Where(pr => !command.Roles
                    .Where(r => r.Id is not null)
                    .Select(r => Guid.Parse(r.Id))
                    .ToList()
                    .Contains(pr.Id))
                .ToList();

            foreach (var role in rolesToRemove)
            {
                project.RemoveRole(role.Id);
            }
            
            foreach (var role in command.Roles)
            {
                var skills = dbContext.Skills
                    .Where(s => role.SkillIds.Contains(s.Id.ToString()))
                    .ToList();
                
                if (role.Id is null)
                {
                    project.AddProjectRole(Guid.Parse(role.RoleId), role.PositionCount, skills);
                }
                else
                {
                    project.UpdateRole(
                        Guid.Parse(role.Id),
                        Guid.Parse(role.RoleId),
                        role.PositionCount,
                        skills
                    );
                }
            }
            
            project.RemoveExcludedTeams(command.TeamIds.Select(t => new Guid(t)));
            
            await dbContext.SaveChangesAsync(cancellationToken);
            
            var response = new Response(
                project.Id.ToString(),
                project.Name,
                project.Description,
                project.Type.ToString(),
                project.Status.ToString(),
                project.Roles.Select(r => r.ToViewModel()).ToList(),
                project.Teams.Select(t => t.ToViewModel()).ToList()
            );

            return response;
        }
    }

    private static RoleViewModel ToViewModel(this ProjectRole role) =>
        new RoleViewModel(
            role.Id.ToString(),
            role.Role.Id.ToString(),
            role.Role.Name,
            role.PositionCount,
            role.Skills.Select(s => new SkillViewModel(s.Id.ToString(), s.Name)).ToList()
        );

    private static TeamViewModel ToViewModel(this Team team) =>
        new TeamViewModel(team.Id.ToString(), team.Name);
}


