using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EnviroSense.Plugins.PostgresRepositories.Repositories;

internal class DeviceApiKeyRepository : IDeviceApiKeyRepository
{
    private readonly AppDbContext _context;

    public DeviceApiKeyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DeviceApiKey> GetByIdAsync(Guid deviceId)
    {
        var apiKey = await _context.DeviceApiKeys.FirstOrDefaultAsync(d => d.Id == deviceId);
        if (apiKey == null)
        {
            throw new DeviceApiKeyNotFound("Device API Key not found");
        }

        return apiKey;
    }

    public async Task<DeviceApiKey> CreateAsync(DeviceApiKey apiKey)
    {
        var createdEntity = await _context.DeviceApiKeys.AddAsync(apiKey);
        await _context.SaveChangesAsync();

        return createdEntity.Entity;
    }
}
