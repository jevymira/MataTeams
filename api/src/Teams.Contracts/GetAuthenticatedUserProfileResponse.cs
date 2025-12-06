namespace Teams.Contracts;

public record UserProfileFetched(
    Guid UserId,
    string IdentityGuid,
    string FirstName,
    string LastName,
    bool IsFacultyOrStaff,
    List<string> ProgramNames,
    List<string> SkillIds, 
    DateTime FetchedAt
);