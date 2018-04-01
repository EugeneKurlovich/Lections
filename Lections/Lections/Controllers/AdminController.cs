using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lections.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Lections()
        {
            return View();
        }
    }
}