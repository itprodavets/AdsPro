using System.Collections.Generic;

namespace Application.Dto;

public sealed record PagedResult<T>(long Total, IReadOnlyCollection<T> Items);