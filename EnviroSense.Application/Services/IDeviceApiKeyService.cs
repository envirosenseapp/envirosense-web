// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Services;

public interface IDeviceApiKeyService
{
    public Task<DeviceApiKey> GetByIdAsync(Guid deviceId);

    public Task<Tuple<DeviceApiKey, string>> CreateAsync(Device device, string name);
}
