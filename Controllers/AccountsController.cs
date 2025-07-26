using EnviroSense.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnviroSense.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        public ActionResult SignUp()
        {
            ViewBag.PasswordDontMatch = TempData["PasswordDontMatch"];
            ViewBag.EmailValidator = TempData["ExistingEmail"];
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                TempData["PasswordDontMatch"] = "Confirm password doesn't match.";
                return RedirectToAction("SignUp");
            }

            var isEmailTaken = await _accountService.IsEmailTaken(email);
            if (isEmailTaken)
            {
                TempData["ExistingEmail"] = "An user with this email allready exists";
                return RedirectToAction("SignUp");
            }

            await _accountService.Add(email, password);

            return RedirectToAction("Index", "Home");
        }

    }
}
