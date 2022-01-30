﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CMDB.Infrastructure;
using Microsoft.AspNetCore.Hosting;

namespace CMDB.Controllers
{

    public class SubscriptionController : CMDBController
    {
        private readonly static string sitePart = "Subscription";
        private readonly static string table = "subscription";
        public SubscriptionController(CMDBContext context, IWebHostEnvironment env) : base(context, env)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
