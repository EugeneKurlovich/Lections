﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lections.Controllers
{
    public class LectionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateLection()
        {
            return View();
        }

        public IActionResult AllLections()
        {
            return View();
        }
    }
}