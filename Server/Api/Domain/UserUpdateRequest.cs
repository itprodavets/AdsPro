namespace Api.Domain;

public sealed record UserUpdateRequest
{
    public required string Login { get; set; }
    public required bool IsActive { get; set; }
}