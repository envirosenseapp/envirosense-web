namespace EnviroSense.Web.ViewModels.ApiKeys;

public class ApiKeyViewModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime? DisabledAt { get; set; }
}
