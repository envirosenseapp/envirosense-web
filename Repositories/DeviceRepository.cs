using EnviroSense.Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnviroSense.Web.Repositories;

public class DeviceRepository : IDeciveRepository
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

    public async Task<Device?> GetAsync(Guid Id)
    {
        return await _context.Devices.FirstOrDefaultAsync(d => d.Id == Id);
    }

    public async Task<Device> CreateAsync(Device device)
    {
        var createDevice = await _context.Devices.AddAsync(device);
        await _context.SaveChangesAsync();

        return createDevice.Entity;
    }
}
