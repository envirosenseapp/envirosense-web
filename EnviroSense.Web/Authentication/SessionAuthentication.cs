using EnviroSense.Application.Emailing;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Emailing;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using BCryptNet = BCrypt.Net.BCrypt;

namespace EnviroSense.Web.Authentication;

public class SessionAuthentication : ISessionAuthentication
{
    private readonly IAccountService _accountService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmailSender _emailSender;

    public SessionAuthentication(IAccountService accountService, IHttpContextAccessor httpContextAccessor, IEmailSender emailSender)
    {
        _accountService = accountService;
        _httpContextAccessor = httpContextAccessor;
        _emailSender = emailSender;
    }

    public async Task<Account> Login(string email, string password)
    {
        var account = await _accountService.GetAccountByEmail(email);
        var isPasswordValid = BCryptNet.Verify(password, account.Password);

        if (isPasswordValid)
        {
            _httpContextAccessor.HttpContext?.Session.SetString("authenticated_account_id", account.Id.ToString());
            await _emailSender.SendEmailAsync(new SendSignedInEmail()
            {
                Email = account.Email,
                Title = "You are successfully signed in.",
                LoginDate = DateTime.UtcNow,
            });
            return account;
        }
        else
        {
            throw new AccountNotFoundException();
        }
    }

    public void Logout()
    {
        _httpContextAccessor.HttpContext?.Session.Clear();
    }

    public async Task<Guid?> CurrentAccountId()
    {
        var account = await CurrentAccount();

        return account?.Id;
    }

    public async Task<Account?> CurrentAccount()
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

        try
        {
            return await _accountService.GetAccountById(accountGuid);
        }
        catch (AccountNotFoundException)
        {
            return null;
        }
    }
}

