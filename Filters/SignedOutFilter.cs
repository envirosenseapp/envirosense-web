using EnviroSense.Web.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnviroSense.Web.Filters;

public class SignedOutFilter : IActionFilter
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SignedOutFilter(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext == null || httpContext.Session == null)
        {
            throw new SessionIsNotAvailableException();
        }

        var session = httpContext.Session;
        var accountId = session.GetString("authenticated_account_id");
        if (!string.IsNullOrEmpty(accountId))
        {
            filterContext.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
    }
}
