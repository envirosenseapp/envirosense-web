namespace EnviroSense.Web.ViewModels.Pagination;

public class PagedViewModel
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 20;
    public int TotalPages { get; set; } = 0;
}
