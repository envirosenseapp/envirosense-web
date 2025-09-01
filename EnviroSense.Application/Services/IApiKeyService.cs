using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Services;

public interface IApiKeyService
{
    public Task<List<ApiKey>> List(Guid accountId);
    public Task<ApiKey> GetByIdAsync(Guid Id);
    public Task<ApiKey> GetByRawAPIKey(string rawKey);
    public Task<(ApiKey key, string revealedApiKey)> CreateAsync(string name, Account account);
}
