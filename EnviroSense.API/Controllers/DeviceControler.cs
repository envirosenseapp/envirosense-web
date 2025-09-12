using EnviroSense.API.Filters;
using EnviroSense.API.Models;
using EnviroSense.API.Models.Core;
using EnviroSense.API.Models.Filters;
using EnviroSense.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Entities = EnviroSense.Domain.Entities;

namespace EnviroSense.API.Controllers;
[TypeFilter(typeof(ApiKeyProtected))]
[Route("/Devices")]
public class DeviceControler : BaseController
{
    IDeviceService _deviceService;

    public DeviceControler(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }
    [HttpGet]
    public async Task<IActionResult> List([FromQuery]
        DeviceQueryFilters queryFilters)
    {
        if (!ModelState.IsValid)
        {
            return ValidationError();
        }

        var devices = await _deviceService.List(queryFilters.PageIndex);
        var result = devices.ToPagedApiResult(MapModel);

        return Success(result);
    }

    private static Devices MapModel(Entities.Device source)
    {
        return new Devices
        {
            Id = source.Id,
            Name = source.Name,
            CreatedAt = source.CreatedAt,
            UpdatedAt = source.UpdatedAt,

        };
    }

}
