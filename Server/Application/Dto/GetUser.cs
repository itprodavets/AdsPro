using System;

namespace Application.Dto;

public class GetUser
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Username { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
}