using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Filters;
using EnviroSense.Repositories.Core;

namespace EnviroSense.Repositories.Repositories;

public interface IMeasurementRepository
{
    Task<Measurement> CreateAsync(Measurement measurement);
    Task<Measurement> GetAsync(Guid id);
    Task<PagedList<Measurement>> ListAsync(MeasurementFilters filters);
    Task<Measurement?> GetLastestAsync(Guid deviceId);
    Task<List<Measurement>> TakeMeasurementsForGivenDay(Guid deviceId, DateTime date);
}
