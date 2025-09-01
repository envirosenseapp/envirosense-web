using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Services;

public interface IApiKeyService
{
    public Task<List<ApiKey>> List();
    public Task<ApiKey> GetByIdAsync(Guid id);
    public Task<ApiKey> GetByRawAPIKey(string rawKey);
    public Task<(ApiKey key, string revealedApiKey)> CreateAsync(string name);
}
