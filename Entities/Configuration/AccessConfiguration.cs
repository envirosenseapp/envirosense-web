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

        builder.Property(e => e.IpAddress)
            .HasColumnName("ip_address");

        builder.Property(e => e.Client)
            .HasColumnName("client");

        builder.Property(e => e.Resource)
            .HasColumnName("resource");   

           // "ressource" is intentionally misspelled with two 's' so it doesn't turn blue in the SQL script in V2
              
    }
}