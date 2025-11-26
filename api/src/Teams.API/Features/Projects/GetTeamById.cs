using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.Infrastructure;

namespace Teams.API.Features.Projects;

public sealed record GetTeamByIdQuery(string Id) : IRequest<GetTeamByIdResponse?>;

public sealed record GetTeamByIdResponse(
    string TeamName,
    GetTeamByIdMemberViewModel Leader,
    IEnumerable<GetTeamByIdRoleViewModel> ProjectRoles);

public sealed record GetTeamByIdRoleViewModel(
    string Id,
    string RoleName,
    IEnumerable<string> Skills,
    int VacantPositionCount,
    IEnumerable<GetTeamByIdMemberViewModel> Members);

public sealed record GetTeamByIdMemberViewModel(
    string Id,
    string Name);

public static class GetTeamByIdEndpoint
{
    public static void Map(RouteGroupBuilder builder) => builder
        .MapGet("{teamId}", GetTeamByIdAsync)
        .WithSummary("Get the details of a team by its unique identifier, " +
                     "including members, their roles, and role vacancies.");

    private static async Task<Results<Ok<GetTeamByIdResponse>, NotFound<string>>> GetTeamByIdAsync(
        string teamId,
        IMediator mediator)
    {
        var response = await  mediator.Send(new GetTeamByIdQuery(teamId));
        
        return response is null
            ? TypedResults.NotFound($"Team not found with ID {teamId}")
            : TypedResults.Ok(response);
    }
}

internal sealed class GetTeamByIdHandler(TeamDbContext context)
    : IRequestHandler<GetTeamByIdQuery, GetTeamByIdResponse?>
{
    public async Task<GetTeamByIdResponse?> Handle(
        GetTeamByIdQuery request,
        CancellationToken cancellationToken)
    {
        var team = await context.Teams
            .Where(t => t.Id.ToString() == request.Id)
            .Include(t => t.Leader)
            .Include(t => t.Members)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (team is null) return null;

        var roles = await context.ProjectRoles
            .Where(r => r.ProjectId == team.ProjectId)
            .GroupJoin(context.TeamMembers, r => r.Id, m => m.ProjectRoleId,
                (r, m) => new GetTeamByIdRoleViewModel(
                    r.Id.ToString(),
                    r.Role.Name,
                    r.Skills.Select(s => s.Name),
                    r.PositionCount - m.Count(),
                    m
                        .Join(context.Users, tm => tm.UserId, u => u.Id,
                            (tm, u) => new
                            {
                                UserId = u.Id,
                                Name = u.FirstName + " " + u.LastName
                            })
                        .Select(usr => new GetTeamByIdMemberViewModel(usr.UserId.ToString(), usr.Name))))
            .ToListAsync(cancellationToken);

        return new GetTeamByIdResponse(
            team.Name,
            new GetTeamByIdMemberViewModel(team.Leader.Id.ToString(), team.Leader.FirstName + " " + team.Leader.LastName),
            roles);
    }
}