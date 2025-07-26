using EnviroSense.Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnviroSense.Web.Repositories;

public class MeasurementRepository : IMeasurementRepository
{
    private readonly AppDbContext _context;

    public MeasurementRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Measurement> CreateAsync(Measurement measurement)
    {
        var createMeasurement = await _context.Measurements.AddAsync(measurement);
        await _context.SaveChangesAsync();

        return createMeasurement.Entity;
    }

    public async Task<List<Measurement>> ListAsync(Guid deviceId)
    {
        return await _context.Measurements.Where(m => m.DeviceId == deviceId).ToListAsync();
    }
}
