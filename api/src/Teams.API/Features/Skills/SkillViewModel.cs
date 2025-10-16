namespace Teams.API.Features.Skills;

public record SkillViewModel
{
    public required string Id { get; init; }
    public required string Name { get; init; }
}