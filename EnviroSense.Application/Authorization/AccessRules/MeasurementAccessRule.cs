using EnviroSense.Application.Authentication;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Authorization.AccessRules;

public class MeasurementAccessRule : BaseAccessRule<Measurement>
{
    public MeasurementAccessRule(IAuthenticationContext authenticationContext) : base(authenticationContext)
    {
    }

    protected override Task<bool> AccountHasAccess(Guid accountId, Measurement entity)
    {
        return Task.FromResult(entity.Device.AccountId == accountId);
    }
}
