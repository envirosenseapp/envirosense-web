using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;

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

    public async Task<Guid?> ResetPasswordAsync(string email)
    {
        try
        {
            var account = await _accountService.GetAccountByEmail(email);
            var accountToReset = CreateResetPasswordEntity(account!);

            await _accountPasswordResetRepository.CreateAsync(accountToReset);

            return accountToReset.SecurityCode;
        }
        catch (AccountNotFoundException)
        {
            return Guid.Empty;
        }


    }

    public async Task<Account> Reset(Guid securityCode, string newPassword)
    {
        var accountToReset = await _accountPasswordResetRepository.GetBySecurityCodeAsync(securityCode);
        if (accountToReset.ResetDate.AddHours(24) < DateTime.UtcNow)
        {
            throw new ResetPasswordLinkExpiredException();
        }

        if (accountToReset.UsedAt != null)
        {
            throw new ResetPasswordAlreadyUsedException();
        }
        string hashedPassword = BCryptNet.HashPassword(newPassword, 10);
        var updatedAccount = await _accountPasswordResetRepository.UpdateAccountPasswordAsync(accountToReset.AccountId, hashedPassword);
        return updatedAccount;
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
