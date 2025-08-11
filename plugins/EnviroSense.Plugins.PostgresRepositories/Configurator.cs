using EnviroSense.Plugins.PostgresRepositories.Migrations.Runner;
using EnviroSense.Plugins.PostgresRepositories.Repositories;
using EnviroSense.Repositories.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EnviroSense.Plugins.PostgresRepositories;

public static class Configurator
{
    public static void AddPostgresRepositories(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddScoped<IAccessRepository, AccessRepository>();
        serviceCollection.AddScoped<IDeviceRepository, DeviceRepository>();
        serviceCollection.AddScoped<IMeasurementRepository, MeasurementRepository>();
        serviceCollection.AddScoped<IAccountRepository, AccountRepository>();


        serviceCollection.AddDbContext<AppDbContext>(opts =>
        {
            opts.UseNpgsql(connectionString);
        });

        serviceCollection.AddScoped<IMigrationRunner, MigrationRunner>();
    }

    public static IApplicationBuilder MustMigrate(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var migrator = serviceScope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            migrator.Run();
        }

        return app;
    }

}
