using EnviroSense.Application.Authentication;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Authorization.AccessRules;

public class DeviceApiKeyAccessRule : BaseAccessRule<DeviceApiKey>
{
    public DeviceApiKeyAccessRule(IAuthenticationContext authenticationContext) : base(authenticationContext)
    {
    }

    protected override Task<bool> AccountHasAccess(Guid accountId, DeviceApiKey entity)
    {
        return Task.FromResult(entity.Device.AccountId == accountId);
    }
}
