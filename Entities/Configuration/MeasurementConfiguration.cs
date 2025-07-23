using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnviroSense.Web.Entities.Configuration;

public class MeasurementConfiguration : IEntityTypeConfiguration<Measurement>
{
    public void Configure(EntityTypeBuilder<Measurement> builder)
    {
        builder.ToTable("measurements");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).HasColumnName("id");
        builder.HasOne(d => d.Device).WithMany(m => m.Measurements).HasForeignKey(m => m.DeviceId).OnDelete(DeleteBehavior.Cascade);
        builder.Property(m => m.Temperature).HasColumnName("temperature");
        builder.Property(m => m.Humidity).HasColumnName("humidity");
        builder.Property(m => m.RecordingDate).HasColumnName("recording_date").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(m => m.RecordingCreatedAt).HasColumnName("recording_created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(m => m.DeviceId).HasColumnName("device_id");
    }
}
