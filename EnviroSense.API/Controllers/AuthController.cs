using System.ComponentModel.DataAnnotations;
using EnviroSense.API.Authentication;
using EnviroSense.API.Models.Auth;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EnviroSense.API.Controllers;

[Route("/auth")]
public class AuthController : BaseController
{
    private readonly ITokenAuthentication _tokenAuthentication;
    private readonly IAccountService _accountService;

    public AuthController(ITokenAuthentication tokenAuthentication, IAccountService accountService)
    {
        _tokenAuthentication = tokenAuthentication;
        _accountService = accountService;
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

    [HttpPost("sign_up")]

    public async Task<IActionResult> SignUp([FromBody][Required] SignUpRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationError();
        }
        var isEmailTaken = await _accountService.IsEmailTaken(request.Email);
        if (isEmailTaken)
        {
            return EmailIsTaken();
        }
        var account = await _accountService.Add(request.Email, request.Password);

        return SuccessfullyCreated(account);
    }


}
