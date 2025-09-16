using System.ComponentModel.DataAnnotations;
using EnviroSense.API.Filters;
using EnviroSense.API.Models;
using EnviroSense.API.Models.Core;
using EnviroSense.API.Models.Filters;
using EnviroSense.Application.Authentication;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Domain.Filters;
using Microsoft.AspNetCore.Mvc;
using Entities = EnviroSense.Domain.Entities;

namespace EnviroSense.API.Controllers;
[TypeFilter(typeof(ApiKeyProtected))]
[Route("/Devices")]
public class DevicesControler : BaseController
{
    IDeviceService _deviceService;
    public DevicesControler(IDeviceService deviceService)
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

        var devices = await _deviceService.List(MapFilters(queryFilters));
        var result = devices.ToPagedApiResult(MapModel);

        return Success(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute][Required] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return ValidationError();
        }

        try
        {
            var device = await _deviceService.Get(id);
            return Success(MapModel(device));
        }
        catch (DeviceNotFoundException)
        {
            return NotFound();
        }

    }

    private static Device MapModel(Entities.Device source)
    {
        return new Device
        {
            Id = source.Id,
            Name = source.Name,
            CreatedAt = source.CreatedAt,
            UpdatedAt = source.UpdatedAt,

        };
    }

    private static DeviceFilters MapFilters(DeviceQueryFilters queryFilters)
    {
        return new DeviceFilters()
        {
            PageIndex = queryFilters.PageIndex,
            PageSize = queryFilters.PageSize,
        };
    }

}
