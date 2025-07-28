using EnviroSense.Web.Entities;
using EnviroSense.Web.Services;
using EnviroSense.Web.ViewModels.Accounts;
using Microsoft.AspNetCore.Mvc;
using BCryptNet = BCrypt.Net.BCrypt;

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
            string hashedPassword = BCryptNet.HashPassword(model.Password, 10);
            var singUpModel = new Account()
            {
                Email = model.Email,
                Password = hashedPassword,
                UpdatedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            await _accountService.Add(singUpModel);

            return RedirectToAction("Index", "Home");
        }
    }
}
