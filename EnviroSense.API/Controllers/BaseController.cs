using EnviroSense.API.Models.Core;
using Microsoft.AspNetCore.Mvc;

namespace EnviroSense.API.Controllers;

public abstract class BaseController : Controller
{
    public IActionResult Success<T>(T result)
    {
        return Json(result);
    }

    protected IActionResult SuccessfullyCreated<T>(T result)
    {
        return new JsonResult(result)
        {
            StatusCode = 201
        };
    }

    public IActionResult ValidationError()
    {
        return BadRequest(new ValidationError(ModelState));
    }
    public IActionResult EmailIsTaken()
    {
        return BadRequest(new
        {
            message = "Email is already taken"
        });
    }

    public IActionResult CustomValidationError(params BaseError.Entry[] entries)
    {
        return BadRequest(new ValidationError(entries));
    }
}
