using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.Infrastructure;

namespace Teams.API.Features.Teams.GetTeam;

public sealed record GetTeamQuery(string Id) : IRequest<GetTeamResponse?>;

public sealed record GetTeamResponse
{
    // TODO
}

public static class GetTeamEndpoint
{
    public static void Map(RouteGroupBuilder builder) => builder
        .MapGet("{id}", GetTeamAsync)
        .WithDescription("Get a team by its ID.");

    private static async Task<Results<Ok<GetTeamResponse>, NotFound>> GetTeamAsync(
        string id, IMediator mediator)
    {
        var response = await mediator.Send(new GetTeamQuery(id));
        return (response is not null) ? TypedResults.Ok(response) : TypedResults.NotFound();
    }
}

internal sealed class GetTeamQueryHandler(TeamDbContext context) : IRequestHandler<GetTeamQuery, GetTeamResponse?>
{
    public async Task<GetTeamResponse?> Handle(GetTeamQuery request, CancellationToken cancellationToken)
    {
        return await context.Teams
            .Where(t => t.Id == new Guid(request.Id))
            .Select(t => new GetTeamResponse
            {
                // TODO
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
