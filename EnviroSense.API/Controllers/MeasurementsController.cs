using System.ComponentModel.DataAnnotations;
using EnviroSense.API.Filters;
using EnviroSense.API.Models;
using EnviroSense.API.Models.Core;
using EnviroSense.API.Models.Filters;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Domain.Filters;
using Microsoft.AspNetCore.Mvc;
using Entities = EnviroSense.Domain.Entities;

namespace EnviroSense.API.Controllers;

[TypeFilter(typeof(ApiKeyProtected))]
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
        [FromQuery]
        MeasurementQueryFilters queryFilters
    )
    {
        if (!ModelState.IsValid)
        {
            return ValidationError();
        }

        var measurements = await _measurementService.List(MapFilters(queryFilters));
        var result = measurements.ToPagedApiResult(MapModel);

        return Success(result);
    }

    [HttpPost()]
    public async Task<IActionResult> Create(
        [FromBody][Required] CreateMeasurement model
    )
    {
        if (!ModelState.IsValid)
        {
            return ValidationError();
        }

        try
        {
            var device = await _deviceService.Get(model.DeviceId);
            var measurement = await _measurementService.Create(new Entities.Measurement
            {
                Device = device,
                Temperature = model.Temperature,
                Humidity = model.Humidity,
                RecordingDate = model.RecordingDate,
            });

            return Success(MapModel(measurement));
        }
        catch (DeviceNotFoundException)
        {
            return CustomValidationError(
                new BaseError.Entry("deviceId", "not found")
                );
        }
        catch (MeasurementNotFoundException)
        {
            return NotFound();
        }
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(
        [FromRoute][Required] Guid id
    )
    {
        if (!ModelState.IsValid)
        {
            return ValidationError();
        }

        try
        {
            var measurement = await _measurementService.Get(id);

            return Success(MapModel(measurement));
        }
        catch (MeasurementNotFoundException)
        {
            return NotFound();
        }
    }

    private static Measurement MapModel(Entities.Measurement source)
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

    private static MeasurementFilters MapFilters(MeasurementQueryFilters queryFilters)
    {
        return new MeasurementFilters
        {
            DeviceId = queryFilters.DeviceId!.Value,
            PageIndex = queryFilters.PageIndex,
            PageSize = queryFilters.PageSize,
        };
    }
}
