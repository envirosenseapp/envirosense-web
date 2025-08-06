using EnviroSense.Web.Entities;

namespace EnviroSense.Web.Repositories;

public interface IMeasurementRepository
{
    Task<Measurement> CreateAsync(Measurement measurement);
    Task<List<Measurement>> ListAsync(Guid deviceId);
}
