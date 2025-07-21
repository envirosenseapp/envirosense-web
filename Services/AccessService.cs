using EnviroSense.Web.Entities;
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
    public async Task<Access> Create()
    {
        var httpContext = _httpContextAccesor.HttpContext;

        var access = new Access
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            IpAddress = httpContext.Connection.RemoteIpAddress.ToString(),
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