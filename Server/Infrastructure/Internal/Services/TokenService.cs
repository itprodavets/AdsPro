using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastructure.Security;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Internal.Services;

internal class TokenService : ITokenService
{
    private readonly string _audience;
    private readonly string _issuer;
    private readonly SymmetricSecurityKey _key;
    private readonly double _tokenLifetime;

    public TokenService(IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey =
            jwtSettings["SecretKey"]
            ?? throw new ArgumentNullException("SecretKey", "SecretKey not found in configuration");
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        _issuer =
            jwtSettings["Issuer"] ?? throw new ArgumentNullException("Issuer", "Issuer not found in configuration");
        _audience = jwtSettings["Audience"] ?? throw new ArgumentNullException("Audience not found in configuration");
        _tokenLifetime = int.TryParse(jwtSettings["LifetimeMinutes"], out var lifetime) ? lifetime : 15;
    }

    public string GenerateToken(Guid userId, string username)
    {
        var claims = new[]
        {
            new Claim(AuthClaimTypes.UserId, userId.ToString()),
            new Claim(AuthClaimTypes.Name, username),
            new Claim(
                AuthClaimTypes.Expiration,
                DateTime.UtcNow.AddMinutes(_tokenLifetime).ToString(CultureInfo.InvariantCulture)
            )
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.Now.AddHours(_tokenLifetime),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}