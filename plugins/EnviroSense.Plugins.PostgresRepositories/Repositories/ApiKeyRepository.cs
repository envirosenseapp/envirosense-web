using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EnviroSense.Plugins.PostgresRepositories.Repositories;

internal class ApiKeyRepository : IApiKeyRepository
{
    private readonly AppDbContext _context;

    public ApiKeyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ApiKey>> ListAsync(Guid id)
    {
        var list = await _context.ApiKeys.Where(x => x.AccountId == id).ToListAsync();
        return list;

    }
    public async Task<ApiKey> GetByHashAsync(string hash)
    {
        var apiKey = await _context.ApiKeys.FirstOrDefaultAsync(d => d.KeyHash == hash);
        if (apiKey == null)
        {
            throw new DeviceApiKeyNotFound("Device API Key not found");
        }

        return apiKey;
    }

    public async Task<ApiKey> GetByIdAsync(Guid apiId)
    {
        var apiKey = await _context.ApiKeys.FirstOrDefaultAsync(d => d.Id == apiId);
        if (apiKey == null)
        {
            throw new DeviceApiKeyNotFound("Device API Key not found");
        }

        return apiKey;
    }

    public async Task<ApiKey> CreateAsync(ApiKey apiKey)
    {
        var createdEntity = await _context.ApiKeys.AddAsync(apiKey);
        await _context.SaveChangesAsync();

        return createdEntity.Entity;
    }
}
