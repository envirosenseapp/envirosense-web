using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;

namespace EnviroSense.Application.Services;

public class AccountPasswordResetService : IAccountPasswordResetService
{
    private readonly IAccountService _accountService;
    private readonly IAccountPasswordResetRepository _accountPasswordResetRepository;

    public AccountPasswordResetService(IAccountService accountService,
        IAccountPasswordResetRepository accountPasswordResetRepository)
    {
        _accountService = accountService;
        _accountPasswordResetRepository = accountPasswordResetRepository;
    }

    public async Task<bool> ResetPasswordAsync(string email)
    {
        try
        {
            var account = await _accountService.GetAccountByEmail(email);
            var accountToReset = CreateResetPasswordEntity(account!);

            await _accountPasswordResetRepository.CreateResetPasswordEntityAsync(accountToReset);

            return true;
        }
        catch (AccountNotFoundException)
        {
            return false;
        }


    }

    private AccountPasswordReset CreateResetPasswordEntity(Account account)
    {
        var accountToReset = new AccountPasswordReset
        {
            Account = account,
            AccountId = account.Id,
            SecurityCode = Guid.NewGuid(),
            UsedAt = null,
            ResetDate = DateTime.UtcNow
        };
        return accountToReset;
    }
}
