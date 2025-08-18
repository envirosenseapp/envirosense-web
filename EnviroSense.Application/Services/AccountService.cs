using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Clients;
using EnviroSense.Repositories.Repositories;
using Microsoft.AspNetCore.Http;
using BCryptNet = BCrypt.Net.BCrypt;

namespace EnviroSense.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmailClient _emailClient;

    public AccountService(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor, IEmailClient emailClient)
    {
        _accountRepository = accountRepository;
        _httpContextAccessor = httpContextAccessor;
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

    public Guid? GetAccountIdFromSession()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext == null || httpContext.Session == null)
        {
            throw new SessionIsNotAvailableException();
        }

        var session = httpContext.Session;
        var accountId = session.GetString("authenticated_account_id");
        if (string.IsNullOrEmpty(accountId))
        {
            return null;
        }

        if (!Guid.TryParse(accountId, out var accountGuid))
        {
            throw new Exception("Unexpected format for account id. Must be guid.");
        }

        return accountGuid;
    }

    public async Task<Account> Login(string email, string password)
    {
        var account = await _accountRepository.GetAccountByEmailAsync(email);
        var isPasswordValid = BCryptNet.Verify(password, account.Password);

        if (isPasswordValid)
        {
            _httpContextAccessor.HttpContext?.Session.SetString("authenticated_account_id", account.Id.ToString());
            await _emailClient.SendMail("Welcome to EnviroSense!",
                "You are successfully signed in.", account.Email);
            return account;
        }
        else
        {
            throw new AccountNotFoundException();
        }
    }

    public async Task<Account> GetAccountById(Guid accountId)
    {
        return await _accountRepository.GetAccountByIdAsync(accountId);
    }

    public async Task<Account?> GetAccountByEmail(string email)
    {
        return await _accountRepository.GetAccountByEmailAsync(email);
    }
}
