using EnviroSense.Application.Authentication;
using EnviroSense.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnviroSense.Web.Filters;

public class SignedInFilter : IActionFilter
{
    private readonly IAuthenticationRetriever _authenticationRetriever;

    public SignedInFilter(IAuthenticationRetriever authenticationRetriever)
    {
        _authenticationRetriever = authenticationRetriever;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (_authenticationRetriever.GetCurrentAccountId() == null)
        {
            context.Result = new RedirectToActionResult("SignIn", "Accounts", null);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
