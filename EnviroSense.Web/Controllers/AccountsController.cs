using EnviroSense.Application.Services;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Web.Filters;
using EnviroSense.Web.ViewModels.Accounts;
using Microsoft.AspNetCore.Mvc;


namespace EnviroSense.Web.Controllers
{
    [TypeFilter(typeof(SignedOutFilter))]
    public class AccountsController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IAccountPasswordResetService _accountPasswordResetService;

        public AccountsController(IAccountService accountService,
            IAccountPasswordResetService accountPasswordResetService)
        {
            _accountService = accountService;
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

            await _accountService.Add(model.Email, model.Password);
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

            await _accountPasswordResetService.ResetPasswordAsync(model.Email);
            ViewBag.SendEmailMessage = "An email was sent to " + model.Email;
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
    }
}
