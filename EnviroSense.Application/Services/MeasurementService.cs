using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;

namespace EnviroSense.Application.Services;

public class MeasurementService : IMeasurementService
{
    private readonly IMeasurementRepository _measureRepository;
    private readonly IDeciveRepository _deciveRepository;
    public MeasurementService(IMeasurementRepository measurementRepository, IDeciveRepository deciveRepository)
    {
        _measureRepository = measurementRepository;
        _deciveRepository = deciveRepository;
    }

    public async Task<Device> Get(Guid deviceID)
    {
        var device = await _deciveRepository.GetAsync(deviceID);
        if (device == null)
        {
            throw new DeviceNotFoundException(deviceID);
        }
        return device;
    }
    public async Task<Measurement> Create(Measurement measurement)
    {
        return await _measureRepository.CreateAsync(measurement);
    }
    public async Task<List<Measurement>?> List(Guid deviceId)
    {
        return await _measureRepository.ListAsync(deviceId);
    }

    public async Task<Measurement?> GetLastest(Guid deviceId)
    {
        return await _measureRepository.GetLastestAsync(deviceId);
    }
}
