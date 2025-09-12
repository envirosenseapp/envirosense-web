using System.ComponentModel.DataAnnotations;
using EnviroSense.API.Authentication;
using EnviroSense.API.Models.Auth;
using EnviroSense.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EnviroSense.API.Controllers;

[Route("/auth")]
public class AuthController : BaseController
{
    private readonly ITokenAuthentication _tokenAuthentication;

    public AuthController(ITokenAuthentication tokenAuthentication)
    {
        _tokenAuthentication = tokenAuthentication;
    }

    [HttpPost("sign_in")]
    public async Task<IActionResult> SignIn([FromBody][Required] SignInRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationError();
        }

        try
        {
            var token = await _tokenAuthentication.Login(request.Email, request.Password);

            var response = new SignInResponse
            {
                Token = token
            };

            return Success(response);
        }
        catch (AccountNotFoundException)
        {
            return CustomValidationError(
                new Models.Core.BaseError.Entry("email", "invalid credentials")
            );
        }
    }
}
