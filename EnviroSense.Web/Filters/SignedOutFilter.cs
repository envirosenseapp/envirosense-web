using EnviroSense.Application.Authentication;
using EnviroSense.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnviroSense.Web.Filters;

public class SignedOutFilter : IAsyncActionFilter
{
    private readonly IAuthenticationContext _authenticationContext;

    public SignedOutFilter(IAuthenticationContext authenticationContext)
    {
        _authenticationContext = authenticationContext;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var currentAccountId = await _authenticationContext.CurrentAccountId();
        if (currentAccountId != null)
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
            return;
        }

        await next();
    }
}
