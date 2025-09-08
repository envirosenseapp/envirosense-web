
namespace EnviroSense.Web.ViewModels.Home;

public class DeviceMeasurementViewModel
{
    public int PageIndex { get; set; } = 0;
    public int TotalPages { get; set; } = 0;
    public List<DeviceMeasurementListViewModel>? DeviceMeasurementList { get; set; }
}
