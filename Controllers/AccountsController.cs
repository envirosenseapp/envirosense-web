using EnviroSense.Web.Services;
using EnviroSense.Web.ViewModels.Accounts;
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
            await _accountService.Add(model.Email, model.Password);

            return RedirectToAction("Index", "Home");
        }
    }
}
