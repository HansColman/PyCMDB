﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using CMDB.Util;
using CMDB.DbContekst;
using Microsoft.AspNetCore.Hosting;

namespace CMDB.Controllers
{
    public class SubscriptionTypeController : CMDBController
    {
        private readonly CMDBContext _context;
        private readonly ILogger<SubscriptionTypeController> _logger;
        private readonly static string sitePart = "Subscription Type";
        private readonly static string table = "subscriptiontype";
        private readonly IWebHostEnvironment _env;
        public SubscriptionTypeController(CMDBContext context, ILogger<SubscriptionTypeController> logger, IWebHostEnvironment env):base(context,logger,env)
        {
            _context = context;
            _logger = logger;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}