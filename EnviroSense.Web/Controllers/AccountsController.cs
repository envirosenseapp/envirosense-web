using EnviroSense.Application.Services;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Clients;
using EnviroSense.Web.Filters;
using EnviroSense.Web.ViewModels.Accounts;
using Microsoft.AspNetCore.Mvc;


namespace EnviroSense.Web.Controllers
{
    [TypeFilter(typeof(SignedOutFilter))]
    public class AccountsController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IEmailClient _emailClient;
        private readonly IAccountPasswordResetService _accountPasswordResetService;

        public AccountsController(IAccountService accountService, IEmailClient emailClient,
            IAccountPasswordResetService accountPasswordResetService)
        {
            _accountService = accountService;
            _emailClient = emailClient;
            _accountPasswordResetService = accountPasswordResetService;
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var isEmailTaken = await _accountService.IsEmailTaken(model.Email);
            if (isEmailTaken)
            {
                ModelState.AddModelError("", "An user with this email already exists");
                return View();
            }

            var account = await _accountService.Add(model.Email, model.Password);
            await _emailClient.SendMail("Welcome to EnviroSense!",
                "Thank you for registering with us.", account.Email);

            return RedirectToAction("SignIn");
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            ViewBag.Info = TempData["Info"];
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _accountService.Login(model.Email, model.Password);
                await _emailClient.SendMail("Welcome to EnviroSense!",
                    "You are successfully signed in.", model.Email);
                return RedirectToAction("Index", "Home");
            }
            catch (AccountNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (SessionIsNotAvailableException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public Task<ActionResult> ForgotPassword()
        {
            return Task.FromResult<ActionResult>(View());
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var securityCode = await _accountPasswordResetService.ResetPasswordAsync(model.Email);
            ViewBag.SendEmailMessage = "An email was sent to " + model.Email;
            if (securityCode == null)
            {
                return View();
            }

            var resetLink = $"http://localhost:5276/Accounts/ResetPassword/{securityCode}";

            await _emailClient.SendMail(
                "Reset Password",
                $@"<p>Hi {model.Email},</p>
       <p>We received a request to reset your password for your account on EnviroSense.</p>
       <p>To create a new password, click the link below:<br/>
       <a href=""{resetLink}"">Reset it here</a></p>
       <p>Thanks,<br/>The EnviroSense Team</p>",
                model.Email
            );

            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ResetPassword(Guid id, ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var updatedAccount =
                    await _accountPasswordResetService.Reset(id,
                        model.NewPassword);
                var signInLink = $"http://localhost:5276/Accounts/SignIn";
                await _emailClient.SendMail(
                    "Password has been reset",
                    $@"<p>Hi {updatedAccount.Email},</p>
        <p>Your password has been reset.</p>
        <p>Log in with your new password now:<br/>
        <a href=""{signInLink}"">Sign in here</a></p>
        <p>Thanks,<br/>The EnviroSense Team</p>",
                    updatedAccount.Email
                );
            }
            catch (ResetPasswordLinkExpiredException)
            {
                ViewBag.Message = "Reset password link expired.";
                return View();
            }
            catch (ResetPasswordAlreadyUsedException)
            {
                ViewBag.Message = "Reset password already used.";
                return View();
            }
            catch (AccountPasswordResetNotFoundException)
            {
                ViewBag.Message = "No password found to reset.";
                return View();
            }
            TempData["Info"] = "You have successfully reset your password.";
            return RedirectToAction("SignIn");
        }
    }
}
