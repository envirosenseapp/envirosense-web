using System.ComponentModel.DataAnnotations;
using EnviroSense.API.Models;
using EnviroSense.API.Models.Core;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Entities = EnviroSense.Domain.Entities;

namespace EnviroSense.API.Controllers;

[Route("/measurements")]
public class MeasurementsController : BaseController
{
    private readonly IMeasurementService _measurementService;
    private readonly IDeviceService _deviceService;

    public MeasurementsController(IMeasurementService measurementService, IDeviceService deviceService)
    {
        _measurementService = measurementService;
        _deviceService = deviceService;
    }

    [HttpGet]
    public async Task<IActionResult> List(
        [FromQuery] [Required] Guid deviceId
    )
    {
        if (!ModelState.IsValid)
        {
            return ValidationError();
        }

        var measurements = await _measurementService.List(deviceId);
        var result = new PagedResult<Measurement>(
            measurements.Select(ToModel)
        );

        return Success(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(
        [FromRoute] [Required] Guid id
    )
    {
        if (!ModelState.IsValid)
        {
            return ValidationError();
        }

        try
        {
            var measurement = await _measurementService.Get(id);

            return Success(ToModel(measurement));
        }
        catch (MeasurementNotFoundException)
        {
            return NotFound();
        }
    }

    private static Measurement ToModel(Entities.Measurement source)
    {
        return new Measurement
        {
            Id = source.Id,
            DeviceId = source.DeviceId,
            Temperature = source.Temperature,
            Humidity = source.Humidity,
            RecordingDate = source.RecordingDate,
            CreatedAt = source.CreatedAt,
        };
    }
}
