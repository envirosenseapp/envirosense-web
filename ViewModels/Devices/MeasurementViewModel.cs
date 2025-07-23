using System;

namespace EnviroSense.Web.ViewModels.Devices;

public class MeasurementViewModel
{
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public float? Temperature { get; set; }
    public float? Humidity { get; set; }
    public DateTime RecordingDate { get; set; }

}
