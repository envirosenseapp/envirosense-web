using EnviroSense.API.Exceptions;
using EnviroSense.Application.Authentication;
using EnviroSense.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnviroSense.API.Filters;

public class ApiKeyProtected : IAsyncActionFilter
{
    private readonly IAuthenticationContext _authenticationContext;

    public ApiKeyProtected(IAuthenticationContext authenticationContext)
    {
        _authenticationContext = authenticationContext;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var accountId = await _authenticationContext.CurrentAccountId();
        if (accountId == null)
        {
            throw new UnauthenticatedException();
        }

        await next();
    }
}
