namespace Teams.Contracts;

public record UserCreated(
    Guid UserId,
    String IdentityGuild,
    string FirstName,
    string LastName,
    bool IsFacultyOrStaff,
    List<string> ProgramNames, 
    List<string> SkillIds,
    DateTime CreatedAt
);