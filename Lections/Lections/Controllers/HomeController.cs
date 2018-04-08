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

        public IActionResult nameAsc()
        {
            return View("Index", lS.getAllLections().OrderBy(n => n.name));
        }

        public IActionResult nameDsc()
        {
            return View("Index", lS.getAllLections().OrderByDescending(n => n.name));
        }

        public IActionResult starsAsc()
        {
            return View("Index", lS.getAllLections().OrderBy(n => n.stars));
        }

        public IActionResult starsDsc()
        {
            return View("Index", lS.getAllLections().OrderByDescending(n => n.stars));
        }

        public IActionResult cDateAsc()
        {
            return View("Index", lS.getAllLections().OrderBy(n => n.dateCreate));
        }

        public IActionResult cDateDsc()
        {
            return View("Index", lS.getAllLections().OrderByDescending(n => n.dateCreate));
        }

        public IActionResult uDateAsc()
        {
            return View("Index", lS.getAllLections().OrderBy(n => n.dateUpdate));
        }

        public IActionResult uDateDsc()
        {
            return View("Index", lS.getAllLections().OrderByDescending(n => n.dateUpdate));
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
