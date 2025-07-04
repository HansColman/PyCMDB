﻿using CMDB.Domain.DTOs;
using CMDB.Infrastructure;
using CMDB.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CMDB.Controllers
{
    /// <summary>
    /// Controller for the mobile
    /// </summary>
    public class MobileController : CMDBController
    {
        private readonly MobileService service;
        private readonly PDFService _PDFService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="env"></param>
        public MobileController(IWebHostEnvironment env) : base(env)
        {
            SitePart = "Mobile";
            Table = "mobile";
            service = new();
            _PDFService = new();
        }
        /// <summary>
        /// The index page which will show all the mobiles
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            log.Debug("Using list all for {0}", SitePart);
            await BuildMenu();
            ViewData["Title"] = "Mobile overview";
            ViewData["AddAccess"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "Add");
            ViewData["InfoAccess"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "Read");
            ViewData["DeleteAccess"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "Delete");
            ViewData["ActiveAccess"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "Activate");
            ViewData["UpdateAccess"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "Update");
            ViewData["AssignIdentity"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "AssignIdentity");
            ViewData["AssignSubscription"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "AssignSubscription");
            ViewData["actionUrl"] = @"\Mobile\Search";
            ViewData["Controller"] = @"\Mobile\Create";
            var mobiles = await service.ListAll();
            return View(mobiles);
        }
        /// <summary>
        /// The search page which will show all the mobiles matching the search string
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<IActionResult> Search(string search)
        {
            log.Debug("Using search for {0}", SitePart);
            if (!String.IsNullOrEmpty(search))
            {
                ViewData["Title"] = "Mobile overview";
                await BuildMenu();
                var mobiles = await service.ListAll(search);
                ViewData["AddAccess"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "Add");
                ViewData["InfoAccess"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "Read");
                ViewData["DeleteAccess"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "Delete");
                ViewData["UpdateAccess"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "Update");
                ViewData["AssignIdentity"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "AssignIdentity");
                ViewData["ActiveAccess"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "Activate");
                ViewData["AssignSubscription"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "AssignSubscription");
                ViewData["actionUrl"] = @"\Mobile\Search";
                ViewData["Controller"] = @"\Mobile\Create";
                return View(mobiles);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
        /// <summary>
        /// The create page which will show the form to create a new mobile
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public async Task<IActionResult> Create(IFormCollection values)
        {
            log.Debug("Using Create in {0}", SitePart);
            ViewData["Title"] = "Create Mobile";
            ViewData["AddAccess"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "Add");
            ViewData["Controller"] = @"\Mobile\Create";
            await BuildMenu();
            MobileDTO mobile = new();
            ViewBag.Types = await service.ListAssetTypes(SitePart);
            string FormSubmit = values["form-submitted"];
            if (!String.IsNullOrEmpty(FormSubmit))
            {
                try
                {
                    string imei = values["IMEI"];
                    mobile.IMEI = Convert.ToInt64(values["IMEI"]);
                    int Type = Convert.ToInt32(values["MobileType"]);
                    var AssetType = await service.ListAssetTypeById(Type);
                    var identity = await service.GetIdentity(1);
                    mobile.MobileType = AssetType;
                    mobile.Identity = identity;
                    if(await service.IsMobileExisting(mobile))
                        ModelState.AddModelError("", "Mobile already exist");
                    if (ModelState.IsValid)
                    {
                        await service.CreateNew(mobile);
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Database exception {0}", ex.ToString());
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
            }
            return View(mobile);
        }
        /// <summary>
        /// The details page which will show the details of the mobile
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var mobile = await service.GetMobileById((int)id);
            if (mobile == null)
                return NotFound();
            log.Debug($"Using details in {Table}");
            ViewData["Title"] = "Mobile details";
            ViewData["Controller"] = @"\Mobile\Create";
            await BuildMenu();
            ViewData["InfoAccess"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "Read");
            ViewData["AddAccess"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "Add");
            ViewData["IdentityOverview"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "IdentityOverview");
            ViewData["SubscriptionOverview"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "SubscriptionOverview");
            ViewData["AssignIdentity"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "AssignIdentity");
            ViewData["ReleaseIdentity"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "ReleaseIdentity");
            ViewData["AssignSubscription"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "AssignSubscription");
            ViewData["ReleaseSubscription"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "ReleaseSubscription");
            ViewData["LogDateFormat"] = service.LogDateFormat;
            ViewData["DateFormat"] = service.DateFormat;
            return View(mobile);
        }
        /// <summary>
        /// The edit page which will show the form to edit a mobile
        /// </summary>
        /// <param name="values"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(IFormCollection values, int? id)
        {
            if (id == null)
                return NotFound();
            var mobile = await service.GetMobileById((int)id);
            if (mobile == null)
                return NotFound();
            log.Debug("Using Edit in {0}", Table);
            ViewData["Title"] = "Edit mobile";
            ViewData["Controller"] = @$"\Mobile\Edit\{id}";
            await BuildMenu();
            ViewData["UpdateAccess"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "Update");
            ViewBag.Types = await service.ListAssetTypes(SitePart);
            string FormSubmit = values["form-submitted"];
            if (!string.IsNullOrEmpty(FormSubmit))
            {
                long newImei = Convert.ToInt64(values["IMEI"]);
                int Type = Convert.ToInt32(values["MobileType.TypeID"]);
                var AssetType = await service.ListAssetTypeById(Type);
                if (await service.IsMobileExisting(mobile, newImei))
                    ModelState.AddModelError("", "Mobile already exist");
                try
                {
                    if (ModelState.IsValid)
                    {
                        await service.Update(mobile, newImei, AssetType);
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    log.Error("DB error: {0}", ex.ToString());
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
            }
            return View(mobile);
        }
        /// <summary>
        /// The delete page which will show the form to delete a mobile
        /// </summary>
        /// <param name="values"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(IFormCollection values, int? id)
        {
            if (id == null)
                return NotFound();
            var mobile = await service.GetMobileById((int)id);
            if (mobile == null)
                return NotFound();
            log.Debug("Using Delete in {0}", Table);
            ViewData["Title"] = "Deactivate Mobile";
            ViewData["DeleteAccess"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "Delete");
            ViewData["backUrl"] = "Mobile";
            ViewData["Controller"] = @$"\Mobile\Delete\{id}";
            await BuildMenu();
            string FormSubmit = values["form-submitted"];
            if (!String.IsNullOrEmpty(FormSubmit))
            {
                try
                {
                    string reason = values["reason"];
                    if (ModelState.IsValid)
                    {
                        if (mobile.Identity.IdenId > 1)
                        {
                            var admin = await service.Admin();
                            await _PDFService.SetUserinfo(
                                UserId: mobile.Identity.UserID,
                                ITEmployee:admin.Account.UserID,
                                Singer: mobile.Identity.Name,
                                FirstName:mobile.Identity.FirstName,
                                LastName:mobile.Identity.LastName,
                                Receiver: mobile.Identity.Name,
                                Language: mobile.Identity.Language.Code,
                                "Release");
                            await _PDFService.SetMobileInfo(mobile);
                            await _PDFService.GenratePDFFile(Table, mobile.MobileId);
                            await _PDFService.GenratePDFFile("identity", mobile.Identity.IdenId);
                            await service.ReleaseIdenity(mobile);
                        }
                        await service.Deactivate(mobile, reason);
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    log.Error("DB error: {0}", ex.ToString());
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
            }
            return View(mobile);
        }
        /// <summary>
        /// The activate page which will show the form to activate a mobile
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Activate(int? id)
        {
            if (id == null)
                return NotFound();
            var mobile = await service.GetMobileById((int)id);
            if (mobile == null)
                return NotFound();
            log.Debug("Using Activate in {0}", Table);
            ViewData["Title"] = "Activate Mobile";
            ViewData["ActiveAccess"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "Activate");
            await BuildMenu();
            if (await service.HasAdminAccess(TokenStore.AdminId, SitePart, "Activate"))
            {
                try
                {
                    await service.Activate(mobile);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    log.Error("DB error: {0}", ex.ToString());
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
            }
            else
                RedirectToAction(nameof(Index));
            return View();
        }
        /// <summary>
        /// The assign identity page which will show the form to assign an identity to a mobile
        /// </summary>
        /// <param name="values"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> AssignIdentity(IFormCollection values, int? id)
        {
            if (id == null)
                return NotFound();
            var mobile = await service.GetMobileById((int)id);
            if (mobile == null)
                return NotFound();
            log.Debug("Using Assign identity in {0}", Table);
            ViewData["Title"] = "Assign identity to mobile";
            ViewData["AssignIdentity"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "AssignIdentity");
            ViewData["backUrl"] = "Mobile";
            ViewData["Controller"] = @$"\Mobile\AssignIdentity\{id}";
            await BuildMenu();
            ViewBag.Identities = await service.ListFreeIdentities();
            string FormSubmit = values["form-submitted"];
            if (!String.IsNullOrEmpty(FormSubmit)) 
            {
                try
                {
                    if (!service.IsDeviceFree(mobile))
                        ModelState.AddModelError("", "Mobile can not be assigned to another user");
                    var identity = await service.GetIdentity(Int32.Parse(values["Identity"]));
                    if (ModelState.IsValid)
                    {
                        await service.AssignIdentity2Mobile(mobile, identity);
                        return RedirectToAction("AssignForm", "Mobile", new { id });
                    }
                }
                catch (Exception ex)
                {
                    log.Error("DB error: {0}", ex.ToString());
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
            }
            return View(mobile);
        }
        /// <summary>
        /// The release identity page which will show the form to release the identity from a mobile
        /// </summary>
        /// <param name="values"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> ReleaseIdentity(IFormCollection values, int? id)
        {
            if (id == null)
                return NotFound();
            var mobile = await service.GetMobileById((int)id);
            if (mobile == null)
                return NotFound();
            log.Debug("Using Release identity in {0}", Table);
            ViewData["Title"] = "Release identity from Mobile";
            ViewData["ReleaseIdentity"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "ReleaseIdentity");
            ViewData["backUrl"] = "Mobile";
            ViewData["Action"] = "ReleaseIdentity";
            await BuildMenu();
            IdentityDTO identity = mobile.Identity;
            ViewData["Name"] = identity.Name;
            var admin = await service.Admin();
            ViewData["AdminName"] = admin.Account.UserID;
            string FormSubmit = values["form-submitted"];
            if (!String.IsNullOrEmpty(FormSubmit))
            {
                string Employee = values["Employee"];
                string ITPerson = values["ITEmp"];
                try
                {
                    if (ModelState.IsValid)
                    {
                        await _PDFService.SetUserinfo(
                            UserId: mobile.Identity.UserID,
                            ITEmployee: ITPerson,
                            Singer: Employee,
                            FirstName: mobile.Identity.FirstName,
                            LastName: mobile.Identity.LastName,
                            Receiver: mobile.Identity.Name,
                            Language: mobile.Identity.Language.Code,
                            "Release");
                        await _PDFService.SetMobileInfo(mobile);
                        await _PDFService.GenratePDFFile(Table, mobile.MobileId);
                        await _PDFService.GenratePDFFile("identity", mobile.Identity.IdenId);
                        await service.ReleaseIdenity(mobile);
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    log.Error("DB error: {0}", ex.ToString());
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
            }
            return View(mobile);
        }
        /// <summary>
        /// The release subscription page which will show the form to release the subscription from a mobile
        /// </summary>
        /// <param name="values"></param>
        /// <param name="id"></param>
        /// <param name="MobileId"></param>
        /// <returns></returns>
        public async Task<IActionResult> ReleaseSubscription(IFormCollection values, int? id, int? MobileId)
        {
            if (id is null && MobileId is null)
                return NotFound();
            var mobile = await service.GetMobileById((int)id);
            var sub = await service.GetSubribtion((int)MobileId);
            if (mobile is null && sub is null)
                return NotFound();
            log.Debug($"Using Release subscription in {Table}");
            ViewData["ReleaseIdentity"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "ReleaseSubscription");
            ViewData["backUrl"] = "Mobile";
            ViewData["Action"] = "ReleaseSubscription";
            await BuildMenu();
            var admin = await service.Admin();
            IdentityDTO identity = mobile.Identity;
            ViewData["Name"] = identity.Name;
            ViewData["AdminName"] = admin.Account.UserID;
            string FormSubmit = values["form-submitted"];
            if (!String.IsNullOrEmpty(FormSubmit))
            {
                string Employee = values["Employee"];
                string ITPerson = values["ITEmp"];
                try
                {
                    await _PDFService.SetUserinfo(
                            UserId: mobile.Identity.UserID,
                            ITEmployee: ITPerson,
                            Singer: Employee,
                            FirstName: mobile.Identity.FirstName,
                            LastName: mobile.Identity.LastName,
                            Receiver: mobile.Identity.Name,
                            Language: mobile.Identity.Language.Code,
                            "Release");
                    await _PDFService.SetSubscriptionInfo(sub);
                    await _PDFService.GenratePDFFile(Table, mobile.MobileId);
                    await _PDFService.GenratePDFFile("subscription", sub.SubscriptionId);
                    await service.ReleaseSubscription(mobile);
                }
                catch (Exception ex) 
                {
                    log.Error("DB error: {0}", ex.ToString());
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mobile);
        }
        /// <summary>
        /// The assign subscription page which will show the form to assign a subscription to a mobile
        /// </summary>
        /// <param name="values"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> AssignSubscription(IFormCollection values, int? id)
        {
            if (id == null)
                return NotFound();
            var mobile = await service.GetMobileById((int)id);
            if (mobile == null)
                return NotFound();
            log.Debug("Using assign subscription in {0}", Table);
            ViewData["Title"] = "Assign subscription to mobile";
            ViewData["AssignSubscription"] = await service.HasAdminAccess(TokenStore.AdminId, SitePart, "AssignSubscription");
            ViewData["backUrl"] = "Mobile";
            await BuildMenu();
            ViewBag.Subscriptions = await service.ListFreeMobileSubscriptions();
            string FormSubmit = values["form-submitted"];
            if (!string.IsNullOrEmpty(FormSubmit))
            {
                SubscriptionDTO subscription =await service.GetSubribtion(Int32.Parse(values["Subscription"]));
                if (!service.IsDeviceFree(mobile, true))
                    ModelState.AddModelError("", "Mobile can not be assigned to another user");
                if (ModelState.IsValid)
                {
                    await service.AssignSubscription(mobile, subscription);
                    return RedirectToAction("AssignForm", "Mobile", new { id });
                }
            }
            return View(mobile);
        }
        /// <summary>
        /// The assignForm page which will show the form with the information of the mobile and what is assigned to it
        /// </summary>
        /// <param name="values"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> AssignForm(IFormCollection values, int? id)
        {
            if (id == null)
                return NotFound();
            var mobile = await service.GetMobileById((int)id);
            if (mobile == null)
                return NotFound();
            log.Debug("Using Assign form in {0}", Table);
            ViewData["Title"] = "Assign form";
            ViewData["backUrl"] = "Mobile";
            ViewData["Action"] = "AssignForm";
            await BuildMenu();
            ViewData["Name"] = mobile.Identity.Name;
            var admin = await service.Admin();
            ViewData["AdminName"] = admin.Account.UserID;
            string FormSubmit = values["form-submitted"];
            if (!string.IsNullOrEmpty(FormSubmit))
            {
                string Employee = values["Employee"];
                string ITPerson = values["ITEmp"];
                if (ModelState.IsValid) {
                    if (mobile.Identity is not null) {
                        await _PDFService.SetUserinfo(
                            UserId: mobile.Identity.UserID,
                            ITEmployee: ITPerson,
                            Singer: Employee,
                            FirstName: mobile.Identity.FirstName,
                            LastName: mobile.Identity.LastName,
                            Receiver: mobile.Identity.Name,
                            Language: mobile.Identity.Language.Code);
                        await _PDFService.SetMobileInfo(mobile);
                        await _PDFService.GenratePDFFile(Table, mobile.MobileId);
                        await _PDFService.GenratePDFFile("identity", mobile.Identity.IdenId);
                    }
                    else if (mobile.Subscription is not null)
                    {
                        var identity = mobile.Subscription.Identity;
                        await _PDFService.SetUserinfo(
                            UserId: identity.UserID,
                            ITEmployee: ITPerson,
                            Singer: Employee,
                            FirstName: identity.FirstName,
                            LastName: identity.LastName,
                            Receiver: identity.Name,
                            Language: identity.Language.Code);
                        await _PDFService.SetSubscriptionInfo(mobile.Subscription);
                        await _PDFService.GenratePDFFile(Table, mobile.MobileId);
                        await _PDFService.GenratePDFFile("subscription", mobile.Subscription.SubscriptionId);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mobile);
        }
    }
}
