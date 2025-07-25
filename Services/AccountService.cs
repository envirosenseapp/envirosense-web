using System;
using EnviroSense.Web.Repositories;

namespace EnviroSense.Web.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

}
