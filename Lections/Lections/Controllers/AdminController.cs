using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lections.Models;
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

        public IActionResult Lections()
        {
            return View();
        }
    }
}