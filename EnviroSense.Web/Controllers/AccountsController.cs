using EnviroSense.Web.Exceptions;
using EnviroSense.Web.Filters;
using EnviroSense.Web.Services;
using EnviroSense.Web.ViewModels.Accounts;
using Microsoft.AspNetCore.Mvc;


namespace EnviroSense.Web.Controllers
{
    [TypeFilter(typeof(SignedOutFilter))]
    public class AccountsController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IEmailClient _emailClient;

        public AccountsController(IAccountService accountService, IEmailClient emailClient)
        {
            _accountService = accountService;
            _emailClient = emailClient;
        }

        public ActionResult SignUp()
        {
            ViewBag.PasswordDontMatch = TempData["PasswordDontMatch"];
            ViewBag.EmailValidator = TempData["ExistingEmail"];
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(SignUpViewModel model)
        {
            var isEmailTaken = await _accountService.IsEmailTaken(model.Email);
            if (!ModelState.IsValid)
            {
                return RedirectToAction("SignUp");
            }

            if (isEmailTaken)
            {
                TempData["ExistingEmail"] = "An user with this email allready exists";
                return RedirectToAction("SignUp");
            }

            var account = await _accountService.Add(model.Email, model.Password);
            await _emailClient.SendMail("Welcome to EnviroSense!",
                "Thank you for registering with us.", account.Email);

            return RedirectToAction("Index", "Home");
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
    }
}
