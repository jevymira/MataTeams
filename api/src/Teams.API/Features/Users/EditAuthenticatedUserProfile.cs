using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.API.Services;
using Teams.Infrastructure;

namespace Teams.API.Features.Users;

public sealed record EditAuthenticatedUserProfileCommand(List<string> SkillIds)
    : IRequest<EditAuthenticatedUserProfileResponse>;

public sealed record EditAuthenticatedUserProfileResponse(
    string FirstName,
    string LastName,
    bool IsFacultyOrStaff,
    IEnumerable<string> Programs,
    IEnumerable<EditAuthenticatedUserProfileSkillViewModel> Skills
);

public sealed record EditAuthenticatedUserProfileSkillViewModel(string Id, string Name);

public static class EditAuthenticatedUserProfileEndpoint
{
    public static void Map(RouteGroupBuilder builder) => builder
        .MapPatch("me", EditAuthenticatedUserProfileAsync)
        .WithSummary("Overwrite the skills of the authenticated user.")
        .RequireAuthorization();

    private static async Task<Ok<EditAuthenticatedUserProfileResponse>> EditAuthenticatedUserProfileAsync(
        EditAuthenticatedUserProfileCommand command,
        IMediator mediator)
    {
        var response = await mediator.Send(command);
        return TypedResults.Ok(response);
    }
}

internal sealed class EditAuthenticatedUserProfileCommandHandler(
    TeamDbContext context,
    IIdentityService identityService)
    : IRequestHandler<EditAuthenticatedUserProfileCommand, EditAuthenticatedUserProfileResponse>
{
    public async Task<EditAuthenticatedUserProfileResponse> Handle(
        EditAuthenticatedUserProfileCommand request, CancellationToken cancellationToken)
    {
        var newSkills = await context.Skills
            .Where(s => request.SkillIds.Contains(s.Id.ToString()))
            .ToListAsync(cancellationToken);
        
        var profile = await context.Users
            .Include(u => u.Skills)
            .SingleAsync(u => u.IdentityGuid == identityService.GetUserIdentity(),
                cancellationToken);
        
        // .ToList() materializes the sequence first; otherwise,
        // .Except() does not create a copy of the collection but instead
        //  a deferred LINQ query, meaning enumeration when foreach runs
        var toAdd = newSkills.Except(profile.Skills).ToList();
        var toRemove = profile.Skills.Except(newSkills).ToList();
        
        foreach (var skill in toAdd) profile.AddSkill(skill);
        foreach (var skill in toRemove) profile.RemoveSkill(skill);
        
        await context.SaveChangesAsync(cancellationToken);

        return new EditAuthenticatedUserProfileResponse(
            profile.FirstName,
            profile.LastName,
            profile.IsFacultyOrStaff,
            profile.Programs,
            profile.Skills.Select(s => new EditAuthenticatedUserProfileSkillViewModel(s.Id.ToString(), s.Name))
        );
    }
}
