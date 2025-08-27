using System.ComponentModel.DataAnnotations;

namespace EnviroSense.API.Models;

public class CreateMeasurement
{
    public Guid DeviceId { get; set; }

    [Range(-80, 50)]
    public float? Temperature { get; set; }

    [Range(0, 180)]
    public float? Humidity { get; set; }

    public DateTime RecordingDate { get; set; }
}
