using EnviroSense.Application.Services;
using EnviroSense.Domain.Exceptions;

namespace EnviroSense.Application.Authorization.AccessRules;

public abstract class BaseAccessRule<T> : IAccessRule<T>
{
    private readonly IAccountService _accountService;

    protected BaseAccessRule(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<bool> HasAccess(T entity)
    {
        var accountId = MustGetAccountId();

        return await AccountHasAccess(accountId, entity);
    }

    protected abstract Task<bool> AccountHasAccess(Guid accountId, T entity);

    private Guid MustGetAccountId()
    {
        var accountId = _accountService.GetAccountIdFromSession();
        if (accountId == null)
        {
            throw new AccessToForbiddenEntityException("Access forbidden (reason: Must be logged in).");
        }

        return accountId.Value;
    }
}
