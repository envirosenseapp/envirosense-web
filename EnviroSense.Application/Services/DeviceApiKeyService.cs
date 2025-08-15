using EnviroSense.Application.Algorithms;
using EnviroSense.Application.Authorization;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;
using Microsoft.Extensions.Logging;

namespace EnviroSense.Application.Services;

public class DeviceApiKeyService : IDeviceApiKeyService
{
    private readonly IDeviceApiKeyRepository _deviceApiKeyRepository;
    private readonly IApiKeyGenerator _apiKeyGenerator;
    private readonly IAuthorizationResolver _authorizationResolver;
    private readonly ILogger<DeviceApiKeyService> _logger;

    public DeviceApiKeyService(
        IDeviceApiKeyRepository deviceApiKeyRepository,
        IApiKeyGenerator apiKeyGenerator,
        IAuthorizationResolver authorizationResolver,
        ILogger<DeviceApiKeyService> logger
    )
    {
        _deviceApiKeyRepository = deviceApiKeyRepository;
        _apiKeyGenerator = apiKeyGenerator;
        _authorizationResolver = authorizationResolver;
        _logger = logger;
    }

    public async Task<DeviceApiKey> GetByIdAsync(Guid deviceId)
    {
        var deviceApiKey = await _deviceApiKeyRepository.GetByIdAsync(deviceId);
        await _authorizationResolver.MustHaveAccess(deviceApiKey);

        return deviceApiKey;
    }

    public async Task<(DeviceApiKey key, string revealedApiKey)> CreateAsync(Device device, string name)
    {
        await _authorizationResolver.MustHaveAccess(device);

        var key = _apiKeyGenerator.Generate();
        var hash = _apiKeyGenerator.Hash(key);

        var apiKey = new DeviceApiKey()
        {
            Device = device,
            KeyHash = hash,
            Id = Guid.NewGuid(),
            Name = name,
        };

        apiKey = await _deviceApiKeyRepository.CreateAsync(apiKey);
        _logger.LogInformation($"Successfully created api key {apiKey.Id} for device {device.Id}");

        return (apiKey, hash);
    }
}
