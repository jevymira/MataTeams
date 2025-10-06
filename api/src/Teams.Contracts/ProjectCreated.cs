namespace Teams.Contracts;

public record ProjectCreated(
    Guid ProjectId,
    string Name,
    string Description,
    string Type,
    string Status,
    Guid OwnerId
);