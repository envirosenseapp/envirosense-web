using EnviroSense.Web.Entities;
using EnviroSense.Web.Entities.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EnviroSense.Web;
public class AppDbContext : DbContext
{
    public required DbSet<Access> Accesses { get; set; }
    public required DbSet<Device> Devices { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AccessConfiguration());
        modelBuilder.ApplyConfiguration(new DeviceConfiguration());
    }
}
