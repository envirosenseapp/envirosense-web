using EnviroSense.Application.Algorithms;
using EnviroSense.Application.Authentication;
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
    private readonly IAccountService _accountService;
    private readonly IAuthenticationContext _authenticationContext;

    public ApiKeyService(
        IApiKeyRepository apiKeyRepository,
        IApiKeyGenerator apiKeyGenerator,
        IAuthorizationResolver authorizationResolver,
        ILogger<ApiKeyService> logger,
        IAccountService accountService,
        IAuthenticationContext authenticationContext
    )
    {
        _apiKeyRepository = apiKeyRepository;
        _apiKeyGenerator = apiKeyGenerator;
        _authorizationResolver = authorizationResolver;
        _logger = logger;
        _accountService = accountService;
        _authenticationContext = authenticationContext;

    }

    public async Task<List<ApiKey>> List()
    {
        var accountId = await _authenticationContext.CurrentAccountId();
        return await _apiKeyRepository.ListAsync(accountId.Value);
    }

    public async Task<ApiKey> GetByIdAsync(Guid id)
    {
        var apiKey = await _apiKeyRepository.GetByIdAsync(id);
        await _authorizationResolver.MustHaveAccess(apiKey);

        return apiKey;
    }

    public async Task<ApiKey> GetByRawAPIKey(string rawKey)
    {
        var hash = _apiKeyGenerator.Hash(rawKey);

        return await _apiKeyRepository.GetByHashAsync(hash);
    }

    public async Task<(ApiKey key, string revealedApiKey)> CreateAsync(string name)
    {
        var account = await _authenticationContext.CurrentAccount();
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
        _logger.LogInformation($"Successfully created api key {apiKey.Id} for {account.Email}");

        return (apiKey, key);
    }
}
