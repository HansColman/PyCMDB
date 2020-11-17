﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using CMDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using CMDB.Util;
using CMDB.DbContekst;
using Microsoft.AspNetCore.Hosting;

namespace CMDB.Controllers
{
    public class IdentityController : CMDBController
    {
        private readonly CMDBContext _context;
        private readonly ILogger<IdentityController> _logger;
        private readonly IWebHostEnvironment env;
        private readonly static string table = "identity";
        private readonly static string sitePart = "Identity";
        public IdentityController(CMDBContext context, ILogger<IdentityController> logger, IWebHostEnvironment env):base(context,logger,env)
        {
            _context = context;
            _logger = logger;
            this.env = env;
        }
        public IActionResult Index()
        {
            _logger.LogDebug("Using List all in {0}", table);
            var list = _context.ListAllIdenties();
            ViewData["Title"] = "Identity overview";
            BuildMenu();
            ViewData["AddAccess"] = _context.HasAdminAccess(_context.Admin, sitePart, "Add");
            ViewData["InfoAccess"] = _context.HasAdminAccess(_context.Admin, sitePart, "Read");
            ViewData["DeleteAccess"] = _context.HasAdminAccess(_context.Admin, sitePart, "Delete");
            ViewData["ActiveAccess"] = _context.HasAdminAccess(_context.Admin, sitePart, "Activate");
            ViewData["UpdateAccess"] = _context.HasAdminAccess(_context.Admin, sitePart, "Update");
            ViewData["AssignAccountAccess"] = _context.HasAdminAccess(_context.Admin, sitePart, "AssignAccount");
            ViewData["AssignDeviceAccess"] = _context.HasAdminAccess(_context.Admin, sitePart, "AssignDevice");
            ViewData["actionUrl"] = @"\Identity\Search";
            return View(list);
        }
        public IActionResult Search(string search)
        {
            _logger.LogDebug("Using search in {0}", table);
            if (!String.IsNullOrEmpty(search))
            {
                ViewData["search"] = search;
                var list = _context.ListAllIdenties();
                ViewData["Title"] = "Identity overview";
                BuildMenu();
                ViewData["AddAccess"] = _context.HasAdminAccess(_context.Admin, sitePart, "Add");
                ViewData["InfoAccess"] = _context.HasAdminAccess(_context.Admin, sitePart, "Read");
                ViewData["DeleteAccess"] = _context.HasAdminAccess(_context.Admin, sitePart, "Delete");
                ViewData["ActiveAccess"] = _context.HasAdminAccess(_context.Admin, sitePart, "Activate");
                ViewData["UpdateAccess"] = _context.HasAdminAccess(_context.Admin, sitePart, "Update");
                ViewData["AssignAccountAccess"] = _context.HasAdminAccess(_context.Admin, sitePart, "AssignAccount");
                ViewData["AssignDeviceAccess"] = _context.HasAdminAccess(_context.Admin, sitePart, "AssignDevice");
                ViewData["actionUrl"] = @"\Identity\Search";
                return View(list);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Details(int? id)
        {
            _logger.LogDebug("Using details in {0}", table);
            ViewData["Title"] = "Identity Details";
            BuildMenu();
            ViewData["InfoAccess"] = _context.HasAdminAccess(_context.Admin, sitePart, "Read");
            ViewData["AddAccess"] = _context.HasAdminAccess(_context.Admin, sitePart, "Add");
            ViewData["AccountOverview"] = _context.HasAdminAccess(_context.Admin, sitePart, "AccountOverview");
            ViewData["DeviceOverview"] = _context.HasAdminAccess(_context.Admin, sitePart, "DeviceOverview");
            ViewData["AssignDevice"] = _context.HasAdminAccess(_context.Admin, sitePart, "AssignDevice");
            ViewData["AssignAccount"] = _context.HasAdminAccess(_context.Admin, sitePart, "AssignAccount");
            ViewData["ReleaseAccount"] = _context.HasAdminAccess(_context.Admin, sitePart, "ReleaseAccount");
            ViewData["ReleaseDevice"] = _context.HasAdminAccess(_context.Admin, sitePart, "ReleaseDevice");
            ViewData["LogDateFormat"] = _context.LogDateFormat;
            ViewData["DateFormat"] = _context.DateFormat;
            if (id ==null)
            {
                return NotFound();
            }
            var list = _context.GetIdentityByID((int)id);
            _context.GetAssingedDevicesForIdentity(list.ElementAt<Identity>(0));
            _context.GetAssignedAccountsForIdentity(list.ElementAt<Identity>(0));
            _context.GetLogs(table, (int)id, list.ElementAt<Identity>(0));
            if (list == null)
            {
                return NotFound();
            }
            return View(list);
        }
        public IActionResult Create(IFormCollection values)
        {
            _logger.LogDebug("Using Create in {0}", table);
            ViewData["Title"] = "Create Identity";
            ViewData["AddAccess"] = _context.HasAdminAccess(_context.Admin, sitePart, "Add");
            BuildMenu();
            ViewBag.Types = _context.ListActiveIdentityTypes();
            ViewBag.Languages = _context.ListAllActiveLanguages();
            Identity identity = new Identity();
            string FormSubmit = values["form-submitted"];
            try
            {
                if (!String.IsNullOrEmpty(FormSubmit))
                {
                    string FirstName = values["FirstName"];
                    identity.FirstName = FirstName;
                    string LastName = values["LastName"];
                    identity.LastName = LastName;
                    string UserID = values["UserID"];
                    identity.UserID = UserID;
                    string Company = values["Company"];
                    identity.Company = Company;
                    string Type = values["Type"];
                    string EMail = values["EMail"];
                    identity.EMail = EMail;
                    string Language = values["Language"];
                    if (_context.IsIdentityExisting(identity))
                        ModelState.AddModelError("", "Idenity alreday existing");
                    if (ModelState.IsValid)
                    {
                        _context.CreateNewIdentity(FirstName, LastName, Convert.ToInt32(Type), UserID, Company, EMail, Language, table);
                        _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (MySqlException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(identity);
        }
        public IActionResult Edit(IFormCollection values, int? id)
        {
            _logger.LogDebug("Using Edit in {0}", table);
            ViewData["Title"] = "Edit Identity";
            BuildMenu();
            ViewData["UpdateAccess"] = _context.HasAdminAccess(_context.Admin, sitePart, "Update");
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Types = _context.ListActiveIdentityTypes();
            ViewBag.Languages = _context.ListAllActiveLanguages();
            string FormSubmit = values["form-submitted"];
            var list = _context.GetIdentityByID((int)id);
            Identity identity = list.ElementAt<Identity>(0);
            if (!String.IsNullOrEmpty(FormSubmit))
            {
                string NewFirstName = values["FirstName"];
                string NewLastName = values["LastName"];
                string NewUserID = values["UserID"];
                string NewCompany = values["Company"];
                string NewType = values["Type"];
                string NewLanguage = values["Language"];
                string NewEMail = values["EMail"];
                if (_context.IsIdentityExisting(identity, NewUserID))
                    ModelState.AddModelError("", "Idenity alreday existing");
                try
                {
                    if (ModelState.IsValid)
                    {
                        _context.EditIdentity(identity, NewFirstName, NewLastName, Convert.ToInt32(NewType), NewUserID, NewCompany, NewEMail, NewLanguage, table);
                        _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (MySqlException ex)
                {
                    _logger.LogCritical("DB error: {0}", ex.ToString());
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
            }
            return View(identity);
        }
        public IActionResult Delete(IFormCollection values, int? id)
        {
            _logger.LogDebug("Using Delete in {0}", table);
            ViewData["Title"] = "Deactivate Identity";
            ViewData["DeleteAccess"] = _context.HasAdminAccess(_context.Admin, sitePart, "Delete");
            BuildMenu();
            if (id == null)
            {
                return NotFound();
            }
            string FormSubmit = values["form-submitted"];
            var list = _context.GetIdentityByID((int)id);
            Identity identity = list.ElementAt<Identity>(0);
            ViewData["backUrl"] = "Identity";
            if (!String.IsNullOrEmpty(FormSubmit))
            {
                ViewData["reason"] = values["reason"];
                try
                {
                    _context.DeactivateIdenity(identity, ViewData["reason"].ToString(), table);
                    _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (MySqlException ex)
                {
                    _logger.LogCritical("DB error: {0}", ex.ToString());
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
            }
            return View(list);
        }
        public IActionResult Activate(int? id)
        {
            _logger.LogDebug("Using Activate in {0}", table);
            ViewData["Title"] = "Activate Identity";
            ViewData["ActiveAccess"] = _context.HasAdminAccess(_context.Admin, sitePart, "Activate");
            BuildMenu();
            if (id == null)
            {
                return NotFound();
            }
            var list = _context.GetIdentityByID((int)id);
            Identity identity = list.ElementAt<Identity>(0);
            if(_context.HasAdminAccess(_context.Admin, sitePart, "Activate"))
            {
                _context.ActivateIdentity(identity, table);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult AssignAccount(IFormCollection values, int? id)
        {
            _logger.LogDebug("Using Assign Account in {0}", table);
            ViewData["Title"] = "Assign Account";
            ViewData["AssignAccount"] = _context.HasAdminAccess(_context.Admin, sitePart, "AssignAccount");
            BuildMenu();
            if (id == null)
            {
                return NotFound();
            }
            string FormSubmit = values["form-submitted"];
            var list = _context.GetIdentityByID((int)id);
            Identity identity = list.ElementAt<Identity>(0);
            ViewBag.Identity = identity;
            ViewBag.Accounts = _context.ListAllFreeAccounts();
            if (!String.IsNullOrEmpty(FormSubmit))
            {
                int AccId = Convert.ToInt32(values["Account"]);
                DateTime from = DateTime.Parse(values["ValidFrom"]);
                DateTime until = DateTime.Parse(values["ValidUntil"]);
                try
                {
                    if (_context.IsPeriodOverlapping(id, null, from, until))
                        ModelState.AddModelError("", "Periods are overlapping please choose other dates");
                    if (ModelState.IsValid)
                    {
                        _context.AssignAccount2Idenity(identity, AccId, from, until, table);
                        _context.SaveChangesAsync();
                        return RedirectToAction("AssignFrom","Identity",new {id});
                    }
                }
                catch (MySqlException ex)
                {
                    _logger.LogCritical("DB error: {0}",ex.ToString());
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
            }
            if (list == null)
            {
                return NotFound();
            }
            return View();
        }
        public IActionResult ReleaseAccount(IFormCollection values,int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            ViewData["Title"] = "Release Account";
            var idenAccount = _context.GetIdenAccountByID(id);
            ViewBag.Identity = idenAccount.ElementAt<IdenAccount>(0).Identity;
            ViewBag.Account = idenAccount.ElementAt<IdenAccount>(0).Account;
            ViewData["ReleaseAccount"] = _context.HasAdminAccess(_context.Admin, sitePart, "ReleaseAccount");
            BuildMenu();
            ViewData["backUrl"] = "Identity";
            ViewData["Action"] = "ReleaseAccount";
            ViewData["Name"] = idenAccount.ElementAt<IdenAccount>(0).Identity.Name;
            ViewData["AdminName"] = _context.Admin.Account.UserID;
            string FormSubmit = values["form-submitted"];
            if (!String.IsNullOrEmpty(FormSubmit))
            {
                string Employee = values["Employee"];
                string ITPerson = values["ITEmp"];
                if (ModelState.IsValid)
                {
                    _context.ReleaseAccount4Identity(idenAccount.ElementAt<IdenAccount>(0).Identity, idenAccount.ElementAt<IdenAccount>(0).Account, id,table);
                    idenAccount = _context.GetIdenAccountByID(id);
                    PDFGenerator PDFGenerator = new PDFGenerator
                    {
                        ITEmployee = ITPerson,
                        Singer = Employee,
                        UserID = idenAccount.ElementAt<IdenAccount>(0).Identity.UserID,
                        Language = idenAccount.ElementAt<IdenAccount>(0).Identity.Language.Code,
                        Receiver = idenAccount.ElementAt<IdenAccount>(0).Identity.Name,
                        Type = "Release"
                    };
                    PDFGenerator.SetAccontInfo(idenAccount.ElementAt<IdenAccount>(0));
                    PDFGenerator.GeneratePDF(env);
                    _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
        public IActionResult AssignFrom(IFormCollection values, int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            _logger.LogDebug("Using Assign Form in {0}", table);
            ViewData["Title"] = "Assign gosods";
            ViewData["AssignDevice"] = _context.HasAdminAccess(_context.Admin, sitePart, "AssignDevice");
            ViewData["AssignAccount"] = _context.HasAdminAccess(_context.Admin, sitePart, "AssignAccount");
            var list = _context.GetIdentityByID((int)id);
            _context.GetAssingedDevicesForIdentity(list.ElementAt<Identity>(0));
            _context.GetAssignedAccountsForIdentity(list.ElementAt<Identity>(0));
            BuildMenu();
            ViewData["backUrl"] = "Identity";
            ViewData["Action"] = "AssignFrom";
            ViewData["Name"] = list.ElementAt<Identity>(0).Name;
            ViewData["AdminName"] = _context.Admin.Account.UserID;
            ViewData["LogDateFormat"] = _context.LogDateFormat;
            ViewData["DateFormat"] = _context.DateFormat;
            string FormSubmit = values["form-submitted"];
            if (!String.IsNullOrEmpty(FormSubmit))
            {
                string Employee = values["Employee"];
                string ITPerson = values["ITEmp"];
                PDFGenerator PDFGenerator = new PDFGenerator
                {
                    ITEmployee = ITPerson,
                    Singer = Employee,
                    UserID = list.ElementAt<Identity>(0).UserID,
                    Language = list.ElementAt<Identity>(0).Language.Code,
                    Receiver = list.ElementAt<Identity>(0).Name
                };
                if (list.ElementAt<Identity>(0).Accounts.Count >0)
                    PDFGenerator.SetAccontInfo(list.ElementAt<Identity>(0).Accounts.ElementAt<IdenAccount>(0));
                else if(list.ElementAt<Identity>(0).Devices.Count > 0)
                {
                    foreach (Device d in list.ElementAt<Identity>(0).Devices)
                        PDFGenerator.SetAssetInfo(d);
                }
                PDFGenerator.GeneratePDF(env);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(list);
        }
    }
}
