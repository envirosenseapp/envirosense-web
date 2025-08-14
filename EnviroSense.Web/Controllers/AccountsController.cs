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

            var (IsaccountToResset, securityCode) = await _accountPasswordResetService.ResetPasswordAsync(model.Email);
            ViewBag.SendEmailMessage = "An email was sent to " + model.Email;
            if (!IsaccountToResset)
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
        public IActionResult ResetPassword(Guid id)
        {
            var model = new ResetPasswordViewModel { SecurityCode = id };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var updatedAccount =
                    await _accountPasswordResetService.FetchAccountPasswordResetEntityById(model.SecurityCode,
                        model.NewPassword);
                var account = await _accountService.GetAccountById(updatedAccount.AccountId);
                var signInLink = $"http://localhost:5276/Accounts/SignIn";
                await _emailClient.SendMail(
                    "Password has been reset",
                    $@"<p>Hi {account.Email},</p>
        <p>Your password has been reset.</p>
        <p>Log in with your new password now:<br/>
        <a href=""{signInLink}"">Sign in here</a></p>
        <p>Thanks,<br/>The EnviroSense Team</p>",
                    account.Email
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

            return RedirectToAction("SignIn");
        }
    }
}
