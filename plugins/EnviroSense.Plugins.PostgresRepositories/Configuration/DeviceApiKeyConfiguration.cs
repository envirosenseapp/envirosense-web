// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using EnviroSense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnviroSense.Plugins.PostgresRepositories.Configuration;

internal class DeviceApiKeyConfiguration : IEntityTypeConfiguration<DeviceApiKey>
{
    public void Configure(EntityTypeBuilder<DeviceApiKey> builder)
    {
        builder.ToTable("device_api_keys");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.Property(e => e.Name)
            .HasColumnName("name");

        builder.Property(e => e.KeyHash)
            .HasColumnName("key_hash")
            .IsRequired();

        builder.Property(e => e.DeviceId)
            .HasColumnName("device_id");

        builder.HasOne(e => e.Device)
            .WithMany(e => e.ApiKeys)
            .HasForeignKey(e => e.DeviceId);

        builder.Property(e => e.DisabledAt)
            .HasColumnName("disabled_at");

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
