using System;

namespace Infrastructure.Services;

public interface ITokenService
{
    string GenerateToken(Guid userId, string login);
}