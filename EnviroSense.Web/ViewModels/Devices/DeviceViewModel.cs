using EnviroSense.Web.ViewModels.Pagination;
namespace EnviroSense.Web.ViewModels.Devices;

public class DeviceViewModel : PagedViewModel<DeviceListViewModel>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
