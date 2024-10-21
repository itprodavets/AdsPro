using System.Security.Claims;

namespace Infrastructure.Security;

public static class AuthClaimTypes
{
    public static string UserId = nameof(UserId);
    public static string Name = ClaimTypes.Name;
    public static string Expiration = ClaimTypes.Expiration;
}