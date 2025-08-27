using EnviroSense.Application.MeasurementsAggregation;
using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Services;

public interface IMeasurementService
{
    Task<Measurement> Get(Guid id);
    Task<Measurement> Create(Measurement measurement);
    Task<List<Measurement>> List(Guid deviceId);
    Task<Measurement?> GetLastest(Guid deviceId);
    Task<List<HourlyMeasurement>> ListDataForGraph(DateTime date, Device device);
}
