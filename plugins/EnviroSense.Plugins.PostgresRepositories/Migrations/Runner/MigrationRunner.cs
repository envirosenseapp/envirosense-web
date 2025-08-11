using EvolveDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EnviroSense.Plugins.PostgresRepositories.Migrations.Runner;

internal class MigrationRunner : IMigrationRunner
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<MigrationRunner> _logger;

    public MigrationRunner(AppDbContext dbContext, ILogger<MigrationRunner> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public void Run()
    {
        try
        {
            var evolve = new Evolve(_dbContext.Database.GetDbConnection())
            {
                Locations = new[]
                {
                    "Migrations/SQL",

                    //TODO: once we set deployment, make seeds applicable per environment
                    "Migrations/Seeds"
                },
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
