using EnviroSense.Application.Authentication;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Web.Authentication;
using EnviroSense.Web.Filters;
using EnviroSense.Web.ViewModels.Accounts;
using Microsoft.AspNetCore.Mvc;


namespace EnviroSense.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IAccountPasswordResetService _accountPasswordResetService;
        private readonly ISessionAuthentication _sessionAuthentication;

        public AccountsController(IAccountService accountService,
            IAccountPasswordResetService accountPasswordResetService,
            ISessionAuthentication sessionAuthentication
        )
        {
            _accountService = accountService;
            _accountPasswordResetService = accountPasswordResetService;
            _sessionAuthentication = sessionAuthentication;
        }

        [TypeFilter(typeof(SignedOutFilter))]
        public ActionResult SignUp()
        {
            return View();
        }

        [TypeFilter(typeof(SignedOutFilter))]
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

            await _accountService.Add(model.Email, model.Password);
            return RedirectToAction("SignIn");
        }

        [TypeFilter(typeof(SignedOutFilter))]
        [HttpGet]
        public ActionResult SignIn()
        {
            ViewBag.Info = TempData["Info"];
            return View();
        }

        [TypeFilter(typeof(SignedOutFilter))]
        [HttpPost]
        public async Task<ActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _sessionAuthentication.Login(model.Email, model.Password);
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

        [TypeFilter(typeof(SignedOutFilter))]
        [HttpGet]
        public Task<ActionResult> ForgotPassword()
        {
            return Task.FromResult<ActionResult>(View());
        }

        [TypeFilter(typeof(SignedOutFilter))]
        [HttpPost]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _accountPasswordResetService.ResetPasswordAsync(model.Email);
            ViewBag.SendEmailMessage = "An email was sent to " + model.Email;
            return View();
        }

        [TypeFilter(typeof(SignedOutFilter))]
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [TypeFilter(typeof(SignedOutFilter))]
        [HttpPost]
        public async Task<ActionResult> ResetPassword(Guid id, ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                await _accountPasswordResetService.Reset(id,
                    model.NewPassword);
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

        [TypeFilter(typeof(SignedInFilter))]
        [HttpGet]
        public async Task<ActionResult> Settings()
        {
            var account = await _sessionAuthentication.GetCurrentAccount();

            var viewModel = new SettingsViewModel
            {
                Email = account.Email,
                UpdatedAt = account.UpdatedAt,
                CreatedAt = account.CreatedAt,
            };
            return View(viewModel);
        }
        [TypeFilter(typeof(SignedInFilter))]
        [HttpPost]
        public async Task<ActionResult> Settings(SettingsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Invalid data";
                return View(model);
            }
            var accountId = await _sessionAuthentication.GetCurrentAccountId();
            await _accountService.ResetPasswordFromSettings(accountId.Value, model.NewPassword);
            ViewBag.Message = "Password reset successfully";
            return View(model);
        }

        [TypeFilter(typeof(SignedInFilter))]
        public ActionResult LogOut()
        {
            _sessionAuthentication.Logout();
            return RedirectToAction("SignIn");
        }
    }
}
