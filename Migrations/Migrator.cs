using EvolveDb;
using Microsoft.EntityFrameworkCore;

namespace EnviroSense.Web.Migrations;

public class Migrator
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<Migrator> _logger;

    public Migrator(AppDbContext dbContext, ILogger<Migrator> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public void MigrateDatabase()
    {
        try
        {
            var evolve = new Evolve(_dbContext.Database.GetDbConnection())
            {
                Locations = new[] { "Migrations/SQL" },
                IsEraseDisabled = true,
            };

            evolve.Migrate();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database migration failed.");
            throw;
        }
    }
}
