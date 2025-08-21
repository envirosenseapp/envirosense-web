namespace EnviroSense.Web.ViewModels.Devices;

public class HourlyMeasurementViewModel
{
    public int Hour { get; set; }
    public float? AvgTemperature { get; set; }
    public float? AvgHumidity { get; set; }
}
