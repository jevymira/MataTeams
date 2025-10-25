namespace Identity.API.Dtos;

public sealed record RegistrationRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}