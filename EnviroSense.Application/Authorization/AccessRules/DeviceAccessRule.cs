using EnviroSense.Application.Services;
using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Authorization.AccessRules;

public class DeviceAccessRule : BaseAccessRule<Device>
{
    public DeviceAccessRule(IAccountService accountService) : base(accountService)
    {
    }

    protected override Task<bool> AccountHasAccess(Guid accountId, Device entity)
    {
        return Task.FromResult(entity.AccountId == accountId);
    }
}
