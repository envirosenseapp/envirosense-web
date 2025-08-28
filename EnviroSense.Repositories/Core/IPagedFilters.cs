namespace EnviroSense.Repositories.Core;

public interface IPagedFilters
{
    public int PageSize { get; }
    public int PageIndex { get; }
}
