using System;
using System.Linq;
using Domain;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static void InitializeDatabase(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ServerDbContext>();

        dbContext.Database.Migrate();
        var passwordService = serviceProvider.GetRequiredService<IPasswordService>();
        SeedData(dbContext, passwordService);
    }

    private static void SeedData(ServerDbContext dbContext, IPasswordService passwordService)
    {
        if (dbContext.Users.Any())
            return;

        dbContext.Users.AddRange(
            Enumerable.Range(0, 20).Select(i => new User(
                Guid.NewGuid(),
                $"user{i}",
                passwordService.HashPassword($"password{i}")
                ).SetActive(true)
            )
        );
        dbContext.SaveChanges();
    }
}