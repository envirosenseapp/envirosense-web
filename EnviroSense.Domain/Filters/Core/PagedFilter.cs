namespace EnviroSense.Domain.Filters.Core;

public class PagedFilter
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 20;
}
