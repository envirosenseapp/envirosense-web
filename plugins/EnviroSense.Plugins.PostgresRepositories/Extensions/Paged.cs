using EnviroSense.Domain.Filters.Core;
using EnviroSense.Repositories.Core;
using Microsoft.EntityFrameworkCore;

namespace EnviroSense.Plugins.PostgresRepositories.Extensions;

public static class Paged
{
    public static Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> query,
        PagedFilter filter
    )
    {
        return query.ToPagedListAsync(
            filter.PageIndex,
            filter.PageSize
        );
    }

    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> query,
        int pageIndex,
        int pageSize
        )
    {
        var count = await query.CountAsync();
        var data = await query.Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToArrayAsync();


        var totalPages = count / pageSize;
        if (count % pageSize > 0)
        {
            totalPages += 1;
        }

        return new PagedList<T>(
            data,
            pageSize,
            pageIndex,
            count,
            totalPages
            );
    }
}
