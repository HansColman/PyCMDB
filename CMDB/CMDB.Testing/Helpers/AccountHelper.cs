﻿using CMDB.Domain.Entities;
using CMDB.Infrastructure;
using CMDB.Testing.Builders.EntityBuilders;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CMDB.Testing.Helpers
{
    public class AccountHelper
    {
        public static async Task<Account> CreateSimpleAccountAsync(CMDBContext context, Admin admin, bool active = true)
        {
            var accounttypes = await context.Types.OfType<AccountType>().Where(x => x.Type == "Normal User").AsNoTracking().ToListAsync();
            var accounttype = accounttypes.FirstOrDefault();
            var app = await context.Applications.Where(x => x.Name == "CMDB").AsNoTracking().FirstOrDefaultAsync();

            Account Account = new AccountBuilder()
                .With(x => x.ApplicationId, app.AppID)
                .With(x => x.TypeId, accounttype.TypeId)
                .With(x => x.LastModifiedAdminId, admin.AdminId)
                .With(x => x.active,1)
                .Build();

            Account.Logs.Add(new LogBuilder()
                .With(x => x.Account, Account)
                .With(x => x.LogText, $"The Account with UserID: {Account.UserID} with type {accounttype.Type} for application {app.Name} is created by Automation in table account")
                .Build()
            );
            context.Accounts.Add(Account);
            await context.SaveChangesAsync();
            if (!active)
            {
                //Change the state to deactive
                Account.active = 0;
                Account.DeactivateReason = "Test";
                await context.SaveChangesAsync();
            }
            return Account;
        }
        public static async Task Delete(CMDBContext context, Account account)
        {
            context.RemoveRange(account.Logs);
            context.Remove(account);
            await context.SaveChangesAsync();
        }
    }
}
