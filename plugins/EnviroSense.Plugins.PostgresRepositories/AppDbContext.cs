using EnviroSense.Domain.Entities;
using EnviroSense.Plugins.PostgresRepositories.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EnviroSense.Plugins.PostgresRepositories;

internal class AppDbContext : DbContext
{

    public required DbSet<Access> Accesses { get; set; }
    public required DbSet<Device> Devices { get; set; }
    public required DbSet<Measurement> Measurements { get; set; }
    public required DbSet<Account> Accounts { get; set; }

    public required DbSet<AccountPasswordReset> AccountPasswordResets { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AccessConfiguration());
        modelBuilder.ApplyConfiguration(new DeviceConfiguration());
        modelBuilder.ApplyConfiguration(new MeasurementConfiguration());
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new AccountPasswordResetConfiguration());
    }
}
