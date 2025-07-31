using EnviroSense.Web.Entities;
using EnviroSense.Web.Exceptions;
using EnviroSense.Web.Repositories;

namespace EnviroSense.Web.Services;

public class AccessService : IAccessService
{
    private readonly IAccessRepository _accessRepository;
    private readonly IHttpContextAccessor _httpContextAccesor;

    public AccessService(IAccessRepository accessRepository, IHttpContextAccessor httpContextAccesor)
    {
        _accessRepository = accessRepository;
        _httpContextAccesor = httpContextAccesor;
    }

    private Guid? GetAccountId()
    {
        var httpContext = _httpContextAccesor.HttpContext;

        if (httpContext == null || httpContext.Session == null)
        {
            return null;
        }

        var session = httpContext.Session;
        var accountId = session.GetString("authenticated_account_id");
        if (accountId == null)
        {
            return null;
        }

        return Guid.Parse(accountId);
    }

    public async Task<Access> Create()
    {
        var httpContext = _httpContextAccesor.HttpContext;
        if (httpContext?.Connection.RemoteIpAddress == null)
        {
            throw new IpAddressNotFoundException();
        }

        var ipAddress = httpContext.Connection.RemoteIpAddress.ToString();
        var account

        var access = new Access
        {
            Id = Guid.NewGuid(),
            Account = account
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
