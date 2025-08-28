using System.ComponentModel.DataAnnotations;
using EnviroSense.API.Models.Core;
using Microsoft.AspNetCore.Mvc;

namespace EnviroSense.API.Models.Filters;

public class MeasurementFilters : PagedFilter
{
    [FromQuery]
    [Required]
    public Guid DeviceId { get; set; }
}
