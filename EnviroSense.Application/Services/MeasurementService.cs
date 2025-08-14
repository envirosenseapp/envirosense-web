using EnviroSense.Application.Authorization;
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
}
