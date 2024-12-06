using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CRMSystem.ViewModels;
using CRMSystem.Models;


namespace CRMSystem.Controllers
{
    public sealed class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View(); 
        }
        public ViewResult Login(string returnUrl)
        {
            //return View("Index", new LoginViewModel() { ReturnUrl = returnUrl });
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }

        /*[HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Name);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();

                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        if (roles.Contains(Role.Admin))
                        {
                            return Redirect("/Admin/Index");
                        }
                        else if (roles.Contains(Role.Manager))
                        {
                            return Redirect("/Manager/Index");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Неверный пароль.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь не найден.");
                }
            }

            // В случае неудачи, возвращаем на страницу Index вместо Login
            return View("Index", model);
        }*/
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Name);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();

                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        if (roles.Contains(Role.Admin))
                        {
                            return Redirect("/Admin/Index");
                        }
                        else if (roles.Contains(Role.Manager))
                        {
                            return Redirect("/Manager/Index");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Неверный пароль.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь не найден.");
                }
            }

            return View(model);
        }
        public async Task<RedirectResult> Logout(string returnUrl = "/")
         {
             await _signInManager.SignOutAsync();

             return Redirect(returnUrl);
         }
    }
}

