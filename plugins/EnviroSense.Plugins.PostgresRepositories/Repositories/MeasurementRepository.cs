using EnviroSense.Domain.Entities;
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

    public async Task<List<Measurement?>> TakeMeasurementsForGivenDay(Guid deviceId, DateTime date)
    {
        var startDate = date.Date;
        var endDate = date.Date.AddDays(1);

        var measurementsList = await _context.Measurements
            .Where(m => m.DeviceId == deviceId &&
                        m.RecordingDate >= startDate &&
                        m.RecordingDate < endDate)
            .ToListAsync();
        return measurementsList;
    }
}
