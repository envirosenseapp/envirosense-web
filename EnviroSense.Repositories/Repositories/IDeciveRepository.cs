using EnviroSense.Domain.Entities;

namespace EnviroSense.Repositories.Repositories;

public interface IDeciveRepository
{
    Task<List<Device>> ListAsync(Guid accountId);
    Task<Device?> GetAsync(Guid Id);
    Task<Device> CreateAsync(Device device);
}
