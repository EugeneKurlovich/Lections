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

        public IActionResult ShowText(string name)
        {
            foreach (Lection lect in db.Lections)
            {
                if (lect.name.Equals(name))
                {
                    return View("ShowLection", lect);
                }
            }
            return View("AllLections");           
        }

        public IActionResult UpdateLection([FromForm] Lection l)
        {
            foreach(Lection lection in db.Lections)
            {
                if(lection.name.Equals(l.name))
                {
                    lection.smallDescription = l.smallDescription;
                    lection.text = l.text;
                }
            }
            db.SaveChanges();
            return View("AllLections", db.Lections);
        }

        public IActionResult AllLections()
        {
            var lect = (from i in db.Lections where i.User.username.Equals(User.Identity.Name) select i).ToList();
            return View(lect);
        }

        public IActionResult LectionEdit(string name)
        {
            foreach(Lection l in db.Lections)
            {
                if (l.name.Equals(name))
                {
                    return View("EditLection",l);
                }
            }
            return View("AllLections",db.Lections);
        }

        public IActionResult LectionDelete(string name)
        {
            foreach (Lection lection in db.Lections)
            {
                if (lection.name.Equals(name))
                {
                    db.Lections.Remove(lection);
                }
            }
            db.SaveChanges();
            return View("AllLections", db.Lections);
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