using System.ComponentModel.DataAnnotations;
using EnviroSense.Web.ViewModels.Pagination;

namespace EnviroSense.Web.ViewModels.Devices;

public class MeasurementViewModel : PagedViewModel
{
    public Guid Id { get; set; }
    public string? DeviceName { get; set; }
    [Required] public Guid DeviceId { get; set; }

    [Range(-80, 50)] public float? Temperature { get; set; }

    [Range(0, 180)] public float? Humidity { get; set; }
    [Required] public DateTime RecordingDate { get; set; }
    public List<MeasurementsListViewModel> Measurements { get; set; } = new List<MeasurementsListViewModel>();
}
