namespace EnviroSense.API.Models;

public class CreateMeasurement
{
    public Guid DeviceId { get; set; }
    public float? Temperature { get; set; }
    public float? Humidity { get; set; }
    public DateTime RecordingDate { get; set; }
}
