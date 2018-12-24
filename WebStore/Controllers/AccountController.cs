using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModel.Account;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(
                new LoginViewModel()
                {
                    ReturnUrl = returnUrl
                }
            );
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Проверяем логин/пароль пользователя
                var loginResult = await _signInManager.PasswordSignInAsync(model.UserName,
                    model.Password, model.RememberMe, lockoutOnFailure: false);

                // Если проверка успешна
                if (loginResult.Succeeded)
                {
                    // и ReturnUrl - локальный
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        // перенаправляем туда, откуда пришли
                        return Redirect(model.ReturnUrl);
                    }
                    // иначе на главную
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Вход невозможен");
            return View(model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterUserViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // создаем сущность пользователь
                var user = new User { UserName = model.UserName, Email = model.Email };

                // используем менеджер для проверки и создания пользователя в БД
                var createResult = await _userManager.CreateAsync(user, model.Password);
                if (createResult.Succeeded)
                {
                    // если успешно, производим логин
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // выводим ошибки
                    foreach (var identityError in createResult.Errors)
                    {
                        ModelState.AddModelError("", identityError.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}