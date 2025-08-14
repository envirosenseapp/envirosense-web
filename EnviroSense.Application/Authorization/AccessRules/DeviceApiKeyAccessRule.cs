// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using EnviroSense.Application.Services;
using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Authorization.AccessRules;

internal class DeviceApiKeyAccessRule : BaseAccessRule<DeviceApiKey>
{
    public DeviceApiKeyAccessRule(IAccountService accountService) : base(accountService)
    {
    }

    protected override Task<bool> AccountHasAccess(Guid accountId, DeviceApiKey entity)
    {
        return Task.FromResult(entity.Device.AccountId == accountId);
    }
}
