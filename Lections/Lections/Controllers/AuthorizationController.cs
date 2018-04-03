using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lections.Models;
using Lections.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Lections.Repository;

namespace Lections.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IAuthenticationSchemeProvider asp;
        static string log;
        UserService uS;
        DatabaseContext db;

        public AuthorizationController(IAuthenticationSchemeProvider a, DatabaseContext context)
        {
            asp = a;
            uS = new UserService(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Sign(String provider)
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, provider);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration([FromForm] User user)
        {
           log = user.username;
           if (uS.checkExist(user.username, user.email))
            {
                uS.createUser(user);
                uS.Save();
                var allSchemeProvider = (await asp.GetAllSchemesAsync())
             .Select(n => n.DisplayName).Where(n => !String.IsNullOrEmpty(n));

                EmailSender es = new EmailSender();
                string url = Url.Action("confirmedEmail", "Authorization", new { Email = user.email }, Request.Scheme);
                es.ConfirmEmail(user.email, url);
                ViewBag.message = "Your account was registered. Sign In";
                return View("Index", allSchemeProvider);
            }
            else
            {
                ViewBag.message = "Username or email already exists";
                return View("Confirmed");
            }
        }


        private async Task Authenticate(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, username),
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            if (uS.checkRegisteredUser(username,password))
            {
                await Authenticate(username);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.message = "Incorrect data / no confirmed account";
                var allSchemeProvider = (await asp.GetAllSchemesAsync())
                   .Select(n => n.DisplayName).Where(n => !String.IsNullOrEmpty(n));
                return View("Index", allSchemeProvider);
            }
        }

        public IActionResult confirmedEmail()
        {
            uS.confirmEmail(log);
            uS.Save();
            ViewBag.Message = "Congratulations, your account is verified.";
            return View("Confirmed");
        }

        public async Task<IActionResult> Login()
        {
            var allSchemeProvider = (await asp.GetAllSchemesAsync())
               .Select(n => n.DisplayName).Where(n => !String.IsNullOrEmpty(n));
            return View("Index", allSchemeProvider);
        }

        public IActionResult SignUp()
        {
            return View("SignUp");
        }
    }
}