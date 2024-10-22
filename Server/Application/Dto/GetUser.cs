namespace Application.Dto;

public class GetUser
{
    public string Username { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
}