using Microsoft.AspNetCore.Mvc.Filters;
using EnviroSense.Web.Repositories;
using EnviroSense.Web.Entities;
using EnviroSense.Web.Services;

namespace EnviroSense.Web.Filters;

public class AccessTrackingFilter : IAsyncActionFilter
{
    private readonly IAccessService _accessService;

    private readonly IAccessRepository _accessRepository;

    public AccessTrackingFilter(IAccessService accessService, IAccessRepository accessRepository)
    {
        _accessService = accessService;
        _accessRepository = accessRepository;

    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var access = await _accessService.Create();
        await _accessRepository.CreateAsync(access);


        await next();
    }


}
