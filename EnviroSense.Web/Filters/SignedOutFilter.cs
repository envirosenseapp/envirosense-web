using EnviroSense.Application.Authentication;
using EnviroSense.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnviroSense.Web.Filters;

public class SignedOutFilter : IAsyncActionFilter
{
    private readonly IAuthenticationRetriever _authenticationRetriever;

    public SignedOutFilter(IAuthenticationRetriever authenticationRetriever)
    {
        _authenticationRetriever = authenticationRetriever;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var currentAccountId = await _authenticationRetriever.GetCurrentAccountId();
        if (currentAccountId != null)
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
            return;
        }

        await next();
    }
}
