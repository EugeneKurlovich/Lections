﻿using System;
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

namespace Lections.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IAuthenticationSchemeProvider asp;

        public AuthorizationController(IAuthenticationSchemeProvider a)
        {
            this.asp = a;
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
            EmailSender es = new EmailSender();
            string url = Url.Action("Login", "Authorization", new { Email = user.email }, Request.Scheme);
            es.ConfirmEmail(user.email, url);
            return RedirectToAction("Index", "Home", new { Email = user.email });
        }



        public async Task<IActionResult> Login()
        {
            var allSchemeProvider = (await asp.GetAllSchemesAsync())
               .Select(n => n.DisplayName).Where(n => !String.IsNullOrEmpty(n));


            return View("Index",allSchemeProvider);
        }

        public IActionResult SignUp()
        {
            return View("SignUp");
        }
    }
}