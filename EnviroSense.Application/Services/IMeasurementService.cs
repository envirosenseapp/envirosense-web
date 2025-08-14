using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Services;

public interface IMeasurementService
{
    Task<Device> Get(Guid deviceId);
    Task<Measurement> Create(Measurement measurement);
    Task<List<Measurement>> List(Guid deviceId);
    Task<Measurement?> GetLastest(Guid deviceId);
}
