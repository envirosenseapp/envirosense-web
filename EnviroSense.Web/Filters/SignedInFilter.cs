using EnviroSense.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnviroSense.Web.Filters;

public class SignedInFilter : IActionFilter
{
    private readonly IAccountService _accountService;

    public SignedInFilter(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (string.IsNullOrEmpty(_accountService.GetAccountIdFromSession()))
        {
            context.Result = new RedirectToActionResult("SignIn", "Accounts", null);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
