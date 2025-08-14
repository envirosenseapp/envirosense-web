using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EnviroSense.Plugins.PostgresRepositories.Repositories;

internal class DeviceRepository : IDeviceRepository
{
    private readonly AppDbContext _context;

    public DeviceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Device>> ListAsync(Guid accountId)
    {
        return await _context.Devices.OrderByDescending(a => a.CreatedAt).Where(a => a.AccountId == accountId)
            .ToListAsync();
    }

    public async Task<Device> GetAsync(Guid id)
    {
        var result = await _context.Devices.FirstOrDefaultAsync(d => d.Id == id);
        if (result == null)
        {
            throw new DeviceNotFoundException(id);
        }

        return result;
    }

    public async Task<Device> CreateAsync(Device device)
    {
        var createDevice = await _context.Devices.AddAsync(device);
        await _context.SaveChangesAsync();

        return createDevice.Entity;
    }
}
