using System;

namespace EnviroSense.Web.Entities;

public class Measurement
{
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public Device Device { get; set; }
    public string Temperature { get; set; }
    public string Humidity { get; set; }
    public DateTime RecordingDate { get; set; }
    public DateTime RecordingCreatedAt { get; set; }
}
