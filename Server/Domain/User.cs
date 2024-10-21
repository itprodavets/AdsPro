namespace Domain;

public sealed record User(string Login, string PasswordHash)
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Login { get; set; } = Login;
    public string PasswordHash { get; set; } = PasswordHash;
    public bool IsActive { get; set; }
}