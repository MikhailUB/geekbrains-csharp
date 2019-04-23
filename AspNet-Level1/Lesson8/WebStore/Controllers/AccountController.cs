using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

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

		public IActionResult Register() => View();

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegistrerUserViewModel model)
		{
			if (!ModelState.IsValid) // проверка данных формы
				return View(model);

			var newUser = new User
			{
				UserName = model.UserName
			};
			// Регистрируем в системе
			var createResult = await _userManager.CreateAsync(newUser, model.Password);
			if (createResult.Succeeded) // Если получилось
			{
				await _signInManager.SignInAsync(newUser, false); // Логиним на сайте

				return RedirectToAction("Index", "Home"); // редирект на главную
			}

			foreach (var error in createResult.Errors) // Если ошибки
				ModelState.AddModelError("", error.Description); // добавляем их к состоянию модели

			return View(model); // возвращем модель браузеру
		}

		public IActionResult Login() => View();

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel login)
		{
			if (!ModelState.IsValid)
				return View(login);

			var loginResult = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, login.RememberMe, false);
			if (loginResult.Succeeded)
			{
				if (Url.IsLocalUrl(login.ReturnUrl))
					return Redirect(login.ReturnUrl);

				return RedirectToAction("Index", "Home"); // на главную
			}

			ModelState.AddModelError("", "Неверное имя пользователя или пароль!");
			return View(login);
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();

			return RedirectToAction("Index", "Home");
		}

		public IActionResult AccessDenied() => View();
	}
}
