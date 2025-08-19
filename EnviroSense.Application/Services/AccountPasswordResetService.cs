using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Clients;
using EnviroSense.Repositories.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;

namespace EnviroSense.Application.Services;

public class AccountPasswordResetService : IAccountPasswordResetService
{
    private readonly IAccountService _accountService;
    private readonly IAccountPasswordResetRepository _accountPasswordResetRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IEmailClient _emailClient;

    public AccountPasswordResetService(IAccountService accountService,
        IAccountPasswordResetRepository accountPasswordResetRepository, IAccountRepository accountRepository, IEmailClient emailClient)
    {
        _accountService = accountService;
        _accountPasswordResetRepository = accountPasswordResetRepository;
        _accountRepository = accountRepository;
        _emailClient = emailClient;
    }

    public async Task<Guid?> ResetPasswordAsync(string email)
    {
        try
        {
            var account = await _accountService.GetAccountByEmail(email);
            var accountToReset = CreateResetPasswordEntity(account!);

            await _accountPasswordResetRepository.CreateAsync(accountToReset);
            var resetLink = $"http://localhost:5276/Accounts/ResetPassword/{accountToReset.SecurityCode}";

            await _emailClient.SendMail(
                "Reset Password",
                $@"<p>Hi {accountToReset.Account.Email},</p>
       <p>We received a request to reset your password for your account on EnviroSense.</p>
       <p>To create a new password, click the link below:<br/>
       <a href=""{resetLink}"">Reset it here</a></p>
       <p>Thanks,<br/>The EnviroSense Team</p>",
                accountToReset.Account.Email
            );
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
        await _emailClient.SendMail(
            "Password has been reset",
            $@"<p>Hi {updatedAccount.Email},</p>
        <p>Your password has been reset.</p>
        <p>Log in with your new password now:<br/>
        <a href=""{signInLink}"">Sign in here</a></p>
        <p>Thanks,<br/>The EnviroSense Team</p>",
            updatedAccount.Email
        );
        return updatedAccount;
    }

    public async Task<Account> ResetPasswordFromSettings(string email, string password)
    {
        var account = await _accountService.GetAccountByEmail(email);
        account.Password = BCryptNet.HashPassword(password, 10);
        var updatedAccount = await _accountPasswordResetRepository.ResetPasswordFromSettingsAsync(account);
        await _emailClient.SendMail(
            "Your Password for EnviroSense Has Been Changed",
            $@"<p>Hi {updatedAccount.Email},</p>
        <p>This email confirms that the password for your EnviroSense account has been successfully changed</p>.
        <p>If you made this change, you can safely disregard this email.</p>

        <p>However, if you did not change your password,</p> 
        <p>your account may have been compromised.</p> 
        <p>Please contact our support team immediately by replying to this email 
        <p>so we can help you secure your account</p>.

        <p>Thank you,</p>
        The EnviroSense Team",
            updatedAccount.Email
        );
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
