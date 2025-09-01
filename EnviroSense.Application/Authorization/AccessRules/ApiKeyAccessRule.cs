using EnviroSense.Application.Authentication;
using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Authorization.AccessRules;

public class ApiKeyAccessRule : BaseAccessRule<ApiKey>
{
    public ApiKeyAccessRule(IAuthenticationContext authenticationContext) : base(authenticationContext)
    {
    }

    protected override Task<bool> AccountHasAccess(Guid accountId, ApiKey entity)
    {
        return Task.FromResult(entity.AccountId == accountId);
    }
}
