using LearningPlatform.Interfaces;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearningPlatform.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AccountController(IUsersRepository usersRepository, IPasswordHasher passwordHasher)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Shedule");
            var loginVM = new LoginUserViewModel();
            return View(loginVM);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserViewModel loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _usersRepository.GetByEmail(loginVM.Email);
            if (user == null)
            {
                ViewData["ValidationMessage"] = "Користувач із таким Email не знайден";
                return View(loginVM);
            }
            var result = _passwordHasher.Verify(loginVM.Password, user.PasswordHash);
            if (!result)
            {
                ViewData["ValidationMessage"] = "Неправильний пароль";
                return View(loginVM);
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = loginVM.RememberMe,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

            return RedirectToAction("Index", "Shedule");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
