using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Services;

public interface IDeviceApiKeyService
{
    public Task<DeviceApiKey> GetByIdAsync(Guid deviceId);

    public Task<Tuple<DeviceApiKey, string>> CreateAsync(Device device, string name);
}
