using EnviroSense.API.Filters;
using EnviroSense.API.Models.Core;
using EnviroSense.API.Models.Filters;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Entities = EnviroSense.Domain.Entities;

namespace EnviroSense.API.Controllers;
[TypeFilter(typeof(ApiKeyProtected))]
[Route("/device")]
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

    private static Device MapModel(Entities.Device source)
    {
        return new Device
        {
            Id = source.Id,
            Account = source.Account,
            Name = source.Name,
            CreatedAt = source.CreatedAt,
            UpdatedAt = source.UpdatedAt,

        };
    }

}
