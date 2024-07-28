using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebApp.HttpClients;
using WebApp.Models;
using System.Security.Claims;
using System.Text.Json;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        AuthService _authService;
        public AccountController(AuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Authentication Cookie.
        /// </summary>
        /// <param name="user"></param>
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

            //// HttpContext is accessible in Asp.Net Core on the same object we keeping it and hence,
            ///  SignInAsync method can access Logged in User Details across the application.
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _authService.Login(loginModel).Result;
                if (user != null)
                {
                    GenerateTicket(user);

                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);

                    if (user.Roles.Contains("User"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "User" });
                    }
                }
                }
            return View(loginModel);
        }

        /// <summary>
        /// To Do for SingUp - Add Razor View
        /// </summary>
        /// <returns></returns>
        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            //// By calling the logout method, It removed the authentication cookies.
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
