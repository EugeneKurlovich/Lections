using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lections.Models;
using Lections.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lections.Controllers
{
    public class ProfileController : Controller
    {
        UserService uS;

        public ProfileController(DatabaseContext context)
        {
            uS = new UserService(context);
        }

        public IActionResult checkAdminL()
        {
                if (uS.isAdmin(User.Identity.Name))
                    return RedirectToAction("Lections", "Admin");
                else
                return RedirectToAction("Index", "Home"); 
        }


        public IActionResult checkAdminU()
        {
            if (uS.isAdmin(User.Identity.Name))
                return RedirectToAction("Users", "Admin");
            else
                return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            if(!uS.checkExist(User.Identity.Name, "null"))
            {
                return View(uS.getUserbyName(User.Identity.Name));
            }
            else
            {
                User user = new User(User.Identity.Name);
                uS.createUser(user);
                uS.Save();
                return RedirectToAction("Index", "Profile");
            }
            //foreach (User us in db.Users)
            //{
            //    if (us.username.Equals(User.Identity.Name))
            //    {
            //        return View(us);
            //    }
            //}

            //User user = new User();
            //user.username = User.Identity.Name;
            //db.Users.Add(user);
            //db.SaveChanges(); 
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser([FromForm] User user)
        {
           User us = uS.getUserbyName(User.Identity.Name);
            us.firstname = user.firstname;
            us.lastname = user.lastname;
            us.email = user.email;
            uS.updateProfile(us);
            uS.Save();
            return RedirectToAction("Index", "Profile");
            //foreach(User i in db.Users)
            //{
            //    if (i.username.Equals(User.Identity.Name))
            //    { 
            //        i.firstname = user.firstname;
            //        i.lastname = user.lastname;
            //        i.email = user.email;
            //    }
            //}
            //db.SaveChanges();
            //return RedirectToAction("Index", "Profile");
        }
    }
}