﻿using CMDB.Infrastructure;
using CMDB.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System;
using System.Text;
using System.Collections.Generic;

namespace CMDB.Services
{
    public class AssetCategoryService : LogService
    {
        public AssetCategoryService(CMDBContext context) : base(context)
        {
        }
        public List<AssetCategory> ListAll()
        {
            var categories = _context.AssetCategories.ToList();
            return categories;
        }
        public List<AssetCategory> ListAll(string searchString)
        {
            string searhterm = "%" + searchString + "%";
            var categories = _context.AssetCategories
                .Where(x => EF.Functions.Like(x.Category, searhterm) && EF.Functions.Like(x.Prefix, searhterm)).ToList();
            return categories;
        }
        public List<AssetCategory> ListByID(int id)
        {
            var categories = _context.AssetCategories.Where(x => x.Id == id).ToList();
            return categories;
        }
        public void Create(AssetCategory category, string table)
        {
            _context.AssetCategories.Add(category);
            _context.SaveChanges();
            string Value = String.Format("Assetcategory {0} with prefix {1}", category.Category, category.Prefix);
            LogCreate(table, category.Id, Value, Admin.Account.UserID);
        }
        public void Update(AssetCategory category, string Category, string prefix, string Table)
        {
            if (String.Compare(category.Category, Category) != 0)
            {
                category.Category = Category;
                _context.AssetCategories.Update(category);
                _context.SaveChanges();
                LogUpdate(Table, category.Id, "Category", category.Category, Category, Admin.Account.UserID);
            }
            if (String.Compare(category.Prefix, prefix) != 0)
            {
                category.Prefix = prefix;
                _context.AssetCategories.Update(category);
                _context.SaveChanges();
                if (String.IsNullOrEmpty(category.Prefix))
                    category.Prefix = "Empty";
                if (String.IsNullOrEmpty(prefix))
                    prefix = "Empty";
                LogUpdate(Table, category.Id, "Prefix", category.Prefix, prefix, Admin.Account.UserID);
            }
        }
        public void Deactivate(AssetCategory category, string Reason, string Table)
        {
            category.Active = "Inactive";
            category.DeactivateReason = Reason;
            _context.AssetCategories.Update(category);
            _context.SaveChanges();
            string Value = String.Format("Assetcategory {0} with prefix {1}", category.Category, category.Prefix);
            LogDeactivate(Table, category.Id, Value, Reason);
        }
        public void Activate(AssetCategory category, string Table)
        {
            category.Active = "Active";
            category.DeactivateReason = "";
            _context.AssetCategories.Update(category);
            _context.SaveChanges();
            string Value = String.Format("Assetcategory {0} with prefix {1}", category.Category, category.Prefix);
            LogActivate(Table, category.Id, Value);
        }
        public bool IsExisting(AssetCategory category, string Category = "")
        {
            List<AssetCategory> Catogories;
            bool result = false;
            bool changed = false;
            if (String.IsNullOrEmpty(Category))
                Catogories = _context.AssetCategories.Where(x => x.Category == category.Category).ToList();
            else if (String.Compare(category.Category, Category) != 0)
            {
                Catogories = _context.AssetCategories.Where(x => x.Category == Category).ToList();
                changed = true;
            }
            else
                Catogories = _context.AssetCategories.Where(x => x.Category == category.Category).ToList();
            if (Catogories.Count > 0)
            {
                result = true;
            }
            if (!changed)
                result = false;
            return result;
        }
    }
}