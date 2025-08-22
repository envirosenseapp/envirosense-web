using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EnviroSense.Plugins.PostgresRepositories.Repositories;

internal class MeasurementRepository : IMeasurementRepository
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

    public async Task<Measurement> GetAsync(Guid id)
    {
        var measurement = await _context.Measurements.FirstOrDefaultAsync(m => m.Id == id);
        if (measurement == null)
        {
            throw new MeasurementNotFoundException(id);
        }
        return measurement;
    }

    public async Task<List<Measurement>> ListAsync(Guid deviceId)
    {
        return await _context.Measurements.Where(m => m.DeviceId == deviceId).ToListAsync();
    }

    public async Task<Measurement?> GetLastestAsync(Guid deviceId)
    {
        return await _context.Measurements
            .Where(m => m.DeviceId == deviceId)
            .OrderByDescending(m => m.RecordingDate)
            .FirstOrDefaultAsync();
    }
}
