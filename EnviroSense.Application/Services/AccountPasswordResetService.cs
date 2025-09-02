using EnviroSense.Application.Emailing;
using EnviroSense.Domain.Emailing;
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
    private readonly IEmailSender _emailSender;

    public AccountPasswordResetService(IAccountService accountService,
        IAccountPasswordResetRepository accountPasswordResetRepository, IAccountRepository accountRepository, IEmailSender emailSender)
    {
        _accountService = accountService;
        _accountPasswordResetRepository = accountPasswordResetRepository;
        _accountRepository = accountRepository;
        _emailSender = emailSender;

    }

    public async Task<Guid?> ResetPasswordAsync(string email)
    {
        try
        {
            var account = await _accountService.GetAccountByEmail(email);
            var accountToReset = CreateResetPasswordEntity(account!);

            await _accountPasswordResetRepository.CreateAsync(accountToReset);
            var resetLink = $"http://localhost:5276/Accounts/ResetPassword/{accountToReset.SecurityCode}";
            await _emailSender.SendEmailAsync(account.Email, new SendPasswordResetLinkEmailPayload()
            {
                GeneratedAt = DateTime.UtcNow,
                PasswordResetEmail = account.Email,
                PasswordResetLink = resetLink
            });
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
        var signInLink = $"http://localhost:5276/Accounts/SignIn";
        await _emailSender.SendEmailAsync(account.Email, new SendPasswordResetNotificationEmailPayload()
        {
            ResetAt = DateTime.UtcNow,
            PasswordResetEmail = account.Email,
            RedirectLink = signInLink
        });
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
