using System.ComponentModel.DataAnnotations;

namespace EnviroSense.Web.ViewModels.Devices;

public class MeasurementsListViewModel
{
    public Guid Id { get; set; }
    public string? DeviceName { get; set; }
    public float? Temperature { get; set; }
    public float? Humidity { get; set; }
    public DateTime RecordingDate { get; set; }
}
