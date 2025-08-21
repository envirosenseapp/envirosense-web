namespace EnviroSense.Web.ViewModels.Devices;

public class GraphViewModel
{
    public Guid Id { get; set; }
    public string DeviceName { get; set; }
    public DateTime date { get; set; }

    public List<HourlyMeasurementViewModel> Measurements { get; set; } = new();
}
