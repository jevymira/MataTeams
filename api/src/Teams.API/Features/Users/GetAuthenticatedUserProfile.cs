using MassTransit.Initializers;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.API.Services;
using Teams.Infrastructure;

namespace Teams.API.Features.Users;

public sealed record GetAuthenticatedUserProfileQuery : IRequest<GetAuthenticatedUserProfileResponse>;

public sealed record GetAuthenticatedUserProfileResponse(
    string FirstName,
    string LastName,
    bool IsFacultyOrStaff,
    IEnumerable<string> Programs,
    IEnumerable<GetAuthenticatedUserProfileSkillViewModel> Skills
);

public sealed record GetAuthenticatedUserProfileSkillViewModel(string Id, string Name);

public static class GetAuthenticatedUserProfileEndpoint
{
    public static void Map(RouteGroupBuilder builder) => builder
        .MapGet("me", GetAuthenticatedUserProfileAsync)
        .WithSummary("Get the profile details of the current logged in user.")
        .RequireAuthorization();

    private static async Task<Ok<GetAuthenticatedUserProfileResponse>> GetAuthenticatedUserProfileAsync(
        IMediator mediator)
    {
        var response = await mediator.Send(new GetAuthenticatedUserProfileQuery());
        return TypedResults.Ok(response);
    }
}

internal sealed class GetAuthenticatedUserProfileQueryHandler(
    TeamDbContext context,
    IIdentityService identityService)
    : IRequestHandler<GetAuthenticatedUserProfileQuery, GetAuthenticatedUserProfileResponse>
{
    public async Task<GetAuthenticatedUserProfileResponse> Handle(
        GetAuthenticatedUserProfileQuery query,
        CancellationToken cancellation)
    {
        return await context.Users
            .Include(user => user.Skills)
            .SingleAsync(u => u.IdentityGuid == identityService.GetUserIdentity(),
                cancellation)
            .Select(u => new GetAuthenticatedUserProfileResponse(
                u.FirstName,
                u.LastName,
                u.IsFacultyOrStaff,
                u.Programs,
                u.Skills.Select(s => new GetAuthenticatedUserProfileSkillViewModel(
                    s.Id.ToString(), s.Name))
            ));
    }
}