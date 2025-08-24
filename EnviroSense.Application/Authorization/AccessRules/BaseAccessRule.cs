using EnviroSense.Application.Authentication;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Exceptions;

namespace EnviroSense.Application.Authorization.AccessRules;

public abstract class BaseAccessRule<T> : IAccessRule<T>
{
    private readonly IAuthenticationRetriever _authenticationRetriever;

    protected BaseAccessRule(IAuthenticationRetriever authenticationRetriever)
    {
        _authenticationRetriever = authenticationRetriever;
    }

    public async Task<bool> HasAccess(T entity)
    {
        var accountId = await MustGetAccountId();

        return await AccountHasAccess(accountId, entity);
    }

    protected abstract Task<bool> AccountHasAccess(Guid accountId, T entity);

    private async Task<Guid> MustGetAccountId()
    {
        var accountId = await _authenticationRetriever.GetCurrentAccountId();
        if (accountId == null)
        {
            throw new AccessToForbiddenEntityException("Access forbidden (reason: Must be logged in).");
        }

        return accountId.Value;
    }
}
