using EnviroSense.Domain.Entities;
using EnviroSense.Repositories.Core;

namespace EnviroSense.Application.Services;

public interface IApiKeyService
{
    public Task<PagedList<ApiKey>> List(int pageIndex);
    public Task<ApiKey> GetByIdAsync(Guid id);
    public Task<ApiKey> GetByRawAPIKey(string rawKey);
    public Task<(ApiKey key, string revealedApiKey)> CreateAsync(string name);
}
