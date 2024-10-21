using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Data;
using Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public sealed record UserLoginCommand : IRequest<UserLoginResult>
{
    public required string Login { get; init; }
    public required string Password { get; init; }
}

public sealed record UserLoginResult
{
    public required string Token { get; init; }
}

public class UserLoginCommandHandler(
    ServerDbContext dbContext,
    IPasswordService passwordService,
    ITokenService tokenService
) : IRequestHandler<UserLoginCommand, UserLoginResult>
{
    public async Task<UserLoginResult> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Login == request.Login, cancellationToken);
        if (user is null)
            throw new UnauthorizedAccessException("Неверный логин или пароль.");

        var isPasswordValid = passwordService.VerifyPassword(request.Password, user.PasswordHash);
        if (!isPasswordValid)
            throw new UnauthorizedAccessException("Неверный логин или пароль.");

        var token = tokenService.GenerateToken(user.Id, user.Login);

        return new UserLoginResult { Token = token };
    }
}