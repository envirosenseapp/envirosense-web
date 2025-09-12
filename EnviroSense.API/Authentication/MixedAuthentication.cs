using System.Text;
using EnviroSense.API.Configuration;
using EnviroSense.API.Exceptions;
using EnviroSense.Application.Algorithms;
using EnviroSense.Application.Authentication;
using EnviroSense.Application.Emailing;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Emailing;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using BCryptNet = BCrypt.Net.BCrypt;

namespace EnviroSense.API.Authentication;

// MixedAuthentication is a class that implements the IAuthenticationContext and ITokenAuthentication interfaces.
// It is used to authenticate users using both API keys and JWT tokens.
// It is used in the EnviroSense.API project.
public class MixedAuthentication : IAuthenticationContext, ITokenAuthentication
{
    private readonly IOptions<JwtSettings> _jwtSettings;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IApiKeyRepository _apiKeyRepository;
    private readonly IApiKeyGenerator _apiKeyGenerator;
    private readonly IAccountService _accountService;
    private readonly IEmailSender _emailSender;

    public MixedAuthentication(
        IOptions<JwtSettings> jwtSettings,
        IHttpContextAccessor httpContextAccessor,
        IApiKeyRepository apiKeyRepository,
        IApiKeyGenerator apiKeyGenerator,
        IAccountService accountService,
        IEmailSender emailSender
    )
    {
        _jwtSettings = jwtSettings;
        _httpContextAccessor = httpContextAccessor;
        _apiKeyRepository = apiKeyRepository;
        _apiKeyGenerator = apiKeyGenerator;
        _accountService = accountService;
        _emailSender = emailSender;
    }

    public async Task<Guid?> CurrentAccountId()
    {
        var account = await CurrentAccount();

        return account?.Id;
    }

    public async Task<Account?> CurrentAccount()
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            throw new Exception("could not get http context");
        }

        if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("X-Api-Key", out var headerValues))
        {
            return await CurrentAccountFromApiKey(headerValues);
        }
        if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out headerValues))
        {
            return await CurrentAccountFromAuthorization(headerValues);
        }

        return null;
    }

    public async Task<Account?> CurrentAccountFromApiKey(StringValues headerValues)
    {
        if (headerValues.Count != 1)
        {
            throw new InvalidApiKeyException("multiple values found, but expected one");
        }

        var key = headerValues[0] ?? throw new InvalidApiKeyException("expecting a value");

        try
        {
            var hash = _apiKeyGenerator.Hash(key);
            var apiKey = await _apiKeyRepository.GetByHashAsync(hash);

            return apiKey.Account;
        }
        catch (DeviceApiKeyNotFound)
        {
            throw new InvalidApiKeyException("api key not found");
        }
    }

    public async Task<Account?> CurrentAccountFromAuthorization(StringValues headerValues)
    {
        if (headerValues.Count != 1)
        {
            throw new InvalidBearerException("multiple values found, but expected one");
        }

        var authorizationValue = headerValues[0] ?? throw new InvalidBearerException("expecting a value");
        if (!authorizationValue.StartsWith("Bearer"))
        {
            throw new InvalidBearerException("Authorization header must start with Bearer");
        }

        var token = authorizationValue.Remove(0, "Bearer".Length);
        token = token.Trim();

        try
        {
            var accountId = GetAccountIDFromToken(token);
            var account = await _accountService.GetAccountById(accountId);

            return account;
        }
        catch (AccountNotFoundException)
        {
            throw new Exception("Could not find account from JWT issuer. Really weird situation.");
        }
    }

    public async Task<string> Login(string email, string password)
    {
        var account = await _accountService.GetAccountByEmail(email);
        var isPasswordValid = BCryptNet.Verify(password, account.Password);

        if (!isPasswordValid)
        {
            throw new AccountNotFoundException();
        }

        await _emailSender.SendEmailAsync(account.Email,
            new SendSignedInEmailPayload() { LoginDate = DateTime.UtcNow, });

        var token = BuildToken(account);

        return token;
    }

    private JwtBuilder CreateJwtBuilder()
    {
        var jwtSecret = _jwtSettings.Value.Secret;
        if (string.IsNullOrEmpty(jwtSecret))
        {
            throw new InvalidOperationException("JWT Secret not configured");
        }

        return JwtBuilder.Create()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(jwtSecret);
    }

    private string BuildToken(Account account)
    {
        var expirationHours = _jwtSettings.Value.ExpirationHours;
        var expirationTime = DateTime.UtcNow.AddHours(expirationHours);

        return CreateJwtBuilder()
            .AddClaim("exp", ((DateTimeOffset)expirationTime).ToUnixTimeSeconds())
            .AddClaim("iat", ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds())
            .AddClaim("sub", account.Id.ToString())
            .AddClaim("email", account.Email)
            .Encode();
    }

    private Guid GetAccountIDFromToken(string token)
    {
        try
        {
            var payload = CreateJwtBuilder()
                .MustVerifySignature()
                .Decode<IDictionary<string, object>>(token);

            if (!payload.TryGetValue("sub", out var subClaim) || subClaim == null)
            {
                throw new InvalidBearerException("Token does not contain valid subject claim");
            }

            if (!Guid.TryParse(subClaim.ToString(), out var accountId))
            {
                throw new InvalidBearerException("Token subject claim is not a valid GUID");
            }

            return accountId;
        }
        catch (Exception ex) when (!(ex is InvalidBearerException))
        {
            throw new InvalidBearerException("Invalid or expired token");
        }
    }
}
