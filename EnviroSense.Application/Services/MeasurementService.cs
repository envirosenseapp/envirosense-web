using EnviroSense.Application.Authorization;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
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
