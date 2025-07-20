using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnviroSense.Web.Entities.Configuration;

public class AccessConfiguration: IEntityTypeConfiguration<Access>
{
    public void Configure(EntityTypeBuilder<Access> builder)
    {
        
        builder.ToTable("accesses");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id");
            
        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}