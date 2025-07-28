using EnviroSense.Web.Entities;
using EnviroSense.Web.Repositories;

namespace EnviroSense.Web.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<bool> IsEmailTaken(string email)
    {
        return await _accountRepository.IsEmailTaken(email);
    }

    public async Task<Account> Add(Account account)
    {
        return await _accountRepository.AddAsync(account);
    }
}
