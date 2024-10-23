using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Data;
using Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Commands;

public sealed record UserLoginCommand : IRequest<UserLoginResult>
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}

public sealed record UserLoginResult
{
    public required string Token { get; init; }
}

public class UserLoginCommandHandler(
    ServerDbContext dbContext,
    IPasswordService passwordService,
    ITokenService tokenService,
    IDistributedCache cache
) : IRequestHandler<UserLoginCommand, UserLoginResult>
{
    public async Task<UserLoginResult> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);

        if (user is null)
            throw new UnauthorizedAccessException("Неверный логин или пароль.");

        if(!user.IsActive)
            throw new UnauthorizedAccessException("Неверный логин или пароль.");

        var isPasswordValid = passwordService.VerifyPassword(request.Password, user.Password);
        if (!isPasswordValid)
            throw new UnauthorizedAccessException("Неверный логин или пароль.");

        var token = tokenService.GenerateToken(user.Id, user.Username);

        var cacheKey = $"UserInfo#{user.Id}";
        await cache.SetAsync(
            cacheKey,
            JsonSerializer.SerializeToUtf8Bytes(new UserInfo(user.IsActive)),
            cancellationToken
        );

        return new UserLoginResult { Token = token };
    }
}