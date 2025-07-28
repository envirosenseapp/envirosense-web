using System.ComponentModel.DataAnnotations;

namespace EnviroSense.Web.ViewModels.Devices;

public class MeasurementViewModel
{
    public Guid Id { get; set; }
    [Required] public Guid DeviceId { get; set; }

    [Range(-80, 50)] public float? Temperature { get; set; }

    [Range(0, 180)] public float? Humidity { get; set; }
    [Required] public DateTime RecordingDate { get; set; }
}
