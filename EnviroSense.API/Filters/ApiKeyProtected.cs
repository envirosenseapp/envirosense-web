using EnviroSense.API.Exceptions;
using EnviroSense.Application.Authentication;
using EnviroSense.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnviroSense.API.Filters;

public class ApiKeyProtected : IActionFilter
{
    private readonly IAuthenticationRetriever _authenticationRetriever;

    public ApiKeyProtected(IAuthenticationRetriever authenticationRetriever)
    {
        _authenticationRetriever = authenticationRetriever;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var accountId = _authenticationRetriever.GetCurrentAccountId();
        if (accountId == null)
        {
            throw new UnauthenticatedException();
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
