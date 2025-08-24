using EnviroSense.Application.Authorization;
using EnviroSense.Application.MeasurementsAggregation;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;

namespace EnviroSense.Application.Services;

public class MeasurementService : IMeasurementService
{
    private readonly IMeasurementRepository _measureRepository;
    private readonly IDeviceRepository _deviceRepository;
    private readonly IAuthorizationResolver _authorizationResolver;

    public MeasurementService(
        IMeasurementRepository measurementRepository,
        IDeviceRepository deviceRepository,
        IAuthorizationResolver authorizationResolver
    )
    {
        _measureRepository = measurementRepository;
        _deviceRepository = deviceRepository;
        _authorizationResolver = authorizationResolver;
    }

    public async Task<Device> Get(Guid deviceId)
    {
        var device = await _deviceRepository.GetAsync(deviceId);
        await _authorizationResolver.MustHaveAccess(device);

        return device;
    }

    public async Task<Measurement> Create(Measurement measurement)
    {
        await _authorizationResolver.MustHaveAccess(measurement.Device);

        return await _measureRepository.CreateAsync(measurement);
    }

    public async Task<List<Measurement>> List(Guid deviceId)
    {
        return await _measureRepository.ListAsync(deviceId);
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

    public async Task<List<HourlyMeasurement>> ListDataForGraph(Guid deviceId, DateTime date, Device device)
    {
        await _authorizationResolver.MustHaveAccess(device);
        var measurementsList = await _measureRepository.TakeMeasurementsForGivenDay(deviceId, date);
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
