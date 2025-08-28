using EnviroSense.Domain.Filters.Core;

namespace EnviroSense.Domain.Filters;

public class MeasurementFilters : PagedFilter
{
    public required Guid DeviceId { get; set; }
}
