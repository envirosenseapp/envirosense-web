namespace EnviroSense.Web.ViewModels.DeviceApiKeys;

public class DetailsDeviceApiKeyViewModel
{
    public string? DeviceName { get; set; }
    public Guid DeviceId { get; set; }

    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? RevealedKey { get; set; }
    public DateTime? DisabledAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
