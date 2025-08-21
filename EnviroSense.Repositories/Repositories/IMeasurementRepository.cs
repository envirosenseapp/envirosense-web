using EnviroSense.Domain.Entities;

namespace EnviroSense.Repositories.Repositories;

public interface IMeasurementRepository
{
    Task<Measurement> CreateAsync(Measurement measurement);
    Task<List<Measurement>> ListAsync(Guid deviceId);
    Task<Measurement?> GetLastestAsync(Guid deviceId);
    
    Task<List<Measurement?>> TakeMeasurementsForGivenDay(Guid deviceId, DateTime date);
}
