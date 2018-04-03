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
    public class AdminController : Controller
    {
        private static DatabaseContext db;
        UserService uS;

        public AdminController(DatabaseContext context)
        {
            uS = new UserService(context);
        }

        public IActionResult Users()
        {
            return View(uS.getAllUsers());
        }

        public IActionResult Lections(string username)
        {

            return View();
        }

        public IActionResult MakeAdmin(string username)
        {
            uS.makeAdm(uS.getUserbyName(username));
            uS.Save();
            return RedirectToAction("Users", "Admin");
        }

        public IActionResult DeleteAdmin(string username)
        {
            uS.delAdm(uS.getUserbyName(username));
            uS.Save();
            return RedirectToAction("Users", "Admin");
        }

        [HttpPost]
        public IActionResult SaveEdit([FromForm]User user)
        {
            User u = uS.getUserbyName(user.username);

            u.firstname = user.firstname;
            u.lastname = user.lastname;
            u.email = user.email;
            u.password = user.password;
            u.ammountStars = user.ammountStars;
            u.ammountLections = user.ammountLections;

            uS.updateAllProfile(u);
            uS.Save();

            return RedirectToAction("Users", "Admin");
        }

        [HttpGet]
        public IActionResult AdminEdit(string username)
        {
            return View("EditUser", uS.getUserbyName(username));

        }

        public IActionResult AdminDelete(string username)
        {
            uS.deleteProfile(uS.getUserbyName(username));
            uS.Save();
            return RedirectToAction("Users", "Admin");
        }

    }
}