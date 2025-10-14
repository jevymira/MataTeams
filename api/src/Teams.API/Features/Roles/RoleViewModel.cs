namespace Teams.API.Features.Roles;

public sealed record RoleViewModel
{
    public required string Id { get; init; }
    
    public required string Name { get; init; }
}