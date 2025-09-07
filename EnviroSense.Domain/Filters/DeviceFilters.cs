using EnviroSense.Domain.Filters.Core;

namespace EnviroSense.Domain.Filters;

public class DeviceFiltres : PagedFilter
{
    public required Guid AccountId { get; set; }
}
