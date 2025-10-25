namespace Identity.API.Dtos;

public sealed class RegistrationResponse
{
    public required bool Success { get; init; }
    public required string Message { get; init; }
    public string? Token { get; init; }
}