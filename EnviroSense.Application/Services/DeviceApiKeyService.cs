using EnviroSense.Application.Algorithms;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;
using Microsoft.Extensions.Logging;

namespace EnviroSense.Application.Services;

public class DeviceApiKeyService : IDeviceApiKeyService
{
    private readonly IDeviceApiKeyRepository _deviceApiKeyRepository;
    private readonly IAccountService _accountService;
    private readonly IApiKeyGenerator _apiKeyGenerator;
    private readonly ILogger<DeviceApiKeyService> _logger;

    public DeviceApiKeyService(
        IDeviceApiKeyRepository deviceApiKeyRepository,
        IAccountService accountService,
        IApiKeyGenerator apiKeyGenerator,
        ILogger<DeviceApiKeyService> logger
    )
    {
        _deviceApiKeyRepository = deviceApiKeyRepository;
        _accountService = accountService;
        _apiKeyGenerator = apiKeyGenerator;
        _logger = logger;
    }

    public Task<DeviceApiKey> GetByIdAsync(Guid deviceId)
    {
        return _deviceApiKeyRepository.GetByIdAsync(deviceId);
    }

    public async Task<Tuple<DeviceApiKey, string>> CreateAsync(Device device, string name)
    {
        var loggedInUser = _accountService.GetAccountIdFromSession();
        if (loggedInUser == null)
        {
            throw new NotAuthenticatedException("Must be authenticated to perform this action");
        }

        if (loggedInUser == device.Id.ToString())
        {
            throw new AccessToForbiddenEntityException("Creating api key to foreign device is forbidden.");
        }

        var key = _apiKeyGenerator.Generate();
        var hash = _apiKeyGenerator.Hash(key);

        var apiKey = new DeviceApiKey()
        {
            DeviceId = device.Id,
            KeyHash = hash,
            Id = Guid.NewGuid(),
            Name = name,
        };

        apiKey = await _deviceApiKeyRepository.CreateAsync(apiKey);
        _logger.LogInformation($"Successfully created api key {apiKey.Id} for device {device.Id}");

        return Tuple.Create(apiKey, hash);
    }
}
