using EnviroSense.Application.Authentication;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Exceptions;

namespace EnviroSense.Application.Authorization.AccessRules;

public abstract class BaseAccessRule<T> : IAccessRule<T>
{
    private readonly IAuthenticationContext _authenticationContext;

    protected BaseAccessRule(IAuthenticationContext authenticationContext)
    {
        _authenticationContext = authenticationContext;
    }

    public async Task<bool> HasAccess(T entity)
    {
        var accountId = await MustGetAccountId();

        return await AccountHasAccess(accountId, entity);
    }

    protected abstract Task<bool> AccountHasAccess(Guid accountId, T entity);

    private async Task<Guid> MustGetAccountId()
    {
        var accountId = await _authenticationContext.CurrentAccountId();
        if (accountId == null)
        {
            throw new AccessToForbiddenEntityException("Access forbidden (reason: Must be logged in).");
        }

        return accountId.Value;
    }
}
