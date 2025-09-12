namespace EnviroSense.Web.ViewModels.ApiKeys;
using EnviroSense.Web.ViewModels.Pagination;

public class ApiKeyViewModel : PagedViewModel<ApiKeyListViewModel>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime? DisabledAt { get; set; }
}
