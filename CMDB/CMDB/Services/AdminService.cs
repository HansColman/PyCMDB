﻿using CMDB.Infrastructure;
using System.Collections.Generic;
using CMDB.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CMDB.Services
{
    public class AdminService : LogService
    {
        public AdminService(CMDBContext context) : base(context)
        {
        }
        public List<Admin> ListAll()
        {
            List<Admin> admins = _context.Admins
                .Include(x => x.Account)
                .ToList();
            return admins;
        }
        public List<Admin> ListAll(string searchString)
        {
            string searhterm = "%" + searchString + "%";
            List<Admin> admins = _context.Admins
                .Include(x => x.Account)
                .Where(x => EF.Functions.Like(x.Level.ToString(), searhterm) || EF.Functions.Like(x.Account.UserID, searhterm))
                .ToList();
            return admins;
        }
        public List<Admin> GetByID(int id)
        {
            List<Admin> admins = _context.Admins
                .Include(x => x.Account)
                .Where(x => x.AdminId == id)
                .ToList();
            return admins;
        }
        public List<SelectListItem> ListAllLevels()
        {
            List<SelectListItem> Levels = new();
            for (int i = 0; i < 9; i++)
            {
                Levels.Add(new SelectListItem(i.ToString(), i.ToString()));
            }
            return Levels;
        }
        public List<SelectListItem> ListActiveCMDBAccounts()
        {
            List<SelectListItem> Levels = new();
            var accounts = _context.Accounts
                .Include(x => x.Application)
                .Include(x => x.Identities)
                .Where(x => x.Application.Name == "CMDB")
                .ToList();
            foreach (var account in accounts)
            {
                foreach (var idenAcc in account.Identities.Where(x => DateTime.Now <= x.ValidFrom && x.ValidUntil >= DateTime.Now))
                {
                    Levels.Add(new SelectListItem(idenAcc.Account.UserID, idenAcc.Account.AccID.ToString()));
                }
            }
            return Levels;
        }
        public void Create(Admin admin, string Table)
        {
            string pwd = new PasswordHasher().EncryptPassword("cmdb");
            admin.Password = pwd;
            admin.DateSet = DateTime.Now;
            _context.Admins.Add(admin);
            _context.SaveChanges();
            string Value = "Admin with UserID: " + admin.Account.UserID + " and level: " + admin.Level.ToString();
            LogCreate(Table, Admin.AdminId, Value, this.Admin.Account.UserID);
        }
        public void Update(Admin admin, int level, string Table)
        {
            if (admin.Level != level)
            {
                admin.Level = level;
                _context.SaveChanges();
                LogUpdate(Table, admin.AdminId, "Level", admin.Level.ToString(), level.ToString(), this.Admin.Account.UserID);
            }
        }
        public void Deactivate(Admin admin, string reason, string table)
        {
            admin.Active = "Inactive";
            admin.DeactivateReason = reason;
            _context.SaveChanges();
            string Value = "Admin with UserID: " + admin.Account.UserID + " and level: " + admin.Level.ToString();
            LogDeactivate(table, admin.AdminId, Value, reason);
        }
        public void Activate(Admin admin, string table)
        {
            admin.Active = "Active";
            admin.DeactivateReason = "";
            _context.SaveChanges();
            string Value = "Admin with UserID: " + admin.Account.UserID + " and level: " + admin.Level.ToString();
            LogActivate(table, admin.AdminId, Value);
        }
        public bool IsExisting(Admin admin)
        {
            bool result = false;
            var admins = _context.Admins.Include(x => x.Account).Where(x => x.Account.UserID == admin.Account.UserID);
            return result;
        }
        public List<Account> GetAccountByID(int ID)
        {
            List<Account> accounts = _context.Accounts
                .Include(x => x.Application)
                .Include(x => x.Type)
                .Where(x => x.AccID == ID)
                .ToList();
            return accounts;
        }
    }
}