using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dto;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public sealed record ListUsersQuery : PagedRequest, IRequest<PagedResult<GetUser>>;

public class ListUsersQueryHandler(ServerDbContext dbContext) : IRequestHandler<ListUsersQuery, PagedResult<GetUser>>
{
    public async Task<PagedResult<GetUser>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        var queryable = dbContext.Users
            .OrderBy(x=> x.Username);

        var totalCount = await queryable.LongCountAsync(cancellationToken);

        var users = await queryable
            .Select(user => new GetUser
            {
                Username = user.Username,
                IsActive = user.IsActive
            })
            .Skip(request.SkipCount)
            .Take(request.MaxResultCount)
            .ToArrayAsync(cancellationToken);

        return new PagedResult<GetUser>(totalCount, users);
    }
}