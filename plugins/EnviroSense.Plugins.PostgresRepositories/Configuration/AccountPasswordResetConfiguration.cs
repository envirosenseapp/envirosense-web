using EnviroSense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnviroSense.Plugins.PostgresRepositories.Configuration;

public class AccountPasswordResetConfiguration : IEntityTypeConfiguration<AccountPasswordReset>
{
    public void Configure(EntityTypeBuilder<AccountPasswordReset> builder)
    {
        builder.ToTable("account_password_resets");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.AccountId).HasColumnName("account_id");
        builder.HasOne(a => a.Account).WithMany(r => r.Resets).HasForeignKey(a => a.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(x => x.SecurityCode).HasColumnName("security_code");
        builder.Property(x => x.UsedAt).HasColumnName("used_at");
        builder.Property(x => x.resetDate).HasColumnName("reset_date");
    }
}
