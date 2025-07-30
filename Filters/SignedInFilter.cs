using EnviroSense.Web.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnviroSense.Web.Filters;

public class SignedInFilter : IActionFilter
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SignedInFilter(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext == null || httpContext.Session == null)
        {
            throw new SessionIsNotAvailableException();
        }

        var session = httpContext.Session;
        var accountId = session.GetString("authenticated_account_id");

        if (string.IsNullOrEmpty(accountId))
        {
            context.Result = new RedirectToActionResult("SignIn", "Accounts", null);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
