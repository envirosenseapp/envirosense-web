using System.ComponentModel.DataAnnotations;
using EnviroSense.API.Authentication;
using EnviroSense.API.Models.Auth;
using EnviroSense.API.Models.ResetPassword;
using EnviroSense.API.Models.RessetPassword;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EnviroSense.API.Controllers;

[Route("/auth")]
public class AuthController : BaseController
{
    private readonly ITokenAuthentication _tokenAuthentication;
    private readonly IAccountPasswordResetService _accountPasswordResetService;
    private readonly IAccountService _accountService;

    public AuthController(ITokenAuthentication tokenAuthentication, IAccountPasswordResetService accountPasswordResetService, IAccountService accountService)
    {
        _tokenAuthentication = tokenAuthentication;
        _accountPasswordResetService = accountPasswordResetService;
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
            return CustomValidationError(new Models.Core.BaseError.Entry("email", "email already taken"));
        }
        var account = await _accountService.Add(request.Email, request.Password);

        return SuccessfullyCreated(account);
    }

    [HttpPost("forgot_password")]

    public async Task<IActionResult> ForgotPassword([FromBody][Required] ForgotPasswordRequest request)
    {
        await _accountPasswordResetService.ResetPasswordAsync(request.Email);
        return NoContent();
    }

    [HttpPost("reset_password/{id}")]

    public async Task<IActionResult> ResetPassword([FromRoute][Required] Guid id,
        [FromBody][Required] ResetPasswordRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationError();
        }

        try
        {
            await _accountPasswordResetService.Reset(id, request.NewPassword);
            return NoContent();
        }
        catch (ResetPasswordLinkExpiredException)
        {
            return CustomValidationError(new Models.Core.BaseError.Entry("link", "Link expired"));
        }
        catch (ResetPasswordAlreadyUsedException)
        {
            return CustomValidationError(new Models.Core.BaseError.Entry("link", "Link already used"));
        }

    }
}
