using System.Net;
using EnviroSense.API.Exceptions;
using EnviroSense.API.Models.Core;
using EnviroSense.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnviroSense.API.Filters;

public class HandleGenericErrors : IActionFilter
{
    private readonly ILogger<HandleGenericErrors> _logger;

    public HandleGenericErrors(ILogger<HandleGenericErrors> logger)
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
            if (
                context.Exception is UnauthenticatedException
                ||
                context.Exception is InvalidApiKeyException
            )
            {
                HandleUnauthenticatedError(context);
            }
            else if (
                context.Exception is AccessToForbiddenEntityException
            )
            {
                HandleForbiddenError(context);
            }
            else
            {
                HandleGenericError(context);
            }
        }
    }

    private void HandleUnauthenticatedError(ActionExecutedContext context)
    {
        var result = new JsonResult(new UnauthenticatedError());
        result.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Result = result;

        _logger.LogError(context.Exception, "Unexpected error occured");
    }

    private void HandleForbiddenError(ActionExecutedContext context)
    {
        var result = new JsonResult(new ForbiddenError());
        result.StatusCode = (int)HttpStatusCode.Forbidden;
        context.Result = result;

        _logger.LogError(context.Exception, "Unexpected error occured");
    }

    private void HandleGenericError(ActionExecutedContext context)
    {
        var result = new JsonResult(new InternalServiceError());
        result.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = result;

        _logger.LogError(context.Exception, "Unexpected error occured");
    }
}
