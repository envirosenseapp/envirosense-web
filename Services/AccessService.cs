using EnviroSense.Web.Entities;
using EnviroSense.Web.Repositories;
using EnviroSense.Web.Exceptions;

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
    public async Task<Access> Create()
    {
        var httpContext = _httpContextAccesor.HttpContext;
        var ipAddress = httpContext.Connection.RemoteIpAddress.ToString();
        if (ipAddress == null)
        {
            throw new IpAddressNotFoundException();
        }

        var access = new Access
        {
            Id = Guid.NewGuid(),
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
