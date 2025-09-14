using EnviroSense.Domain.Filters.Core;

namespace EnviroSense.Domain.Filters;

public class DeviceFilters : PagedFilter
{
    public Guid? AccountId { get; set; }
}
