﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CMDB.Controllers
{
    public class DockingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
