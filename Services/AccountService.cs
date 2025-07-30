using EnviroSense.Web.Entities;
using EnviroSense.Web.Exceptions;
using EnviroSense.Web.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;

namespace EnviroSense.Web.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountService(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
    {
        _accountRepository = accountRepository;
        _httpContextAccessor = httpContextAccessor;
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
            CreatedAt = DateTime.UtcNow
        };
        return await _accountRepository.AddAsync(account);
    }

    public async Task<Account> Login(string email, string password)
    {
        var account = await _accountRepository.GetAccountByEmail(email);
        var isPasswordValid = BCryptNet.Verify(password, account.Password);

        if (isPasswordValid)
        {
            var accountId = account.Id.ToString();
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null)
            {
                throw new SessionIsNotAvailableException();
            }
            session.SetString("authenticated_account_id", accountId);
            return account;
        }
        else
        {
            throw new AccountNotFoundException();
        }
    }
}
