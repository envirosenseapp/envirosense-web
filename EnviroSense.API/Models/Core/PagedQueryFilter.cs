using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EnviroSense.API.Models.Core;

public class PagedQueryFilter
{

    [FromQuery]
    [Range(0, int.MaxValue)]
    public int PageIndex { get; set; } = 0;

    [FromQuery]
    [Range(0, 100)]
    public int PageSize { get; set; } = 20;
}
