namespace EnviroSense.Web.ViewModels.ApiKeys;

public class ApiKeysViewModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime? DisabledAt { get; set; }
}
