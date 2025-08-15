using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Services;

public interface IDeviceApiKeyService
{
    public Task<DeviceApiKey> GetByIdAsync(Guid deviceId);

    public Task<(DeviceApiKey key, string revealedApiKey)> CreateAsync(Device device, string name);
}
