
using EnviroSense.Domain.Entities;
using EnviroSense.Repositories.Repositories;

namespace EnviroSense.Application.Services;

public class AccountPasswordResetService : IAccountPasswordResetService
{
    private readonly IAccountService _accountService;
    private readonly IAccountPasswordResetRepository _accountPasswordResetRepository;

    public AccountPasswordResetService(IAccountService accountService, IAccountPasswordResetRepository accountPasswordResetRepository)
    {
        _accountService = accountService;
        _accountPasswordResetRepository = accountPasswordResetRepository;
    }

    private AccountPasswordReset CreateResetPasswordEntity(Account account)
    {
        var accountToReset = new AccountPasswordReset
        {
            Account = account,
            AccountId = account.Id,
            SecurityCode = Guid.NewGuid(),
            UsedAt = null,
            resetDate = DateTime.UtcNow

        };
        return accountToReset;
    }
    public async Task<bool> ResetPasswordAsync(string email)
    {
        var account = await _accountService.GetAccountByEmail(email);
        if (account == null)
        {
            return false;
        }

        var accountToReset = CreateResetPasswordEntity(account);

        var savedAccountToResset = await _accountPasswordResetRepository.CreateResetPasswordEntityAsync(accountToReset);

        return true;

    }

}
