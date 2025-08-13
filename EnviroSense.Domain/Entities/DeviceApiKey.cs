// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace EnviroSense.Domain.Entities;

public class DeviceApiKey
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required string KeyHash { get; set; }

    public required Guid DeviceId { get; set; }

    public virtual Device? Device { get; set; }

    public DateTime? DisabledAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
