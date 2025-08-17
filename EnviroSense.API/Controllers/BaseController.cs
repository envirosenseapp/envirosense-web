using Microsoft.AspNetCore.Mvc;

namespace EnviroSense.API.Controllers;

public abstract class BaseController : Controller
{
    public IActionResult Success<T>(T result)
    {
        return Json(result);
    }
}
