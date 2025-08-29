using EnviroSense.Domain.Entities;

namespace EnviroSense.Repositories.Repositories;

public interface IApiKeyRepository
{
    public Task<List<ApiKey>> ListAsync(Guid id);
    public Task<ApiKey> GetByHashAsync(string hash);
    public Task<ApiKey> GetByIdAsync(Guid apiId);
    public Task<ApiKey> CreateAsync(ApiKey apiKey);
}
