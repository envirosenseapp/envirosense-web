using EnviroSense.Domain.Entities;

namespace EnviroSense.Repositories.Repositories;

public interface IDeviceApiKeyRepository
{
    public Task<DeviceApiKey> GetByIdAsync(Guid deviceId);
    public Task<DeviceApiKey> CreateAsync(DeviceApiKey apiKey);
}
