using EnviroSense.API.Exceptions;
using EnviroSense.Application.Authentication;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;

namespace EnviroSense.API.Authentication;

public class APIKeyAuthentication : IAuthenticationRetriever
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDeviceApiKeyService _apiKeyService;

    public APIKeyAuthentication(IHttpContextAccessor httpContextAccessor, IDeviceApiKeyService apiKeyService)
    {
        _httpContextAccessor = httpContextAccessor;
        _apiKeyService = apiKeyService;
    }

    public async Task<Guid?> GetCurrentAccountId()
    {
        var account = await GetCurrentAccount();

        return account?.Id;
    }

    public async Task<Account?> GetCurrentAccount()
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            throw new Exception("could not get http context");
        }

        if (!_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("X-Api-Key", out var headerValues))
        {
            return null;
        }

        if (headerValues.Count != 1)
        {
            throw new InvalidApiKeyException("multiple values found, but expected one");
        }

        var key = headerValues[0] ?? throw new InvalidApiKeyException("expecting a value");

        try
        {
            var apiKey = await _apiKeyService.GetByRawAPIKey(key);

            return apiKey.Device.Account;
        }
        catch (DeviceApiKeyNotFound)
        {
            throw new InvalidApiKeyException("api key not found");
        }
    }
}

