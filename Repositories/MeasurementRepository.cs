using EnviroSense.Web.Entities;

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
}
