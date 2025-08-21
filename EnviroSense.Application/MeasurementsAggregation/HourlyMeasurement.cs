namespace EnviroSense.Application.MeasurementsAggregation;

public class HourlyMeasurement
{
    public int Hour { get; set; }
    public float? AvgTemperature { get; set; }
    public float? AvgHumidity { get; set; }
}
