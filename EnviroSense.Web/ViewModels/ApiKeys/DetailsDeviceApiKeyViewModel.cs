namespace EnviroSense.Web.ViewModels.ApiKeys;

public class DetailsDeviceApiKeyViewModel
{
    public string KeyName { get; set; }
    public Guid Id { get; set; }
    public string? Owner { get; set; }
    public string? RevealedKey { get; set; }
    public DateTime? DisabledAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
