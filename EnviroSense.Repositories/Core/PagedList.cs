namespace EnviroSense.Repositories.Core;

public class PagedList<T>
{
    public PagedList(
        IEnumerable<T> records,
        int pageSize,
        int pageIndex,
        int totalCount,
        int totalPages
        )
    {
        Records = records;
        PageSize = pageSize;
        PageIndex = pageIndex;
        TotalCount = totalCount;
        TotalPages = totalPages;
    }

    public IEnumerable<T> Records { get; }
    public int PageSize { get; }
    public int PageIndex { get; }
    public int TotalCount { get; }
    public int TotalPages { get; }
}
