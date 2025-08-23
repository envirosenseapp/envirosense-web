using EnviroSense.Application.Services;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Clients;
using BCryptNet = BCrypt.Net.BCrypt;

namespace EnviroSense.Web.Authentication;

public class SessionAuthentication : ISessionAuthentication
{
    private readonly IAccountService _accountService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmailClient _emailClient;

    public SessionAuthentication(IAccountService accountService, IHttpContextAccessor httpContextAccessor, IEmailClient emailClient)
    {
        _accountService = accountService;
        _httpContextAccessor = httpContextAccessor;
        _emailClient = emailClient;
    }

    public async Task<Account> Login(string email, string password)
    {
        var account = await _accountService.GetAccountByEmail(email);
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

    public void Logout()
    {
        _httpContextAccessor.HttpContext?.Session.Clear();
    }

    public Guid? GetCurrentAccountId()
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

}

