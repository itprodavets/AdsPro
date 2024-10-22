namespace Api.Domain;

public sealed record UserUpdateRequest
{
    public required string Username { get; set; }
    public required bool IsActive { get; set; }
}