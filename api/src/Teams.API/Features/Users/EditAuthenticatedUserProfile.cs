using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.API.Services;
using Teams.Infrastructure;

namespace Teams.API.Features.Users;

public static class EditAuthenticatedUserProfile
{
    public sealed record Command(
        string FirstName,
        string LastName,
        List<string> SkillIds
    ) : IRequest<Response>;

    public sealed record Response(
        string FirstName,
        string LastName,
        bool IsFacultyOrStaff,
        IEnumerable<string> Programs,
        IEnumerable<SkillViewModel> SkillIds
    );

    public sealed record SkillViewModel(string Id, string Name);

    public static void MapEndpoint(RouteGroupBuilder builder) => builder
        .MapPut("me", EditProfileAsync)
        .WithSummary("Rename and/or overwrite the skills of the authenticated user.")
        .RequireAuthorization();

    private static async Task<Ok<Response>> EditProfileAsync(
        Command command,
        IMediator mediator
    )
    {
        var response = await mediator.Send(command);
        return TypedResults.Ok(response);
    }

    internal sealed class CommandHandler(
        TeamDbContext dbContext,
        IIdentityService identityService
    ) : IRequestHandler<Command, Response>
    {
        public async Task<Response> Handle(
            Command command, 
            CancellationToken cancellationToken
        )
        {
            var newSkills = await dbContext.Skills
            .Where(s => command.SkillIds.Contains(s.Id.ToString()))
            .ToListAsync(cancellationToken);

            var profile = await dbContext.Users
                .Include(u => u.Skills)
                .SingleAsync(u => u.IdentityGuid == identityService.GetUserIdentity(),
                    cancellationToken);
            
            profile.ChangeFirstName(command.FirstName);
            profile.ChangeLastName(command.LastName);

            // .ToList() materializes the sequence first; otherwise,
            // .Except() does not create a copy of the collection but instead
            //  a deferred LINQ query, meaning enumeration when foreach runs
            var toAdd = newSkills.Except(profile.Skills).ToList();
            var toRemove = profile.Skills.Except(newSkills).ToList();

            foreach (var skill in toAdd) profile.AddSkill(skill);
            foreach (var skill in toRemove) profile.RemoveSkill(skill);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new Response(
                profile.FirstName,
                profile.LastName,
                profile.IsFacultyOrStaff,
                profile.Programs,
                profile.Skills.Select(s => new SkillViewModel(s.Id.ToString(), s.Name))
            );
        }
    }
}
