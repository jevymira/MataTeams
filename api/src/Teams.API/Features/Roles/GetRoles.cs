using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.Infrastructure;

namespace Teams.API.Features.Roles;

public sealed record GetRolesQuery : IRequest<IEnumerable<RoleViewModel>>;

public static class GetRolesEndpoint
{
    public static void Map(RouteGroupBuilder group) => group
        .MapGet("", GetRolesAsync)
        .WithSummary("Get all roles.");

    private static async Task<Ok<IEnumerable<RoleViewModel>>> GetRolesAsync(ISender sender)
    {
        var roles = await sender.Send(new GetRolesQuery());
        return TypedResults.Ok(roles);
    }
}

internal sealed class GetRolesQueryHandler(TeamDbContext context)
    : IRequestHandler<GetRolesQuery, IEnumerable<RoleViewModel>>
{
    public async Task<IEnumerable<RoleViewModel>> Handle(
        GetRolesQuery request, CancellationToken cancellationToken)
    {
        return await context.Roles
            .Select(r => new RoleViewModel
            {
                Id = r.Id.ToString(),
                Name = r.Name,
            })
            .ToListAsync(cancellationToken);
    }
}