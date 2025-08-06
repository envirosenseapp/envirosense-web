using EnviroSense.Web.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnviroSense.Web.Filters;

public class AccessTrackingFilter : IAsyncActionFilter
{
    private readonly IAccessService _accessService;
    public AccessTrackingFilter(IAccessService accessService)
    {
        _accessService = accessService;
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await _accessService.Create();
        await next();
    }
}
