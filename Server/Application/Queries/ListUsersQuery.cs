using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dto;
using Domain;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public sealed record ListUsersQuery : PagedRequest, IRequest<PagedResult<User>>;

public class ListUsersQueryHandler(ServerDbContext dbContext) : IRequestHandler<ListUsersQuery, PagedResult<User>>
{
    public async Task<PagedResult<User>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        var queryable = dbContext.Users;

        var totalCount = await queryable.LongCountAsync(cancellationToken);

        var users = await queryable
            .Skip(request.SkipCount)
            .Take(request.MaxResultCount)
            .ToArrayAsync(cancellationToken);

        return new PagedResult<User>(totalCount, users);
    }
}