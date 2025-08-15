using EnviroSense.Application.Services;
using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Authorization.AccessRules;

public class MeasurementAccessRule : BaseAccessRule<Measurement>
{
    public MeasurementAccessRule(IAccountService accountService) : base(accountService)
    {
    }

    protected override Task<bool> AccountHasAccess(Guid accountId, Measurement entity)
    {
        return Task.FromResult(entity.Device.AccountId == accountId);
    }
}
