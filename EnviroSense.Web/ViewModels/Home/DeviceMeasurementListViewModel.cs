
namespace EnviroSense.Web.ViewModels.Home;

public class DeviceMeasurementListViewModel
{
    public Guid DeviceId { get; set; }
    public string? DeviceName { get; set; }
    public float? Temperature { get; set; }
    public float? Humidity { get; set; }
    public DateTime? RecordingDate { get; set; }
}
