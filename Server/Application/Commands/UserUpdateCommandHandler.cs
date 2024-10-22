using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Commands;

public sealed record UserUpdateCommand : IRequest<bool>
{
    public required string Login { get; init; }
    public required bool IsActive { get; init; }
}

public class UserUpdateCommandHandler(ServerDbContext dbContext, IDistributedCache cache)
    : IRequestHandler<UserUpdateCommand, bool>
{
    public async Task<bool> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Login == request.Login, cancellationToken);
        if (user is null)
            return false;

        user.SetActive(request.IsActive);
        await dbContext.SaveChangesAsync(cancellationToken);
        var cacheKey = $"UserInfo#{user.Id}";
        await cache.SetAsync(
            cacheKey,
            JsonSerializer.SerializeToUtf8Bytes(new UserInfo(user.IsActive)),
            cancellationToken
        );

        return true;
    }
}