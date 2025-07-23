using System;

namespace EnviroSense.Web.ViewModels.Devices;

public class MeasurementViewModel
{
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public string? Temperature { get; set; }
    public string? Humidity { get; set; }
    public DateTime RecordingDate { get; set; }
    public DateTime RecordingCreatedAt { get; set; }

}
