using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.API.Services;
using Teams.Domain.Aggregates.UserAggregate;
using Teams.Domain.SharedKernel;
using Teams.Infrastructure;

namespace Teams.API.Features.Users;

public sealed record CreateProfileCommand : IRequest<CreateProfileResponse>
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required bool IsFacultyOrStaff { get; init; }
    
    /// <summary>
    /// Academic program(s) of involvement, e.g., Computer Science.
    /// For a complete list, see https://www.csun.edu/node/11001/academic-programs
    /// </summary>
    public required IEnumerable<string> Programs { get; init; }
    public required IEnumerable<string> SkillIds { get; init; }
}

public sealed record CreateProfileResponse
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required bool IsFacultyOrStaff { get; init; }
    public required IEnumerable<string> Programs { get; init; }
    public required IEnumerable<CreateProfileSkillModel> Skills { get; init; }
}

public sealed record CreateProfileSkillModel
{
    public required string Name { get; init; }
}

public static class CreateProfile
{
    public static void MapEndpoint(RouteGroupBuilder builder) => builder
        .MapPost("", CreateProfileAsync)
        .WithSummary("Create a profile for the authenticated user. Optionally enumerate skills.")
        .RequireAuthorization();

    private static async Task<Results<Ok<CreateProfileResponse>, Conflict<string>>> CreateProfileAsync(
        CreateProfileCommand command,
        IMediator mediator)
    {
        try
        {
            var response = await mediator.Send(command);
            return TypedResults.Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return TypedResults.Conflict(ex.Message);
        }
    }
}

internal sealed class CreateProfileCommandHandler(
    TeamDbContext context,
    IIdentityService identityService)
    : IRequestHandler<CreateProfileCommand, CreateProfileResponse>
{
    public async Task<CreateProfileResponse> Handle(CreateProfileCommand command, CancellationToken cancellation)
    {
        var user = context.Users
            .FirstOrDefault(u => u.IdentityGuid == identityService.GetUserIdentity());

        if (user is not null)
        {
            throw new InvalidOperationException("Profile already exists for the authenticated user.");
        }
        
        user = new User(Guid.CreateVersion7(), command.FirstName,command.LastName, command.IsFacultyOrStaff, identityService.GetUserIdentity());
        
        var skills = await context.Skills
            .Where(s => command.SkillIds.Contains(s.Id.ToString()))
            .ToListAsync(cancellation);
        
        foreach (var skill in skills)
        {
            user.AddSkill(skill, Proficiency.Proficient); // TODO: remove proficiencies
        }
        
        context.Users.Add(user);
        
        await context.SaveChangesAsync(cancellation);
        
        return new CreateProfileResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            IsFacultyOrStaff = user.IsFacultyOrStaff,
            Programs = user.Programs,
            Skills = skills.Select(s => new CreateProfileSkillModel
            {
                Name = s.Name
            })
        };
    }
}