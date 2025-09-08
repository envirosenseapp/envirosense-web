using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Domain.Filters;
using EnviroSense.Plugins.PostgresRepositories.Extensions;
using EnviroSense.Repositories.Core;
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

    public Task<PagedList<ApiKey>> ListAsync(ApiKeyFilters filters)
    {
        var list = _context.ApiKeys.Where(x => x.AccountId == filters.AccountId).ToPagedListAsync(filters);
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

    public async Task<ApiKey> GetByIdAsync(Guid id)
    {
        var apiKey = await _context.ApiKeys.FirstOrDefaultAsync(d => d.Id == id);
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
