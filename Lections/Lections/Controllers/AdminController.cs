using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lections.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lections.Controllers
{
    public class AdminController : Controller
    {
        private static DatabaseContext db;

        public AdminController(DatabaseContext context)
        {
            db = context;
        }

        public IActionResult Users()
        {
            return View(db.Users);
        }

        public IActionResult Lections(string username)
        { 
          
            return View();
        }

        public IActionResult MakeAdmin(string username)
        {
            foreach (User user in db.Users)
            {
                if (user.username.Equals(username))
                {
                    user.isAdmin = true;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Users", "Admin");
        }

        public IActionResult DeleteAdmin(string username)
        {
            foreach (User user in db.Users)
            {
                if (user.username.Equals(username))
                {
                    user.isAdmin = false;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Users", "Admin");
        }

        [HttpPost]
        public IActionResult SaveEdit([FromForm]User user)
        {
            foreach(User u in db.Users)
            {
                if (u.username.Equals(user.username))
                {
                    u.firstname = user.firstname;
                    u.lastname = user.lastname;
                    u.email = user.email;
                    u.password = user.password;
                    u.ammountStars = user.ammountStars;
                    u.ammountLections = user.ammountLections;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Users", "Admin");
        }

        [HttpGet]
        public IActionResult AdminEdit(string username)
        {
            foreach(User user in db.Users)
            {
                if(user.username.Equals(username))
                {
                    return View("EditUser", user);
                }
            }
            return View("EditUser");
        }

        public IActionResult AdminDelete(string username)
        {
            foreach (User user in db.Users)
            {
                if (user.username.Equals(username))
                {
                    db.Users.Remove(user);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Users", "Admin");
        }

    }
}