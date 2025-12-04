using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.Infrastructure;

namespace Teams.API.Features.Users;

public sealed record GetProfileByIdQuery(string TeamId) : IRequest<GetProfileByIdResponse?>;

public sealed record GetProfileByIdResponse(
    string FirstName,
    string LastName,
    bool IsFacultyOrStaff,
    IEnumerable<string> Programs,
    IEnumerable<GetProfileByIdSkillViewModel> Skills);

public sealed record GetProfileByIdSkillViewModel(string Id, string Name);

public class GetProfileByIdEndpoint
{
    public static void Map(RouteGroupBuilder builder) => builder
        .MapGet("{userId}", GetProfileByIdAsync);

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
        return await dbContext.Users
            .Where(u => u.Id == new Guid(request.TeamId))
            .Select(u => new GetProfileByIdResponse(
                u.FirstName,
                u.LastName,
                u.IsFacultyOrStaff,
                u.Programs,
                u.Skills
                    .Select(s => new GetProfileByIdSkillViewModel(
                        s.Id.ToString(),
                        s.Name))))
            .SingleOrDefaultAsync(cancellationToken);
    }
}