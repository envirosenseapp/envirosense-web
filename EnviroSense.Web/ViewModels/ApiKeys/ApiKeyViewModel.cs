namespace EnviroSense.Web.ViewModels.ApiKeys;

public class ApiKeyViewModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime? DisabledAt { get; set; }

    public int PageIndex { get; set; } = 0;

    public int TotalPages { get; set; } = 0;

    public List<ApiKeyListViewModel> ApiKeyList { get; set; } = new List<ApiKeyListViewModel>();
}
