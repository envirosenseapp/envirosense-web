using EnviroSense.Application.Authentication;
using EnviroSense.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnviroSense.Web.Filters;

public class SignedOutFilter : IActionFilter
{
    private readonly IAuthenticationRetriever _authenticationRetriever;

    public SignedOutFilter(IAuthenticationRetriever authenticationRetriever)
    {
        _authenticationRetriever = authenticationRetriever;
    }

    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (_authenticationRetriever.GetAccountIdFromSession() != null)
        {
            filterContext.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
    }
}
