using ExpirationDate.Models;
using ExpirationDate.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ExpirationDate.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController<HomeController>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender, IStringLocalizer<HomeController> localizer) : base(localizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Register()
        {
            SetViewData();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            if (token == null || email == null)
                
                return RedirectToAction("Index", "Home");

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return RedirectToAction("Index", "Home");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            ViewData["Success"] = result.Succeeded;
            return View("ConfirmEmail");
        }

        [HttpGet]
        public IActionResult Login()
        {
            SetViewData();
            return View();
        }

        [HttpGet]
        public IActionResult PrintGreetings()
        {
            SetViewData();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("PrintGreetings", "Account");
                }
                else
                {
                    if (result.IsLockedOut)
                    {
                        return RedirectToAction("Privacy", "Home");
                    }
                    else if (result.RequiresTwoFactor)
                    {
                        // Обработка двухфакторной аутентификации
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
/*
        [HttpGet]
        public IActionResult ErrorLog()
        {            
            var errors = new List<string>
            {
                "Sample error 1",
                "Sample error 2",
            };

            ViewData["Account"] = errors;//Errors
            return View();
        }
*/        
    }
}
