using EnviroSense.Web.ViewModels.Pagination;
namespace EnviroSense.Web.ViewModels.Devices;

public class DeviceViewModel : PagedViewModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<DeviceListViewModel> Devices { get; set; } = new List<DeviceListViewModel>();
}
