﻿using CMDB.Infrastructure;
using CMDB.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System;
using System.Text;

namespace CMDB.Services
{
    public class LogService : CMDBServices
    {
        private string LogText;
        public LogService(CMDBContext context) : base(context)
        {
        }

        #region Admin stuff
        public bool HasAdminAccess(Admin admin, string site, string action)
        {
            var perm = _context.RolePerms
                .Include(x => x.Menu)
                .Include(x => x.Permission)
                .Where(x => x.Level == admin.Level || x.Menu.Label == site || x.Permission.Rights == action).ToList();
            if (perm.Count > 0)
                return true;
            else
                return false;
        }
        #endregion
        #region log functions
        public void GetLogs(string table, int ID, Model model)
        {
            model.Logs = table switch
            {
                "identity" => _context.Logs.Include(x => x.Identity).Where(x => x.Identity.IdenId == ID).OrderByDescending(x => x.LogDate).ToList(),
                "identitytype" => _context.Logs.Include(x => x.IdentityType).Where(x => x.IdentityType.TypeID == ID).OrderByDescending(x => x.LogDate).ToList(),
                "account" => _context.Logs.Include(x => x.Account).Where(x => x.Account.AccID == ID).OrderByDescending(x => x.LogDate).ToList(),
                "accounttype" => _context.Logs.Include(x => x.AccountType).Where(x => x.AccountType.TypeID == ID).OrderByDescending(x => x.LogDate).ToList(),
                "role" => _context.Logs.Include(x => x.Role).Where(x => x.Role.RoleId == ID).OrderByDescending(x => x.LogDate).ToList(),
                "roletype" => _context.Logs.Include(x => x.RoleType).Where(x => x.RoleType.TypeId == ID).OrderByDescending(x => x.LogDate).ToList(),
                "assettype" => _context.Logs.Include(x => x.AssetType).Where(x => x.AssetType.TypeID == ID).OrderByDescending(x => x.LogDate).ToList(),
                "menu" => _context.Logs.Include(x => x.Menu).Where(x => x.Menu.MenuId == ID).OrderByDescending(x => x.LogDate).ToList(),
                "permissions" => _context.Logs.Include(x => x.Permission).Where(x => x.Permission.Id == ID).OrderByDescending(x => x.LogDate).ToList(),
                "application" => _context.Logs.Include(x => x.Application).Where(x => x.Application.AppID == ID).OrderByDescending(x => x.LogDate).ToList(),
                "kensington" => _context.Logs.Include(x => x.Kensington).Where(x => x.Kensington.KeyID == ID).OrderByDescending(x => x.LogDate).ToList(),
                "admin" => _context.Logs.Include(x => x.Admin).Where(x => x.Admin.AdminId == ID).OrderByDescending(x => x.LogDate).ToList(),
                "mobile" => _context.Logs.Include(x => x.Mobile).Where(x => x.Mobile.IMEI == ID).OrderByDescending(x => x.LogDate).ToList(),
                "subscriptiontype" => _context.Logs.Include(x => x.SubscriptionType).Where(x => x.SubscriptionType.Id == ID).OrderByDescending(x => x.LogDate).ToList(),
                "subscription" => _context.Logs.Include(x => x.Subscription).Where(x => x.Subscription.Id == ID).OrderByDescending(x => x.LogDate).ToList(),
                "assetcategory" => _context.Logs.Include(x => x.Category).Where(x => x.Category.Id == ID).OrderByDescending(x => x.LogDate).ToList(),
                _ => throw new Exception("No log insert statement created for table: " + table),
            };
        }
        public void GetLogs(string table, string AssetTag, Model model)
        {
            model.Logs = table switch
            {
                "laptop" => _context.Logs.Include(x => x.Laptop).Where(x => x.Laptop.AssetTag == AssetTag).OrderByDescending(x => x.LogDate).ToList(),
                "desktop" => _context.Logs.Include(x => x.Desktop).Where(x => x.Desktop.AssetTag == AssetTag).OrderByDescending(x => x.LogDate).ToList(),
                "docking" => _context.Logs.Include(x => x.Docking).Where(x => x.Docking.AssetTag == AssetTag).OrderByDescending(x => x.LogDate).ToList(),
                "token" => _context.Logs.Include(x => x.Token).Where(x => x.Token.AssetTag == AssetTag).OrderByDescending(x => x.LogDate).ToList(),
                "screen" => _context.Logs.Include(x => x.Screen).Where(x => x.Screen.AssetTag == AssetTag).OrderByDescending(x => x.LogDate).ToList(),
                _ => throw new Exception("No log insert statement created for table: " + table),
            };
        }
        protected void LogCreate(string table, int ID, string Value, string AdminName)
        {
            this.LogText = "The " + Value + " is created by " + AdminName + " in table " + table;
            DoLog(table, ID);
        }
        protected void LogCreate(string table, string AssetTag, string Value, string AdminName)
        {
            this.LogText = "The " + Value + " is created by " + AdminName + " in table " + table;
            DoLog(table, AssetTag);
        }
        protected void LogUpdate(string table, int ID, string field, string oldValue, string newValue, string AdminName)
        {
            if (String.IsNullOrEmpty(oldValue))
                oldValue = "Empty";
            if (String.IsNullOrEmpty(newValue))
                newValue = "Empty";
            LogText = "The " + field + " in table " + table + " has been changed from " + oldValue + " to " + newValue + " by " + AdminName;
            DoLog(table, ID);
        }
        protected void LogUpdate(string table, string AssetTag, string field, string oldValue, string newValue, string AdminName)
        {
            if (String.IsNullOrEmpty(oldValue))
                oldValue = "Empty";
            if (String.IsNullOrEmpty(newValue))
                newValue = "Empty";
            LogText = "The " + field + " in table " + table + " has been changed from " + oldValue + " to " + newValue + " by " + AdminName;
            DoLog(table, AssetTag);
        }
        protected void LogDeactivate(string table, int ID, string value, string reason)
        {
            LogText = "The " + value + " in table " + table + " is deleted due to " + reason + " by " + Admin.Account.UserID;
            DoLog(table, ID);
        }
        protected void LogDeactivated(string table, string AssetTag, string value, string reason)
        {
            LogText = "The " + value + " in table " + table + " is deleted due to " + reason + " by " + Admin.Account.UserID;
            DoLog(table, AssetTag);
        }
        protected void LogActivate(string table, int ID, string value)
        {
            LogText = "The " + value + " in table " + table + " is activated by " + Admin.Account.UserID;
            DoLog(table, ID);
        }
        protected void LogActivate(string table, string AssetTag, string value)
        {
            LogText = "The " + value + " in table " + table + " is activated by " + Admin.Account.UserID;
            DoLog(table, AssetTag);
        }
        protected void LogAssignIden2Account(string table, int ID, Identity identity, Account account)
        {
            LogText = "The Identity width name:" + identity.Name + " in table " + table + " is assigned to Account with UserID" + account.UserID + " by " + Admin.Account.UserID;
            DoLog(table, ID);
        }
        protected void LogAssignAccount2Identity(string table, int ID, Account account, Identity identity)
        {
            LogText = "The Account with UserID " + account.UserID + " in table " + table + " is assigned to Identity width name:" + identity.Name + " by " + Admin.Account.UserID;
            DoLog(table, ID);
        }
        protected void LogReleaseAccountFromIdentity(string table, int IdenId, Identity identity, Account account)
        {
            LogText = "Identity with Name " + identity.Name + " in table " + table + " is released from Account with UserID " + account.UserID + " in appliction " + account.Application.Name + " by " + Admin.Account.UserID;
            DoLog(table, IdenId);
        }
        protected void LogReleaseIdentity4Account(string table, int AccId, Identity identity, Account account)
        {
            LogText = "Account with UserID " + account.UserID + " in appliction " + account.Application.Name + " in table " + table + " is released from Identity with Name " + identity.Name + " by " + Admin.Account.UserID;
            DoLog(table, AccId);
        }
        private void DoLog(string table, int ID)
        {
            DateTime LogDate = DateTime.Now;
            Log log = new()
            {
                LogDate = LogDate,
                LogText = this.LogText
            };
            switch (table)
            {
                case "identity":
                    Identity identity = _context.Identities.Where(x => x.IdenId == ID).First();
                    log.Identity = identity;
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                case "identitytype":
                    IdentityType identityType = _context.IdentityTypes.Where(x => x.TypeID == ID).First();
                    log.IdentityType = identityType;
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                case "account":
                    Account account = _context.Accounts.Where(x => x.AccID == ID).First();
                    log.Account = account;
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                case "accounttype":
                    log.AccountType = _context.AccountTypes.Where(x => x.TypeID == ID).First();
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                case "role":
                    log.Role = _context.Roles.Where(x => x.RoleId == ID).First(); ;
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                case "roletype":
                    log.RoleType = _context.RoleTypes.Where(x => x.TypeId == ID).First();
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                case "assettype":
                    log.AssetType = _context.AssetTypes.Where(x => x.TypeID == ID).First();
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                case "menu":
                    log.Menu = _context.Menus.Where(x => x.MenuId == ID).First();
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                case "permissions":
                    log.Permission = _context.Permissions.Where(x => x.Id == ID).First();
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                case "application":
                    log.Application = _context.Applications.Where(x => x.AppID == ID).First();
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                case "kensington":
                    log.Kensington = _context.Kensingtons.Where(x => x.KeyID == ID).First();
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                case "admin":
                    log.Admin = _context.Admins.Where(x => x.AdminId == ID).First();
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                case "mobile":
                    log.Mobile = _context.Mobiles.Where(x => x.IMEI == ID).First();
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                case "subscriptiontype":
                    log.SubscriptionType = _context.SubscriptionTypes.Where(x => x.Id == ID).First();
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                case "subscription":
                    log.Subscription = _context.Subscriptions.Where(x => x.Id == ID).First();
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                case "assetcategory":
                    log.Category = _context.AssetCategories.Where(x => x.Id == ID).First();
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                default:
                    throw new Exception("No log insert statement created for table: " + table);
            }
        }
        private void DoLog(string table, string AssetTag)
        {
            DateTime LogDate = DateTime.Now;
            Log log = new()
            {
                LogDate = LogDate,
                LogText = this.LogText
            };
            switch (table)
            {
                case "laptop":
                    var Laptop = _context.Laptops.Where(x => x.AssetTag == AssetTag).First();
                    log.Laptop = Laptop;
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                case "desktop":
                    var desktop = _context.Desktops.Where(x => x.AssetTag == AssetTag).First();
                    log.Desktop = desktop;
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                case "docking":
                    var docking = _context.Dockings.Where(x => x.AssetTag == AssetTag).First();
                    log.Docking = docking;
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                case "screen":
                    var Screen = _context.Screens.Where(x => x.AssetTag == AssetTag).First();
                    log.Screen = Screen;
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                case "token":
                    var Token = _context.Tokens.Where(x => x.AssetTag == AssetTag).First();
                    log.Token = Token;
                    _context.Logs.Add(log);
                    _context.SaveChanges();
                    break;
                default:
                    throw new Exception("No log insert statement created for table: " + table);
            }
        }
        #endregion
    }
}