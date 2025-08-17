using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;

namespace EnviroSense.Application.Services;

public class AccountPasswordResetService : IAccountPasswordResetService
{
    private readonly IAccountService _accountService;
    private readonly IAccountPasswordResetRepository _accountPasswordResetRepository;
    private readonly IAccountRepository _accountRepository;

    public AccountPasswordResetService(IAccountService accountService,
        IAccountPasswordResetRepository accountPasswordResetRepository, IAccountRepository accountRepository)
    {
        _accountService = accountService;
        _accountPasswordResetRepository = accountPasswordResetRepository;
        _accountRepository = accountRepository;
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
        try
        {
            var accountToReset = await _accountPasswordResetRepository.GetBySecurityCodeAsync(securityCode);

            if (accountToReset.UsedAt != null)
            {
                throw new ResetPasswordAlreadyUsedException();
            }

            if (accountToReset.ResetDate.AddHours(24) <= DateTime.UtcNow)
            {
                throw new ResetPasswordLinkExpiredException();
            }

            accountToReset.UsedAt = DateTime.UtcNow;
            var account = accountToReset.Account;
            account.Password = BCryptNet.HashPassword(newPassword, 10);
            await _accountPasswordResetRepository.UpdateAsync(accountToReset);
            var updatedAccount = await _accountRepository.UpdateAsync(account);


            return updatedAccount;
        }
        catch (AccountPasswordResetNotFoundException)
        {
            throw new AccountPasswordResetNotFoundException();
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
