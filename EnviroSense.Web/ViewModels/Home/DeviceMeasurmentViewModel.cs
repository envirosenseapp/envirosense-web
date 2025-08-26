
namespace EnviroSense.Web.ViewModels.Home;

public class DeviceMeasurmentViewModel
{
    public Guid DeviceId { get; set; }
    public required string DeviceName { get; set; }
    public float? Temperature { get; set; }
    public float? Humidity { get; set; }
    public DateTime? RecordingDate { get; set; }
}
