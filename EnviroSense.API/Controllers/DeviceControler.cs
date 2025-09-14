using EnviroSense.API.Filters;
using EnviroSense.API.Models;
using EnviroSense.API.Models.Core;
using EnviroSense.API.Models.Filters;
using EnviroSense.Application.Authentication;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Filters;
using Microsoft.AspNetCore.Mvc;
using Entities = EnviroSense.Domain.Entities;

namespace EnviroSense.API.Controllers;
[TypeFilter(typeof(ApiKeyProtected))]
[Route("/Devices")]
public class DeviceControler : BaseController
{
    IDeviceService _deviceService;
    IAuthenticationContext _authContext;

    public DeviceControler(IDeviceService deviceService, IAuthenticationContext authContext)
    {
        _deviceService = deviceService;
        _authContext = authContext;
    }
    [HttpGet]
    public async Task<IActionResult> List([FromQuery]
        DeviceQueryFilters queryFilters)
    {
        if (!ModelState.IsValid)
        {
            return ValidationError();
        }
        var accountId = await _authContext.CurrentAccountId();
        if (accountId == null)
        {
            throw new UnauthorizedAccessException();
        }
        var devices = await _deviceService.List(MapFilters(queryFilters, accountId));
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

    private static DeviceFilters MapFilters(DeviceQueryFilters queryFilters, Guid? accountId)
    {
        return new DeviceFilters()
        {
            AccountId = accountId,
            PageIndex = queryFilters.PageIndex,
            PageSize = queryFilters.PageSize,
        };
    }

}
