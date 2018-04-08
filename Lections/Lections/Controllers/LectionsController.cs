using System;
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
        LikeService lkS;

        public LectionsController(DatabaseContext context)
        {
            uS = new UserService(context);
            lS = new LectionService(context);
            lkS = new LikeService(context);
        }

        public IActionResult nameAsc()
        {
            return View("AllLections", lS.getLectionsByUser(uS.getUserIdByName(User.Identity.Name)).OrderBy(n => n.name));
        }

        public IActionResult nameDsc()
        {
            return View("AllLections", lS.getLectionsByUser(uS.getUserIdByName(User.Identity.Name)).OrderByDescending(n => n.name));
        }

        public IActionResult starsAsc()
        {
            return View("AllLections", lS.getLectionsByUser(uS.getUserIdByName(User.Identity.Name)).OrderBy(n => n.stars));
        }

        public IActionResult starsDsc()
        {
            return View("AllLections", lS.getLectionsByUser(uS.getUserIdByName(User.Identity.Name)).OrderByDescending(n => n.stars));
        }

        public IActionResult cDateAsc()
        {
            return View("AllLections", lS.getLectionsByUser(uS.getUserIdByName(User.Identity.Name)).OrderBy(n => n.dateCreate));
        }

        public IActionResult cDateDsc()
        {
            return View("AllLections", lS.getLectionsByUser(uS.getUserIdByName(User.Identity.Name)).OrderByDescending(n => n.dateCreate));
        }

        public IActionResult uDateAsc()
        {
            return View("AllLections", lS.getLectionsByUser(uS.getUserIdByName(User.Identity.Name)).OrderBy(n => n.dateUpdate));
        }

        public IActionResult uDateDsc()
        {
            return View("AllLections", lS.getLectionsByUser(uS.getUserIdByName(User.Identity.Name)).OrderByDescending(n => n.dateUpdate));
        }

        public IActionResult CreateLection()
        {
            return View();
        }

        public IActionResult ShowText(string name)
        {
            Lection l = lS.getAuthorByLName(name);
            ViewBag.usName = uS.getUserNameById(l.UserId);
            return View("ShowLection", lS.getLectionByName(name));

        }

        public IActionResult UpdateLection([FromForm] Lection l)
        {
            Lection lection = lS.getLectionByName(l.name);

            lection.smallDescription = l.smallDescription;
            lection.text = l.text;
            lection.dateUpdate = DateTime.Now;
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

        [HttpGet]
        public IActionResult getStars(string star, string name)
        {
            Lection l = lS.getAuthorByLName(name);
            if (!lkS.checkExistLike (uS.getUserbyName(User.Identity.Name).Id, lS.getLectionByName(name).Id))
            {
                Likes like = new Likes();
                like.userStar = Convert.ToInt32(star);
                like.LectionId = lS.getLectionByName(name).Id;
                like.UserId = uS.getUserbyName(User.Identity.Name).Id;
                lkS.addLike(like);
                lkS.Save();
                lS.likeLection(lS.getLectionByName(name).Id, lkS.getNowRating(lS.getLectionByName(name).Id));
                lS.Save();
              
                User user = uS.getUserById(l.UserId);
                user.ammountStars++;
                uS.plusStar(user);
                uS.Save();
               
            }
            else
            {
               Likes like = lkS.getLikeById(uS.getUserbyName(User.Identity.Name).Id, lS.getLectionByName(name).Id);
                like.userStar = Convert.ToInt32(star);
                lkS.updateLike(like);
                lkS.Save();
                lS.likeLection(lS.getLectionByName(name).Id, lkS.getNowRating(lS.getLectionByName(name).Id));
                lS.Save();
            }

            ViewBag.usName = uS.getUserNameById(l.UserId);
            return View("ShowLection", lS.getLectionByName(name));
        }


        public IActionResult LectionDelete(string name)
        {
            lS.deleteUserLection(lS.getLectionByName(name));
            User user = uS.getUserbyName(User.Identity.Name);
            lS.Save();
            user.ammountLections--;
            uS.minusLection(user);
            uS.Save();
            return View("AllLections", lS.getAllLections());
        }

        public IActionResult SaveLection([FromForm] Lection lection)
        {            
            if(lS.checkExistLectionName(lection.name))
            {
                lection.UserId = uS.getUserIdByName(User.Identity.Name);
                lection.dateCreate = DateTime.Now;
                lection.dateUpdate = DateTime.Now;
                lS.createUserLection(lection);
                lS.Save();
                User user = uS.getUserbyName(User.Identity.Name);
                user.ammountLections++;
                uS.plusLection(user);
                uS.Save();
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