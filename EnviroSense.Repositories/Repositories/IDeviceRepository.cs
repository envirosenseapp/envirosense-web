using EnviroSense.Domain.Entities;

namespace EnviroSense.Repositories.Repositories;

public interface IDeviceRepository
{
    Task<List<Device>> ListAsync(Guid accountId);
    Task<Device> GetAsync(Guid id);
    Task<Device> CreateAsync(Device device);
}
