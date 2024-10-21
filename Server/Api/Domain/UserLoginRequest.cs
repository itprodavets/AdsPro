namespace Api.Domain;

public sealed record UserLoginRequest
{
    public required string Login { get; set; }
    public required string Password { get; set; }
}