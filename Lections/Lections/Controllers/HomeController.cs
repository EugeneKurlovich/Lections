using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lections.Models;
using Lections.Services;

namespace Lections.Controllers
{
    public class HomeController : Controller
    {

        LectionService lS;

        public HomeController(DatabaseContext context)
        {
            lS = new LectionService(context);
        }

        public IActionResult Index()
        {
            return View(lS.getAllLections());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
