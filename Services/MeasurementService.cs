using System;
using EnviroSense.Web.Entities;
using EnviroSense.Web.Repositories;

namespace EnviroSense.Web.Services;

public class MeasurementService : IMeasurementService
{
    private readonly IMeasurementRepository _measureRepository;
    public MeasurementService(IMeasurementRepository measurementRepository)
    {
        _measureRepository = measurementRepository;
    }
    public async Task<Measurement> Create(DateTime recordingDate, string temperature, string humidity, Guid deviceID)
    {
        var measurement = new Measurement
        {
            RecordingDate = recordingDate,
            Temperature = temperature,
            Humidity = humidity,
            DeviceId = deviceID
        };

        return await _measureRepository.CreateAsync(measurement);
    }
    public async Task<List<Measurement>> List(Guid deviceId)
    {
        return await _measureRepository.ListAsync(deviceId);
    }
}
