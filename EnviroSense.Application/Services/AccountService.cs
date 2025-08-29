using EnviroSense.Application.Emailing;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;
using Microsoft.AspNetCore.Http;
using BCryptNet = BCrypt.Net.BCrypt;

namespace EnviroSense.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmailClient _emailClient;

    public AccountService(IAccountRepository accountRepository,
        IEmailClient emailClient)
    {
        _accountRepository = accountRepository;
        _emailClient = emailClient;
    }

    public async Task<bool> IsEmailTaken(string email)
    {
        return await _accountRepository.IsEmailTaken(email);
    }

    public async Task<Account> Add(string email, string password)
    {
        string hashedPassword = BCryptNet.HashPassword(password, 10);
        var account = new Account()
        {
            Email = email,
            Password = hashedPassword,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            Devices = new List<Device>(),
            Accesses = new List<Access>(),
            PasswordResets = new List<AccountPasswordReset>()
        };
        var accountAdded = await _accountRepository.AddAsync(account);
        await _emailClient.SendMail("Welcome to EnviroSense!",
            "Thank you for registering with us.", accountAdded.Email);
        return accountAdded;
    }


    public async Task<Account> GetAccountById(Guid accountId)
    {
        return await _accountRepository.GetAccountByIdAsync(accountId);
    }

    public async Task<Account> GetAccountByEmail(string email)
    {
        return await _accountRepository.GetAccountByEmailAsync(email);
    }

    public async Task<Account> ResetPasswordFromSettings(Guid id, string password)
    {
        var account = await GetAccountById(id);
        account.Password = BCryptNet.HashPassword(password, 10);
        var updatedAccount = await _accountRepository.UpdateAsync(account);
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

}
