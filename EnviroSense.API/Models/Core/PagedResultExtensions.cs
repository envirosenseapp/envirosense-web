using EnviroSense.Repositories.Core;

namespace EnviroSense.API.Models.Core;

public static class PagedResultExtensions
{
    public static PagedApiResult<TDst> ToPagedApiResult<TSrc, TDst>(this PagedList<TSrc> source, Func<TSrc, TDst> mapper)
    {
        return new PagedApiResult<TDst>(
            source.Records.Select(mapper),
            source.PageSize,
            source.PageIndex,
            source.TotalCount,
            source.TotalPages
        );
    }
}
