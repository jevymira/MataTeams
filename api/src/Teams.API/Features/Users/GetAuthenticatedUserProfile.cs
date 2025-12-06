using MassTransit.Initializers;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.API.Services;
using Teams.Infrastructure;
using Teams.Contracts;
using MassTransit;

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

// internal sealed class GetAuthenticatedUserProfileQueryHandler(
//     TeamDbContext context,
//     IIdentityService identityService)
//     : IRequestHandler<GetAuthenticatedUserProfileQuery, GetAuthenticatedUserProfileResponse>
// {
//     public async Task<GetAuthenticatedUserProfileResponse> Handle(
//         GetAuthenticatedUserProfileQuery query,
//         CancellationToken cancellation)
//     {
//         return await context.Users
//             .Include(user => user.Skills)
//             .SingleAsync(u => u.IdentityGuid == identityService.GetUserIdentity(),
//                 cancellation)
//             .Select(u => new GetAuthenticatedUserProfileResponse(
//                 u.FirstName,
//                 u.LastName,
//                 u.IsFacultyOrStaff,
//                 u.Programs,
//                 u.Skills.Select(s => new GetAuthenticatedUserProfileSkillViewModel(
//                     s.Id.ToString(), s.Name))
//             ));
//     }
// }

internal sealed class GetAuthenticatedUserProfileQueryHandler(
    TeamDbContext context,
    IIdentityService identityService,
    IPublishEndpoint publishEndpoint,
    ILogger<GetAuthenticatedUserProfileQueryHandler> logger)
    : IRequestHandler<GetAuthenticatedUserProfileQuery, GetAuthenticatedUserProfileResponse>
{
    public async Task<GetAuthenticatedUserProfileResponse> Handle(
        GetAuthenticatedUserProfileQuery query,
        CancellationToken cancellation)
    {
        logger.LogInformation("Starting GetAuthenticatedUserProfileQueryHandler");
        
        var user = await context.Users
            .Include(user => user.Skills)
            .SingleAsync(u => u.IdentityGuid == identityService.GetUserIdentity(), cancellation);
        
        logger.LogInformation("Preparing to publish UserProfileFetched for user {UserId} with {SkillCount} skills", 
            user.Id, user.Skills.Count);
        
        try
        {
            var identityGuid = user.IdentityGuid; 
            
            logger.LogInformation("User IdentityGuid: {IdentityGuid}", identityGuid);
            
            var eventData = new UserProfileFetched(
                user.Id,
                identityGuid, // Use the actual IdentityGuid
                user.FirstName,
                user.LastName,
                user.IsFacultyOrStaff,
                user.Programs.ToList(),
                user.Skills.Select(s => s.Id.ToString()).ToList(),
                DateTime.UtcNow
            );
            
            logger.LogInformation("Publishing event: {@EventData}", eventData);
            
            await publishEndpoint.Publish(eventData, cancellation);
            
            logger.LogInformation("Successfully published UserProfileFetched");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to publish UserProfileFetched");
        }

        return new GetAuthenticatedUserProfileResponse(
            user.FirstName,
            user.LastName,
            user.IsFacultyOrStaff,
            user.Programs,
            user.Skills.Select(s => new GetAuthenticatedUserProfileSkillViewModel(
                s.Id.ToString(), s.Name))
        );
    }
}