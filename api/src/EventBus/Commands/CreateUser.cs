namespace EventBus.Commands;

public record CreateUser
{
    public string IdentityGuid { get; init; }
}