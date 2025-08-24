using EnviroSense.Application.Authentication;
using EnviroSense.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnviroSense.Web.Filters;

public class SignedInFilter : IAsyncActionFilter
{
    private readonly IAuthenticationContext _authenticationContext;

    public SignedInFilter(IAuthenticationContext authenticationContext)
    {
        _authenticationContext = authenticationContext;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var currentAccountId = await _authenticationContext.CurrentAccountId();
        if (currentAccountId == null)
        {
            context.Result = new RedirectToActionResult("SignIn", "Accounts", null);
            return;
        }

        await next();
    }
}
