namespace Domain;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Login { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = false;

    private User()
    {
    }

    public User(Guid id, string login, string passwordHash)
    {
        Id = id;
        Login = login ?? throw new ArgumentNullException(nameof(login));
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
    }

    public User SetActive(bool isActive)
    {
        IsActive = isActive;
        return this;
    }

}