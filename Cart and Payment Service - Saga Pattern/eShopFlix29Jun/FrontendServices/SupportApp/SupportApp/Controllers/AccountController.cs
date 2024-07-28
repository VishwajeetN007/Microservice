using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SupportApp.HttpClients;
using SupportApp.Models;
using System.Security.Claims;
using System.Text.Json;

namespace SupportApp.Controllers
{
    public class AccountController : BaseController
    {
        AuthService _authService;
        public AccountController(AuthService authService)
        {
            _authService = authService;
        }

        private void GenerateTicket(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.UserData, JsonSerializer.Serialize(user)),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, string.Join(",",user.Roles))
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
            };
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
        }

        public IActionResult Login()
        {
            if (CurrentUser != null)
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = _authService.Login(loginModel).Result;
                if (user != null)
                {
                    GenerateTicket(user);
                    if (user.Roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                }
            }
            return View(loginModel);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
