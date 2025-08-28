using EnviroSense.Application.MeasurementsAggregation;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Filters;
using EnviroSense.Repositories.Core;

namespace EnviroSense.Application.Services;

public interface IMeasurementService
{
    Task<Measurement> Get(Guid id);
    Task<Measurement> Create(Measurement measurement);
    Task<PagedList<Measurement>> List(MeasurementFilters filters);
    Task<Measurement?> GetLastest(Guid deviceId);
    Task<List<HourlyMeasurement>> ListDataForGraph(DateTime date, Device device);
}
