using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lections.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lections.Controllers
{
    public class ProfileController : Controller
    {

        private static DatabaseContext db;

        public ProfileController(DatabaseContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            foreach (User user in db.Users)
            {
                if (user.username.Equals(User.Identity.Name))
                {
                    return View(user);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser([FromForm] User user)
        {
            foreach(User i in db.Users)
            {
                if (i.username.Equals(User.Identity.Name))
                { 
                    i.firstname = user.firstname;
                    i.lastname = user.lastname;
                    i.email = user.email;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Profile");
        }
    }
}