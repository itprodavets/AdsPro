using System.Linq;
using System.Text;
using System.Text.Json;
using Infrastructure.Data;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Extensions;

public static class AuthExtensions
{
    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"] ?? string.Empty;

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"]
                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        if (context.Principal is null)
                        {
                            context.Fail("No Principal Found");
                            return;
                        }

                        var cache = context.HttpContext.RequestServices.GetRequiredService<IDistributedCache>();
                        var userId = context.Principal.Claims.Single(x => x.Type == AuthClaimTypes.UserId).Value;
                        var cacheKey = $"UserInfo#{userId}";
                        var userInfoString = await cache.GetStringAsync(cacheKey);
                        if (userInfoString is null)
                        {
                            context.Fail("User not found");
                            return;
                        }

                        var userInfo = JsonSerializer.Deserialize<UserInfo>(userInfoString);
                        if (userInfo is null)
                        {
                            context.Fail("User not found");
                            return;
                        }

                        if (!userInfo.IsActive)
                            context.Fail("User inactive");
                    }
                };
            });

        return services;
    }
}