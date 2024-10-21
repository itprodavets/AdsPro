namespace Application.Dto;

public abstract record PagedRequest
{
    public int SkipCount { get; set; }
    public int MaxResultCount { get; set; } = 100;
}