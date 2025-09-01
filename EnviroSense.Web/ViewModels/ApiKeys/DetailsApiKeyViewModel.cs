namespace EnviroSense.Web.ViewModels.ApiKeys;

public class DetailsApiKeyViewModel
{
    public required string KeyName { get; set; }
    public Guid Id { get; set; }
    public string? RevealedKey { get; set; }
    public DateTime? DisabledAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
