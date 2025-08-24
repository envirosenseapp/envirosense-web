using EnviroSense.API.Models;
using EnviroSense.API.Models.Core;
using Microsoft.AspNetCore.Mvc;

namespace EnviroSense.API.Controllers;

public abstract class BaseController : Controller
{
    public IActionResult Success<T>(T result)
    {
        return Json(result);
    }

    public IActionResult ValidationError()
    {
        return BadRequest(new ValidationError(ModelState));
    }

    public IActionResult CustomValidationError(params BaseError.Entry[] entries)
    {
        return BadRequest(new ValidationError(entries));
    }
}
