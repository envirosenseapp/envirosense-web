using EnviroSense.Application.Algorithms;
using EnviroSense.Application.Authorization;
using EnviroSense.Domain.Entities;
using EnviroSense.Repositories.Repositories;
using Microsoft.Extensions.Logging;

namespace EnviroSense.Application.Services;

public class ApiKeyService : IApiKeyService
{
    private readonly IApiKeyRepository _apiKeyRepository;
    private readonly IApiKeyGenerator _apiKeyGenerator;
    private readonly IAuthorizationResolver _authorizationResolver;
    private readonly ILogger<ApiKeyService> _logger;

    public ApiKeyService(
        IApiKeyRepository apiKeyRepository,
        IApiKeyGenerator apiKeyGenerator,
        IAuthorizationResolver authorizationResolver,
        ILogger<ApiKeyService> logger
    )
    {
        _apiKeyRepository = apiKeyRepository;
        _apiKeyGenerator = apiKeyGenerator;
        _authorizationResolver = authorizationResolver;
        _logger = logger;
    }

    public async Task<List<ApiKey>> List(Guid accountId)
    {
        return await _apiKeyRepository.ListAsync(accountId);
    }

    public async Task<ApiKey> GetByIdAsync(Guid Id)
    {
        var deviceApiKey = await _apiKeyRepository.GetByIdAsync(Id);
        await _authorizationResolver.MustHaveAccess(deviceApiKey);

        return deviceApiKey;
    }

    public async Task<ApiKey> GetByRawAPIKey(string rawKey)
    {
        var hash = _apiKeyGenerator.Hash(rawKey);

        return await _apiKeyRepository.GetByHashAsync(hash);
    }

    public async Task<(ApiKey key, string revealedApiKey)> CreateAsync(string name, Account account)
    {

        var key = _apiKeyGenerator.Generate();
        var hash = _apiKeyGenerator.Hash(key);

        var apiKey = new ApiKey()
        {
            Account = account,
            KeyHash = hash,
            Id = Guid.NewGuid(),
            Name = name,
        };

        apiKey = await _apiKeyRepository.CreateAsync(apiKey);
        _logger.LogInformation($"Successfully created api key {apiKey.Id} for  {account.Email}");

        return (apiKey, key);
    }
}
