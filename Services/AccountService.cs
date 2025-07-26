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

    public async Task<Account> Add(string email, string password)
    {
        var account = new Account
        {
            Email = email,
            Password = password,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        return await _accountRepository.AddAsync(account);
    }
}
