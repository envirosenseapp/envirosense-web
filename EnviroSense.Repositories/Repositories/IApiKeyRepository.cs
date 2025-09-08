using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Filters;
using EnviroSense.Repositories.Core;

namespace EnviroSense.Repositories.Repositories;

public interface IApiKeyRepository
{
    public Task<PagedList<ApiKey>> ListAsync(ApiKeyFilters filters);
    public Task<ApiKey> GetByHashAsync(string hash);
    public Task<ApiKey> GetByIdAsync(Guid id);
    public Task<ApiKey> CreateAsync(ApiKey apiKey);
}
