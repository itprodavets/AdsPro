namespace Api.Domain;

public sealed record UserLoginResponse
{
    public required string Token { get; set; }
}