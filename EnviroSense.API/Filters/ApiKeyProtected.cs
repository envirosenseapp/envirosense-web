using EnviroSense.API.Exceptions;
using EnviroSense.Application.Authentication;
using EnviroSense.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnviroSense.API.Filters;

public class ApiKeyProtected : IAsyncActionFilter
{
    private readonly IAuthenticationRetriever _authenticationRetriever;

    public ApiKeyProtected(IAuthenticationRetriever authenticationRetriever)
    {
        _authenticationRetriever = authenticationRetriever;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var accountId = await _authenticationRetriever.GetCurrentAccountId();
        if (accountId == null)
        {
            throw new UnauthenticatedException();
        }

        await next();
    }
}
