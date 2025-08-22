using EnviroSense.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnviroSense.API.Filters;

public class NoApiKeyProtected : IActionFilter
{
    private readonly IAccountService _accountService;

    public NoApiKeyProtected(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
        //TODO
    }

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
    }
}
