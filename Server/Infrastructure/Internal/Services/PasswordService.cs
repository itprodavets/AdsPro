using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Internal.Services;

internal class PasswordService : IPasswordService
{
    private readonly PasswordHasher<object> _passwordHasher = new();

    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(null!, password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        var result = _passwordHasher.VerifyHashedPassword(null!, passwordHash, password);
        return result == PasswordVerificationResult.Success;
    }
}