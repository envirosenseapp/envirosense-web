using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Services;

public interface IApiKeyService
{
    public Task<List<ApiKey>> List(Guid id);
    public Task<ApiKey> GetByIdAsync(Guid apiId);
    public Task<ApiKey> GetByRawAPIKey(string rawKey);
    public Task<(ApiKey key, string revealedApiKey)> CreateAsync(string name, Account account);
}
