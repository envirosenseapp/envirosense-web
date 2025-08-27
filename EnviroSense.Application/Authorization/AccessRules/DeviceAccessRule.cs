using EnviroSense.Application.Authentication;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Authorization.AccessRules;

public class DeviceAccessRule : BaseAccessRule<Device>
{
    public DeviceAccessRule(IAuthenticationContext authenticationContext) : base(authenticationContext)
    {
    }

    protected override Task<bool> AccountHasAccess(Guid accountId, Device entity)
    {
        return Task.FromResult(entity.AccountId == accountId);
    }
}
