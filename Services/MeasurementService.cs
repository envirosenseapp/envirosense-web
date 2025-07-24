using System;
using EnviroSense.Web.Entities;
using EnviroSense.Web.Repositories;
using EnviroSense.Web.Exceptions;

namespace EnviroSense.Web.Services;

public class MeasurementService : IMeasurementService
{
    private readonly IMeasurementRepository _measureRepository;
    private readonly IDeciveRepository _deciveRepository;
    public MeasurementService(IMeasurementRepository measurementRepository, IDeciveRepository deciveRepository)
    {
        _measureRepository = measurementRepository;
        _deciveRepository = deciveRepository;
    }
    public async Task<Measurement?> Create(DateTime recordingDate, float temperature, float humidity, Guid deviceID)
    {
        var device = await _deciveRepository.GetAsync(deviceID);
        if (device == null)
        {
            throw new DeviceNotFound();
        }
        var measurement = new Measurement
        {
            RecordingDate = recordingDate,
            Temperature = temperature,
            Humidity = humidity,
            Device = device

        };

        return await _measureRepository.CreateAsync(measurement);
    }
    public async Task<List<Measurement>> List(Guid deviceId)
    {
        return await _measureRepository.ListAsync(deviceId);
    }
}
