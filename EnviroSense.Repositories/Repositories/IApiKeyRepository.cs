using EnviroSense.Domain.Entities;

namespace EnviroSense.Repositories.Repositories;

public interface IApiKeyRepository
{
    public Task<List<ApiKey>> ListAsync(Guid accountId);
    public Task<ApiKey> GetByHashAsync(string hash);
    public Task<ApiKey> GetByIdAsync(Guid Id);
    public Task<ApiKey> CreateAsync(ApiKey apiKey);
}
