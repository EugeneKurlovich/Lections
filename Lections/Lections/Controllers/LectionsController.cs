﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lections.Models;
using Lections.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lections.Controllers
{
    public class LectionsController : Controller
    {
        UserService uS;
        LectionService lS;

        public LectionsController(DatabaseContext context)
        {
            uS = new UserService(context);
            lS = new LectionService(context);
        }

        public IActionResult CreateLection()
        {
            return View();
        }

        public IActionResult ShowText(string name)
        {
            return View("ShowLection", lS.getLectionByName(name));

        }

        public IActionResult UpdateLection([FromForm] Lection l)
        {
            Lection lection = lS.getLectionByName(l.name);

            lection.smallDescription = l.smallDescription;
            lection.text = l.text;
            lS.updateUserLection(lection);
            lS.Save();
            return View("AllLections", lS.getAllLections());
        }

        public IActionResult AllLections()
        {
            return View(lS.getLectionsByUser(uS.getUserIdByName(User.Identity.Name)));
        }

        public IActionResult LectionEdit(string name)
        {
            return View("EditLection", lS.getLectionByName(name));
        }

        public IActionResult LectionDelete(string name)
        {
            lS.deleteUserLection(lS.getLectionByName(name));
            lS.Save();
            return View("AllLections", lS.getAllLections());
        }

        public IActionResult SaveLection([FromForm] Lection lection)
        {            
            if(lS.checkExistLectionName(lection.name))
            {
                lection.UserId = uS.getUserIdByName(User.Identity.Name);
                lS.createUserLection(lection);
                lS.Save();
                return View("AllLections", lS.getAllLections());
            }
            else
            {
                ViewBag.message = "Lection name exist";
                return View("CreateLection");
            }
            
        }
    }
}