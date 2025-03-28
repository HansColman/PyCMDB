﻿using CMDB.Domain.Entities;
using CMDB.Infrastructure;
using CMDB.Testing.Builders.EntityBuilders;
using CMDB.Testing.Builders.EntityBuilders.Devices;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CMDB.Testing.Helpers.Devices
{
    public class TokenHelper
    {
        public static async Task<Token> CreateNewToken(CMDBContext context, Admin admin, bool active = true)
        {
            var cat = context.AssetCategories.Where(x => x.Category == "Token").AsNoTracking().SingleOrDefault();
            var AssetType = await AssetTypeHelper.CreateSimpleAssetType(context, cat, admin);
            Token token = new TokenBuilder()
                .With(x => x.CategoryId,cat.Id)
                .With(x => x.TypeId, AssetType.TypeID)
                .With(x => x.IdentityId, 1)
                .With(x => x.LastModifiedAdminId, admin.AdminId)
                .Build();
            token.Logs.Add(new LogBuilder().With(x => x.Device, token)
                .With(x => x.LogText, $"The {cat.Category} with type {token.Type} is created by Automation in table token")
                .Build()
                );
            context.Devices.Add(token);
            await context.SaveChangesAsync();
            if (!active)
            {
                token.active = 0;
                context.SaveChanges();
            }
            return token;
        }
        public static async Task Delete(CMDBContext context, Token token)
        {
            context.RemoveRange(token.Logs);
            context.Remove(token);
            await context.SaveChangesAsync();
        }
    }
}
