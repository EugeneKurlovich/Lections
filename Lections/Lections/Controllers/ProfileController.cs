using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lections.Models;
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
    }
}