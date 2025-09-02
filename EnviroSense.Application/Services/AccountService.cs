using EnviroSense.Application.Emailing;
using EnviroSense.Domain.Emailing;
using EnviroSense.Domain.Entities;
using EnviroSense.Repositories.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;

namespace EnviroSense.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmailSender _emailSender;

    public AccountService(IAccountRepository accountRepository,
        IEmailSender emailSender)
    {
        _accountRepository = accountRepository;
        _emailSender = emailSender;
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
        await _emailSender.SendEmailAsync(accountAdded.Email, new SendSignedUpEmailPayload()
        {
            LoginDate = DateTime.UtcNow,
        });
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
        await _emailSender.SendEmailAsync(account.Email,
            new SendPasswordChangedNotificationEmailPayload()
            {
                ChangedAt = DateTime.UtcNow,
                PasswordResetEmail = account.Email

            });
        return updatedAccount;
    }

}
