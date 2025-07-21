using Microsoft.AspNetCore.Mvc.Filters;
using EnviroSense.Web.Repositories;
using EnviroSense.Web.Entities;

namespace EnviroSense.Web.Filters;

public class AccessTrackingFilter : IAsyncActionFilter
{
    private readonly IAccessRepository _accessRepository;

    private readonly IHttpContextAccessor _httpContextAccesor;

    public AccessTrackingFilter(IAccessRepository accessRepository, IHttpContextAccessor httpContextAccesor)
    {
        _accessRepository = accessRepository;
        _httpContextAccesor = httpContextAccesor;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
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

        await _accessRepository.CreateAsync(access);
     

        await next();
    }


}
