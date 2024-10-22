namespace Api.Domain;

public sealed record UserLoginRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}