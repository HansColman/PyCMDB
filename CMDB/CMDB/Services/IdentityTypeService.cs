﻿using CMDB.Infrastructure;
using CMDB.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMDB.Services
{
    public class IdentityTypeService : LogService
    {
        public IdentityTypeService(CMDBContext context) : base(context)
        {
        }
        public async Task<ICollection<IdentityType>> ListAll()
        {
            var types = await _context.IdentityTypes.ToListAsync();
            return types;
        }
        public async Task<ICollection<IdentityType>> ListAll(string searchString)
        {
            string searhterm = "%" + searchString + "%";
            var types = await _context.IdentityTypes
                .Where(x => EF.Functions.Like(x.Description, searhterm) || EF.Functions.Like(x.Type, searchString))
                .ToListAsync();
            return types;
        }
        public async Task<ICollection<IdentityType>> GetByID(int id)
        {
            var types = await _context.IdentityTypes
                .Where(x => x.TypeID == id)
                .ToListAsync();
            return types;
        }
        public async Task Create(IdentityType identityType, string Table)
        {
            identityType.LastModfiedAdmin = Admin;
            _context.IdentityTypes.Add(identityType);
            await _context.SaveChangesAsync();
            string Value = "Identitytype created with type: " + identityType.Type + " and description: " + identityType.Description;
            await LogCreate(Table, identityType.TypeID, Value);
        }
        public async Task Update(IdentityType identityType, string Type, string Description, string Table)
        {
            identityType.LastModfiedAdmin = Admin;
            if (String.Compare(identityType.Type, Type) != 0)
            {
                identityType.Type = Type;
                await _context.SaveChangesAsync();
                await LogUpdate(Table, identityType.TypeID, "Type", identityType.Type, Type);
            }
            if (String.Compare(identityType.Description, Description) != 0)
            {
                identityType.Description = Description;
                await _context.SaveChangesAsync();
                await LogUpdate(Table, identityType.TypeID, "Description", identityType.Description, Description);
            }
        }
        public async Task Deactivate(IdentityType identityType, string reason, string Table)
        {
            identityType.LastModfiedAdmin = Admin;
            identityType.DeactivateReason = reason;
            identityType.Active = "Inactive";
            await _context.SaveChangesAsync();
            string Value = "Account type created with type: " + identityType.Type + " and description: " + identityType.Description;
            await LogDeactivate(Table, identityType.TypeID, Value, reason);
        }
        public async Task Activate(IdentityType identityType, string table)
        {
            identityType.LastModfiedAdmin = Admin;
            identityType.DeactivateReason = "";
            identityType.Active = "Active";
            await _context.SaveChangesAsync();
            string Value = "Account type created with type: " + identityType.Type + " and description: " + identityType.Description;
            await LogActivate(table, identityType.TypeID, Value);
        }
        public bool IsExisting(IdentityType identityType, string Type = "", string Description = "")
        {
            bool result = false;
            if (String.IsNullOrEmpty(Type) && String.Compare(identityType.Type, Type) != 0)
            {
                var identypes = _context.IdentityTypes
                    .Where(x => x.Type == Type)
                    .ToList();
                if (identypes.Count > 0)
                    result = true;
            }
            if (String.IsNullOrEmpty(Description) && String.Compare(identityType.Description, Description) != 0)
            {
                var identypes = _context.IdentityTypes
                    .Where(x => x.Description == Description)
                    .ToList();
                if (identypes.Count > 0)
                    result = true;
            }
            return result;
        }
    }
}
