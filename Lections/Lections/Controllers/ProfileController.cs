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

        public IActionResult checkAdminL()
        {
            foreach (User user in db.Users)
            {
                if (user.username.Equals(User.Identity.Name) && user.isAdmin)
                {
                    return RedirectToAction("Lections", "Admin");
                }
            }
            return RedirectToAction("Index", "Home");
        }


        public IActionResult checkAdminU()
        {
            foreach (User user in db.Users)
            {
                if (user.username.Equals(User.Identity.Name) && user.isAdmin)
                {
                    return RedirectToAction("Users", "Admin");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            foreach (User us in db.Users)
            {
                if (us.username.Equals(User.Identity.Name))
                {
                    return View(us);
                }
            }

            User user = new User();
            user.username = User.Identity.Name;
            db.Users.Add(user);
            db.SaveChanges();

            return RedirectToAction("Index", "Profile");
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