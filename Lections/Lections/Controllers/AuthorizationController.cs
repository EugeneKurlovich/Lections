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

namespace Lections.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IAuthenticationSchemeProvider asp;
        private static DatabaseContext db;
        static string log;
        UnitOfWork unitOfWork;

        public AuthorizationController(IAuthenticationSchemeProvider a, DatabaseContext context)
        {
            asp = a;
            db = context;
            unitOfWork = new UnitOfWork();
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
                foreach (User u in db.Users)
                {
                    if (u.username.Equals(user.username) || u.email.Equals(user.email))
                    {
                        ViewBag.message = "Username or email already exists";
                        return View("Confirmed");
                    }
                }

                var allSchemeProvider = (await asp.GetAllSchemesAsync())
                   .Select(n => n.DisplayName).Where(n => !String.IsNullOrEmpty(n));

                db.Users.Add(user);
                EmailSender es = new EmailSender();
                string url = Url.Action("confirmedEmail", "Authorization", new { Email = user.email }, Request.Scheme);
                es.ConfirmEmail(user.email, url);
                await db.SaveChangesAsync();
                ViewBag.message = "Your account was registered. Sign In";
                return View("Index", allSchemeProvider);
        }


        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.username),
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
            var uu = from u in db.Users where u.username.Equals(username) && u.password.Equals(password) select u;
            foreach (User u in uu)
            {
                if (u.emailConfirmed)
                {
                    await Authenticate(u);
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.message = "Incorrect data / no confirmed account";
            var allSchemeProvider = (await asp.GetAllSchemesAsync())
               .Select(n => n.DisplayName).Where(n => !String.IsNullOrEmpty(n));
            return View("Index", allSchemeProvider);
        }

        public IActionResult confirmedEmail()
        {
            var a = from u in db.Users where u.username.Equals(log) select u;
            foreach (User user in a)
            {
                user.emailConfirmed = true;
            }
            db.SaveChanges();

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