// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using EnviroSense.Application.Services;
using EnviroSense.Domain.Exceptions;

namespace EnviroSense.Application.Authorization.AccessRules;

internal abstract class BaseAccessRule<T> : IAccessRule<T>
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
