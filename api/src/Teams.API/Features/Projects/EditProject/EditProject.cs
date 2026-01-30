using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.API.Services;
using Teams.Domain.SharedKernel;
using Teams.Infrastructure;

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
        List<string> TeamIds);
    
    public sealed record Response(
        string Id,
        string Name,
        string Description,
        string Type,
        string Status,
        IEnumerable<RoleViewModel> Roles,
        IEnumerable<TeamViewModel> Teams);

    public sealed record Role(
        string Id,
        // TODO string RoleId,
        int PositionCount,
        List<string> SkillIds);
    
    public sealed record Command(
        string Id,
        string Name,
        string Description,
        string Type,
        string Status,
        List<Role> Roles,
        List<string> TeamIds)
        : IRequest<Response>;

    public sealed record RoleViewModel(
        string Id,
        string RoleId,
        string RoleName,
        int PositionCount,
        IEnumerable<SkillViewModel> Skills);
    
    public sealed record TeamViewModel(
        string Id,
        string Name);
    
    public sealed record SkillViewModel(string Id, string Name);

    public static void Map(RouteGroupBuilder group) => group
        .MapPut("{projectId}", EditProjectAsync)
        .WithSummary("Overwrite properties of a project, edit project roles, and remove project teams.")
        .WithDescription("Include the IDs of teams to be retained; those absent will be removed." +
                         "Teams can be removed regardless of whether they still have members.")
        .RequireAuthorization();

    private static async Task<Ok<Response>> EditProjectAsync(
        string projectId,
        Request request,
        IMediator mediator)
    {
        var command = new Command(
            projectId,
            request.Name,
            request.Description,
            request.Type,
            request.Status,
            request.Roles,
            request.TeamIds);
            
        var response = await mediator.Send(command);
        return TypedResults.Ok(response);
    }

    internal sealed class CommandHandler(
        IAuthorizationService authorizationService,
        IIdentityService identityService,
        TeamDbContext dbContext)
        : IRequestHandler<Command, Response>
    {
        public async Task<Response> Handle(
            Command command,
            CancellationToken cancellationToken)
        {
            var project = await dbContext.Projects
                .Include(p => p.Roles)
                .Include(p => p.Teams)
                    .ThenInclude(t => t.Members)
                // .Include(project => project.Teams)
                    // .ThenInclude(t => t.MembershipRequests)
                .Include(p => p.Owner)
                .SingleOrDefaultAsync(p => p.Id == new Guid(command.Id), cancellationToken);

            // Check user authorization to edit project.
            var authorizationResult = await authorizationService.AuthorizeAsync(
                identityService.GetUser(),
                project,
                [new IsOwnerRequirement()]);
            if (!authorizationResult.Succeeded)
            {
                throw new UnauthorizedAccessException("User lacks the permissions to edit this project.");
            }
            
            // Mutate project object through its public interface.
            project.Rename(command.Name);
            project.ChangeDescription(command.Description);
            project.SetType(ProjectType.FromName(command.Type));
            Enum.TryParse<ProjectStatus>(command.Status, true, out var status);
            project.SetStatus(status);
            project.RemoveExcludedTeams(command.TeamIds.Select(t => new Guid(t)));
            
            await dbContext.SaveChangesAsync(cancellationToken);
            
            return new Response(
                project.Id.ToString(),
                project.Name,
                project.Description,
                project.Type.ToString(),
                project.Status.ToString(),
                project.Roles
                    .Select(r => new RoleViewModel(
                        r.Id.ToString(),
                        r.Role.Id.ToString(),
                        r.Role.Name,
                        r.PositionCount,
                        r.Skills
                        .Select(s => new SkillViewModel(
                            s.Id.ToString(),
                            s.Name)))),
                project.Teams
                    .Select(t => new TeamViewModel(
                        t.Id.ToString(),
                        t.Name)));
        }
    }
}


