using EnviroSense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnviroSense.Plugins.PostgresRepositories.Configuration;

internal class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
{
    public void Configure(EntityTypeBuilder<ApiKey> builder)
    {
        builder.ToTable("api_keys");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.Property(e => e.Name)
            .HasColumnName("name");

        builder.Property(e => e.KeyHash)
            .HasColumnName("key_hash")
            .IsRequired();

        builder.Property(e => e.AccountId)
            .HasColumnName("account_id");

        builder.HasOne(e => e.Account)
            .WithMany(e => e.ApiKeys)
            .HasForeignKey(e => e.AccountId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.DisabledAt)
            .HasColumnName("disabled_at");

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
