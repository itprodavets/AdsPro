namespace Domain;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Username { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = false;

    private User()
    {
    }

    public User(Guid id, string username, string password)
    {
        Id = id;
        Username = username ?? throw new ArgumentNullException(nameof(username));
        Password = password ?? throw new ArgumentNullException(nameof(password));
    }

    public User SetActive(bool isActive)
    {
        IsActive = isActive;
        return this;
    }

}