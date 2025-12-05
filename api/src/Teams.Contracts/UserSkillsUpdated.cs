namespace Teams.Contracts;

public record UserSkillsUpdated(
    Guid UserId,
    List<string> SkillIds,
    DateTime UpdatedAt
);