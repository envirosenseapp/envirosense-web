using EnviroSense.Domain.Filters.Core;

namespace EnviroSense.Domain.Filters;

public class ApiKeyFilters : PagedFilter
{
    public required Guid AccountId { get; set; }
}
