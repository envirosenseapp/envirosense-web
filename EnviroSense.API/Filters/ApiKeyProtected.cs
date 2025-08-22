using EnviroSense.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnviroSense.API.Filters;

public class ApiKeyProtected : IActionFilter
{
    private readonly IAccountService _accountService;

    public ApiKeyProtected(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        //TODO
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
