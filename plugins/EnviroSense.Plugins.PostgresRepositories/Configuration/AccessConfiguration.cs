using EnviroSense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnviroSense.Plugins.PostgresRepositories.Configuration;

internal class AccessConfiguration : IEntityTypeConfiguration<Access>
{
    public void Configure(EntityTypeBuilder<Access> builder)
    {
        builder.ToTable("accesses");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id");
        builder.Property(a => a.AccountId).HasColumnName("account_id");
        builder.HasOne(a => a.Account).WithMany(a => a.Accesses).HasForeignKey(a => a.AccountId).IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(e => e.IpAddress)
            .HasColumnName("ip_address").HasMaxLength(45);

        builder.Property(e => e.Client)
            .HasColumnName("client").HasMaxLength(512);

        builder.Property(e => e.Resource)
            .HasColumnName("resource").HasMaxLength(1024);
    }
}
