using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.Infrastructure;

namespace Teams.API.Features.Users;

public sealed record GetProfileByIdQuery(string userId) : IRequest<GetProfileByIdResponse?>;

public sealed record GetProfileByIdResponse(
    string FirstName,
    string LastName,
    bool IsFacultyOrStaff,
    IEnumerable<string> Programs,
    IEnumerable<GetProfileByIdSkillViewModel> Skills,
    List<GetProfileByIdProjectRoleViewModel> ProjectsIsOwner,
    List<GetProfileByIdProjectRoleViewModel> ProjectsIsMember);

public sealed record GetProfileByIdSkillViewModel(string Id, string Name);

public sealed record GetProfileByIdProjectRoleViewModel(
    string ProjectId,
    string ProjectName,
    string OwnerName,
    string OwnerId,
    // string TeamId,
    // string TeamName,
    // string ProjectRoleId,
    string RoleName,
    IEnumerable<GetProfileByIdSkillViewModel> ProjectRoleSkills);

public class GetProfileByIdEndpoint
{
    public static void Map(RouteGroupBuilder builder) => builder
        .MapGet("{userId}", GetProfileByIdAsync)
        .WithSummary("Get a user profile by its ID, including the user's skills and projects.");

    private static async Task<Results<Ok<GetProfileByIdResponse>, NotFound>> GetProfileByIdAsync(
        string userId,
        IMediator mediator)
    {
        var response = await mediator.Send(new GetProfileByIdQuery(userId));
        return (response is null) ? TypedResults.NotFound() : TypedResults.Ok(response);
    }
}

internal sealed class GetProfileByIdQueryHandler(TeamDbContext dbContext)
    : IRequestHandler<GetProfileByIdQuery, GetProfileByIdResponse?>
{
    public async Task<GetProfileByIdResponse?> Handle(
        GetProfileByIdQuery request,
        CancellationToken cancellationToken)
    {
        Guid.TryParse(request.userId, out var userId);

        var models = await (
            from tm in dbContext.TeamMembers
            where tm.UserId == userId

            join t in dbContext.Teams
                on tm.TeamId equals t.Id

            join pr in dbContext.ProjectRoles
                on tm.ProjectRoleId equals pr.Id

            join p in dbContext.Projects
                on pr.ProjectId equals p.Id

            select new GetProfileByIdProjectRoleViewModel(
                p.Id.ToString(),
                p.Name,
                p.Owner.FirstName + " " + p.Owner.LastName,
                p.OwnerId.ToString(),
                pr.Role.Name,
                pr.Skills.Select(s => new GetProfileByIdSkillViewModel(
                    s.Id.ToString(),
                    s.Name
                ))
            )
        ).ToListAsync(cancellationToken);

        var projectsIsLeader = models.Where(p => p.OwnerId.Equals(request.userId)).ToList();
        var projectsIsMember = models.Where(p => !p.OwnerId.Equals(request.userId)).ToList();

        return await dbContext.Users
            .Where(u => u.Id == new Guid(request.userId))
            .Select(u => new GetProfileByIdResponse(
                u.FirstName,
                u.LastName,
                u.IsFacultyOrStaff,
                u.Programs,
                u.Skills.Select(s => new GetProfileByIdSkillViewModel(
                    s.Id.ToString(),
                    s.Name)),
                projectsIsLeader,
                projectsIsMember))
            .SingleOrDefaultAsync(cancellationToken);
    }
}