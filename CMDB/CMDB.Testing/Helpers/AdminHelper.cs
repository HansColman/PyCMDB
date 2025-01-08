﻿using CMDB.Domain.Entities;
using CMDB.Infrastructure;
using CMDB.Testing.Builders.EntityBuilders;
using CMDB.Testing.Helpers.Devices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMDB.Testing.Helpers
{
    public class AdminHelper
    {
        public static async Task<Admin> CreateCMDBAdmin(CMDBContext context, int level = 9)
        {
            try
            {
                var app = context.Applications.Where(x => x.Name == "CMDB").AsNoTracking().FirstOrDefault();
                var language = context.Languages.Where(x => x.Code == "NL").AsNoTracking().FirstOrDefault();
                var identype = context.Types.OfType<IdentityType>().Where(x => x.Type == "Werknemer").AsNoTracking().FirstOrDefault();
                var accounttype = context.Types.OfType<AccountType>().Where(x => x.Type == "Administrator").AsNoTracking().FirstOrDefault();

                var Account = new AccountBuilder()
                    .With(x => x.ApplicationId, app.AppID)
                    .With(x => x.TypeId, accounttype.TypeId)
                    .With(x => x.LastModifiedAdminId, 1)
                    .Build();
                Account.Logs.Add(new LogBuilder()
                    .With(x => x.LogText, $"Account created {Account.UserID} for application {app.Name}")
                    .Build()
                );
                context.Accounts.Add(Account);
                await context.SaveChangesAsync();

                var admin = new AdminBuilder()
                    .With(x => x.Level, level)
                    .With(x => x.Account, Account)
                    .With(x => x.LastModifiedAdminId, 1)
                    .Build();

                admin.Logs.Add(new LogBuilder()
                    .With(x => x.Admin, admin)
                    .With(x => x.LogText, $"Admin created with userid: {Account.UserID}")
                    .Build()
                );

                context.Admins.Add(admin);
                await context.SaveChangesAsync();

                var identity = new IdentityBuilder()
                    .With(x => x.LanguageCode, language.Code)
                    .With(x => x.TypeId, identype.TypeId)
                    .With(x => x.UserID, Account.UserID)
                    .With(x => x.LastModifiedAdminId, admin.AdminId)
                    .Build();

                identity.Logs.Add(new LogBuilder()
                    .With(x => x.Identity, identity)
                    .With(x => x.LogText, $"identity created {identity.Name}")
                    .Build()
                );

                context.Identities.Add(identity);

                context.IdenAccounts.Add(new()
                {
                    Identity = identity,
                    Account = Account,
                    LastModifiedAdminId = admin.AdminId
                });

                await context.SaveChangesAsync();
                return admin;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static async Task<Dictionary<string, Object>> DeleteCascading(CMDBContext context, Admin admin)
        {
            Dictionary<string, Object> Data = new();
            try
            {
                //AccountType
                var accountType = context.Types
                    .OfType<AccountType>()
                    .Include(x => x.Logs)
                    .Where(x => x.LastModifiedAdminId == admin.AdminId)
                    .ToList();
                foreach (var type in accountType)
                {
                    Data.Add("AccountType" + accountType.IndexOf(type).ToString(), type);
                    await AccountTypeHelper.Delete(context, type);
                }
                //AssetType
                var assetType = context.AssetTypes
                    .Where(x => x.LastModifiedAdminId == admin.AdminId)
                    .ToList();
                foreach (var type in assetType)
                {
                    Data.Add("AssetType" + assetType.IndexOf(type).ToString(), type);
                    await AssetTypeHelper.Delete(context, type);
                }
                //IdentityTypes
                var identypes = context.Types
                    .OfType<IdentityType>()
                    .Include(x => x.Logs)
                    .Where(x => x.LastModifiedAdminId == admin.AdminId)
                    .ToList();
                foreach (var type in identypes)
                {
                    Data.Add("IdentityType" + identypes.IndexOf(type).ToString(), type);
                    await IdentityTypeHelper.Delete(context, type);
                }
                //Laptop
                var laptops = context.Devices
                    .OfType<Laptop>()
                    .Include(x => x.Logs)
                    .Where(x => x.LastModifiedAdminId == admin.AdminId)
                    .ToList();
                foreach (var laptop in laptops)
                {
                    Data.Add("Laptop" + laptops.IndexOf(laptop).ToString(), laptop);
                    await LaptopHelper.Delete(context, laptop);
                }
                //Desktop
                var desktops = context.Devices
                    .OfType<Desktop>()
                    .Include(x => x.Logs)
                    .Where(x => x.LastModifiedAdminId == admin.AdminId)
                    .ToList();
                foreach (var desktop in desktops)
                {
                    Data.Add("Desktop" + desktops.IndexOf(desktop).ToString(), desktop);
                    await DesktopHelper.Delete(context, desktop);
                }
                //Screen
                var screens = context.Devices
                    .OfType<Screen>()
                    .Include(x => x.Logs)
                    .Where(x => x.LastModifiedAdminId == admin.AdminId)
                    .ToList();
                foreach (var screen in screens)
                {
                    Data.Add("Screen" + screens.IndexOf(screen).ToString(), screen);
                    await ScreenHelper.Delete(context, screen);
                }
                //Docking
                var dockings = context.Devices
                    .OfType<Docking>()
                    .Include(x => x.Logs)
                    .Where(x => x.LastModifiedAdminId == admin.AdminId)
                    .ToList();
                foreach (var docking in dockings)
                {
                    Data.Add("Docking" + dockings.IndexOf(docking).ToString(), docking);
                    await DockingHelpers.Delete(context, docking);
                }
                //Token
                var tokens = context.Devices
                    .OfType<Token>()
                    .Include(x => x.Logs)
                    .Where(x => x.LastModifiedAdminId == admin.AdminId)
                    .ToList();
                foreach (var token in tokens)
                {
                    Data.Add("Token" + tokens.IndexOf(token).ToString(), token);
                    await TokenHelper.Delete(context, token);
                }
                //mobiles
                var mobiles = context.Mobiles
                    .Include(x => x.Logs)
                    .Where(x => x.LastModifiedAdminId == admin.AdminId)
                    .ToList();
                foreach (var mobile in mobiles)
                {
                    Data.Add("Mobile"+mobiles.IndexOf(mobile).ToString(),mobile);
                    await MobileHelper.Delete(context, mobile);
                }
                //IdenAccount
                var idenAccs = context.IdenAccounts
                    .Where(x => x.LastModifiedAdminId == admin.AdminId)
                    .ToList();
                foreach (var idenacc in idenAccs)
                {
                    Data.Add("IdenAccount" + idenAccs.IndexOf(idenacc).ToString(), idenacc);
                    context.Remove<IdenAccount>(idenacc);
                }
                //Identity
                var identities = context.Identities
                    .Include(x => x.Logs)
                    .Where(x => x.LastModifiedAdminId == admin.AdminId)
                    .ToList();
                foreach (var identity in identities)
                {
                    Data.Add("Identity" + identities.IndexOf(identity).ToString(), identity);
                    await IdentityHelper.Delete(context, identity);
                }
                //Account
                var accounts = context.Accounts
                    .Include(x => x.Logs)
                    .Where(x => x.LastModifiedAdminId == admin.AdminId)
                    .ToList();
                foreach (var account in accounts)
                {
                    Data.Add("Account" + accounts.IndexOf(account).ToString(), account);
                    await AccountHelper.Delete(context, account);
                }
                //Subscription
                var subs = context.Subscriptions
                    .Include(x => x.Logs)
                    .Where(x => x.LastModifiedAdminId == admin.AdminId)
                    .ToList();
                foreach (var sub in subs)
                {
                    Data.Add($"Subscription{sub.SubscriptionId}", sub);
                    await SubscriptionHelper.Delete(context, sub);
                }
                //SubscriptionTypes
                var subtypes = context.SubscriptionTypes
                    .Include(x => x.Logs)
                    .Where(x => x.LastModifiedAdminId == admin.AdminId)
                    .ToList();
                foreach(var subtype in subtypes)
                {
                    Data.Add($"SubscriptionType{subtype.Id}",subtype);
                    await SubscriptionTypeHelper.Delete(context,subtype);
                }
                //Admin
                context.RemoveRange(admin.Logs);
                context.Remove<Admin>(admin);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return Data;
        }
    }
}
