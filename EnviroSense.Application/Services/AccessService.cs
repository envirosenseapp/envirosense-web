using EnviroSense.Application.Authentication;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;
using Microsoft.AspNetCore.Http;

namespace EnviroSense.Application.Services;

public class AccessService : IAccessService
{
    private readonly IAccessRepository _accessRepository;
    private readonly IHttpContextAccessor _httpContextAccesor;
    private readonly IAccountService _accountService;
    private readonly IAuthenticationRetriever _authenticationRetriever;

    public AccessService(IAccessRepository accessRepository, IHttpContextAccessor httpContextAccesor,
        IAccountService accountService,
        IAuthenticationRetriever authenticationRetriever
    )
    {
        _accessRepository = accessRepository;
        _httpContextAccesor = httpContextAccesor;
        _accountService = accountService;
        _authenticationRetriever = authenticationRetriever;
    }

    public async Task<Access> Create(
        )
    {
        var httpContext = _httpContextAccesor.HttpContext;
        if (httpContext?.Connection.RemoteIpAddress == null)
        {
            throw new IpAddressNotFoundException();
        }

        var ipAddress = httpContext.Connection.RemoteIpAddress.ToString();
        Guid? accountId = _authenticationRetriever.GetCurrentAccountId();
        Account? account = null;

        if (accountId != null)
        {
            account = await _accountService.GetAccountById(accountId.Value);
        }

        var access = new Access
        {
            Id = Guid.NewGuid(),
            Account = account,
            CreatedAt = DateTime.UtcNow,
            IpAddress = ipAddress,
            Client = httpContext.Request.Headers["User-Agent"].ToString(),
            Resource = httpContext.Request.Path
        };

        var createdAccess = await _accessRepository.CreateAsync(access);

        return createdAccess;
    }

    public Task<int> Count()
    {
        return _accessRepository.Count();
    }

    public Task<List<Access>> List()
    {
        return _accessRepository.ListAsync();
    }
}
