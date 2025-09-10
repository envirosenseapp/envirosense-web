using EnviroSense.Repositories.Core;

namespace EnviroSense.Web.ViewModels.Pagination;

public class PagedViewModel<T>
{
    public void CopyPaginationParamsFrom<T1>(PagedList<T1> list)
    {
        PageIndex = list.PageIndex;
        PageSize = list.PageSize;
        TotalPages = list.TotalPages;
    }
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 20;
    public int TotalPages { get; set; } = 0;
    public List<T> Items { get; set; } = new List<T>();

}
