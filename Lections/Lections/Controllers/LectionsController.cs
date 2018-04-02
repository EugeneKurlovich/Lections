using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lections.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lections.Controllers
{
    public class LectionsController : Controller
    {

        private static DatabaseContext db;

        public LectionsController(DatabaseContext context)
        {
            db = context;
        }

        public IActionResult CreateLection()
        {
            return View();
        }

        public IActionResult AllLections()
        {
            return View();
        }

        public IActionResult SaveLection([FromForm] Lection lection)
        {
            var id = from i in db.Users where User.Identity.Name.Equals(i.username) select i.Id;
            foreach (int usId in id)
            {
                lection.UserId = usId;
            }
            
            db.Lections.Add(lection);
            db.SaveChanges();
            return View("AllLections");
        }
    }
}