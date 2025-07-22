using System;
using EnviroSense.Web.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace EnviroSense.Web.Repositories;

public class DeviceRepository : IDeciveRepository
{
    private readonly AppDbContext _context;
    public DeviceRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Device>> ListAsync()
    {
        return await _context.Devices.OrderByDescending(a => a.CreatedAt).ToListAsync();
    }
    public async Task<Device> GetAsync(Guid Id)
    {
        return await _context.Devices.FirstOrDefaultAsync(d => d.Id == Id);
    }
}
