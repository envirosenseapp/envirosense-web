namespace EnviroSense.Web.ViewModels.Devices;

public class DeviceViewModel
{
    public Guid Id { get; set; }

    public int TotalPages { get; set; } = 0;
    public int PageIndex { get; set; } = 0;
    public required string Name { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<DeviceListViewModel> Devices { get; set; } = new List<DeviceListViewModel>();
}
