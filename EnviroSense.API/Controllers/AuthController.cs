using System.ComponentModel.DataAnnotations;
using EnviroSense.API.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace EnviroSense.API.Controllers;

[Route("/auth")]
public class AuthController : BaseController
{
    [HttpPost("sign_in")]
    public IActionResult SignIn([FromBody][Required] SignInRequest request)
    {
        return NoContent();
    }
}
