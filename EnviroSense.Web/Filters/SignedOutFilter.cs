using EnviroSense.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnviroSense.Web.Filters;

public class SignedOutFilter : IActionFilter
{
    private readonly IAccountService _accountService;

    public SignedOutFilter(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (!string.IsNullOrEmpty(_accountService.GetAccountIdFromSession()))
        {
            filterContext.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
    }
}
