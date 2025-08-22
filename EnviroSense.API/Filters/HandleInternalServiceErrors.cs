using System.Net;
using EnviroSense.API.Models.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnviroSense.API.Filters;

public class HandleInternalServiceErrors : IActionFilter
{
    private readonly ILogger<HandleInternalServiceErrors> _logger;

    public HandleInternalServiceErrors(ILogger<HandleInternalServiceErrors> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (!context.ExceptionHandled && context.Exception != null)
        {
            context.ExceptionHandled = true;

            var result = new JsonResult(new InternalServiceError());
            result.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = result;
            
            _logger.LogError(context.Exception,"Unexpected error occured");
        }
    }
}
