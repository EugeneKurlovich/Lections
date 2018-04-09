﻿using System;
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
        UserService uS;
        LectionService lS;
        LikeService lkS;


        public IActionResult subjAsc()
        {
            return View("Lections", lS.getAllLections().OrderBy(n => n.subject));
        }
        public IActionResult subjDsc()
        {
            return View("Lections", lS.getAllLections().OrderByDescending(n => n.subject));
        }
        public IActionResult idAsc()
        {
            return View("Lections", lS.getAllLections().OrderBy(n => n.UserId));
        }
        public IActionResult idDsc()
        {
            return View("Lections", lS.getAllLections().OrderBy(n => n.UserId));
        }
        public IActionResult nameAsc()
        {
            return View("Lections", lS.getAllLections().OrderBy(n => n.name));
        }
        public IActionResult nameDsc()
        {
            return View("Lections", lS.getAllLections().OrderByDescending(n => n.name));
        }
        public IActionResult starsAsc()
        {
            return View("Lections", lS.getAllLections().OrderBy(n => n.stars));
        }
        public IActionResult starsDsc()
        {
            return View("Lections", lS.getAllLections().OrderByDescending(n => n.stars));
        }
        public IActionResult cDateAsc()
        {
            return View("Lections", lS.getAllLections().OrderBy(n => n.dateCreate));
        }
        public IActionResult cDateDsc()
        {
            return View("Lections", lS.getAllLections().OrderByDescending(n => n.dateCreate));
        }
        public IActionResult uDateAsc()
        {
            return View("Lections", lS.getAllLections().OrderBy(n => n.dateUpdate));
        }
        public IActionResult uDateDsc()
        {
            return View("Lections", lS.getAllLections().OrderByDescending(n => n.dateUpdate));
        }

        public AdminController(DatabaseContext context)
        {
            uS = new UserService(context);
            lS = new LectionService(context);
            lkS = new LikeService(context);
        }
        public IActionResult uNameAsc()
        {
            return View("Users" , uS.getAllUsers().OrderBy(n => n.username));
        }
        public IActionResult uNameDsc()
        {
            return View("Users", uS.getAllUsers().OrderByDescending(n => n.username));
        }
        public IActionResult starAsc()
        {
            return View("Users", uS.getAllUsers().OrderBy(n => n.ammountStars));
        }
        public IActionResult starDsc()
        {
            return View("Users", uS.getAllUsers().OrderByDescending(n => n.ammountStars));
        }
        public IActionResult lAsc()
        {
            return View("Users", uS.getAllUsers().OrderBy(n => n.ammountLections));
        }
        public IActionResult lDsc()
        {
            return View("Users", uS.getAllUsers().OrderByDescending(n => n.ammountLections));
        }
        public IActionResult admAsc()
        {
            return View("Users", uS.getAllUsers().OrderBy(n => n.isAdmin));
        }
        public IActionResult admDsc()
        {
            return View("Users", uS.getAllUsers().OrderByDescending(n => n.isAdmin));
        }
        public IActionResult emailAsc()
        {
            return View("Users", uS.getAllUsers().OrderBy(n => n.emailConfirmed));
        }
        public IActionResult emailDsc()
        {
            return View("Users", uS.getAllUsers().OrderByDescending(n => n.emailConfirmed));
        }


        public IActionResult Users()
        {
            return View(uS.getAllUsers());
        }

        public IActionResult Lections(string username)
        {
            return View(lS.getAllLections());
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

        public IActionResult AdminEditLection(string name)
        {
            return View("EditLection", lS.getLectionByName(name));
        }

        public IActionResult SaveEditLection([FromForm] Lection l)
        {
            Lection lection = lS.getLectionByName(l.name);
            lection.smallDescription = l.smallDescription;
            lection.text = l.text;
            lection.dateUpdate = DateTime.Now;
            lS.updateUserLection(lection);
            lS.Save();
            return View("Lections", lS.getAllLections());
        }

        public IActionResult AdminDeleteLection(string name)
        {
            Lection lection = lS.getLectionByName(name);
            User user = uS.getUserById(lection.UserId);
            user.ammountLections--;
            uS.updateProfile(user);
            uS.Save();
            lS.deleteUserLection(lS.getLectionByName(name));        
            lS.Save();
            return View("Lections", lS.getAllLections());
        }

        public IActionResult AdminDelete(string username)
        {

            uS.deleteProfile(uS.getUserbyName(username));
            lkS.deleteUserLikes(uS.getUserIdByName(username));
            uS.Save();
            lkS.Save();
            return RedirectToAction("Users", "Admin");
        }

    }
}