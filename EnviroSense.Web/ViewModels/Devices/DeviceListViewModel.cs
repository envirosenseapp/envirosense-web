namespace EnviroSense.Web.ViewModels.Devices;

public class DeviceListViewModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
