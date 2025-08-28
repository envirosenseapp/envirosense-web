using EnviroSense.Application.Authorization;
using EnviroSense.Application.MeasurementsAggregation;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Domain.Filters;
using EnviroSense.Repositories.Core;
using EnviroSense.Repositories.Repositories;

namespace EnviroSense.Application.Services;

public class MeasurementService : IMeasurementService
{
    private readonly IMeasurementRepository _measureRepository;
    private readonly IAuthorizationResolver _authorizationResolver;

    public MeasurementService(
        IMeasurementRepository measurementRepository,
        IAuthorizationResolver authorizationResolver
    )
    {
        _measureRepository = measurementRepository;
        _authorizationResolver = authorizationResolver;
    }

    public async Task<Measurement> Get(Guid id)
    {
        var measurement = await _measureRepository.GetAsync(id);
        await _authorizationResolver.MustHaveAccess(measurement);

        return measurement;
    }

    public async Task<Measurement> Create(Measurement measurement)
    {
        await _authorizationResolver.MustHaveAccess(measurement.Device);

        return await _measureRepository.CreateAsync(measurement);
    }

    public async Task<PagedList<Measurement>> List(MeasurementFilters filters)
    {
        return await _measureRepository.ListAsync(filters);
    }

    public async Task<Measurement?> GetLastest(Guid deviceId)
    {
        var record = await _measureRepository.GetLastestAsync(deviceId);
        if (record == null)
        {
            return null;
        }

        await _authorizationResolver.MustHaveAccess(record);
        return record;
    }

    public async Task<List<HourlyMeasurement>> ListDataForGraph(DateTime date, Device device)
    {
        await _authorizationResolver.MustHaveAccess(device);
        var measurementsList = await _measureRepository.TakeMeasurementsForGivenDay(device.Id, date);
        if (measurementsList == null || !measurementsList.Any())
        {
            throw new MeasurementsForThisDayNotFoundException();
        }
        var hourlyAverage = measurementsList
            .GroupBy(m => m.RecordingDate.Hour)
            .Select(g => new HourlyMeasurement
            {
                Hour = g.Key,
                AvgTemperature = g.Average(m => m.Temperature),
                AvgHumidity = g.Average(m => m.Humidity)
            })
            .OrderBy(g => g.Hour)
            .ToList();
        return hourlyAverage;
    }
}
