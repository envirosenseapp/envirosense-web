using System;

namespace EnviroSense.Web.Entities;

public class Measurement
{
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public required Device Device { get; set; }
    public float? Temperature { get; set; }
    public float? Humidity { get; set; }
    public DateTime RecordingDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
