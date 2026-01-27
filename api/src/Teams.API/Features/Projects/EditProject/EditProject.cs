using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.API.Services;
using Teams.Infrastructure;

namespace Teams.API.Features.Projects.EditProject;

public static class EditProject
{
    public sealed record Request(
        string Name,
        string Description,
        string Type,
        string Status,
        List<Role> Roles
        /* List<Team> Teams */);
    
    public sealed record Response(
        string Id,
        string Name,
        string Description,
        string Type,
        string Status,
        List<RoleViewModel> Roles
        /* List<Team> Teams */);

    public sealed record Role(
        string RoleId,
        int PositionCount,
        List<string> SkillIds);
    
    // public sealed record Team
    
    public sealed record Command(
        string Id,
        string Name,
        string Description,
        string Type,
        string Status,
        List<Role> Roles
        /* List<Team> Teams */)
        : IRequest<Response>;

    public sealed record RoleViewModel(
        string RoleId,
        int PositionCount,
        List<SkillViewModel> Skills);
    
    public sealed record SkillViewModel(string Id, string Name);

    public static void Map(RouteGroupBuilder group) => group
        .MapPut("{projectId}", EditProjectAsync)
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
            request.Roles);
            
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
                // .Include(project => project.Teams)
                    // .ThenInclude(t => t.MembershipRequests)
                .Include(p => p.Teams)
                    .ThenInclude(t => t.Members)
                .Include(p => p.Owner)
                .SingleOrDefaultAsync(p => p.Id == new Guid(command.Id), cancellationToken);

            
            
            return new Response(
                project.Id.ToString(),
                project.Name,
                project.Description,
                project.Type.ToString(),
                project.Status.ToString(),
                project.Roles.Select(r => new RoleViewModel(
                    r.Id.ToString(),
                    r.PositionCount,
                    r.Skills.Select(s => new SkillViewModel(
                        s.Id.ToString(),
                        s.Name))
                        .ToList()))
                    .ToList());
        }
    }
}


