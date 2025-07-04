﻿using Bright.ScreenPlay.Abilities;
using CMDB.Domain.Entities;
using CMDB.Infrastructure;
using CMDB.Testing.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CMDB.UI.Specflow.Abilities.Data
{
    public class DataContext: Ability
    {
        public readonly CMDBContext context;
        public Admin Admin { get; private set; }
        public DataContext() 
        {
            string connectionstring = "Server=.;Database=CMDB;User Id=sa;Password=Gr7k6VKW92dteZ5n;encrypt=false;";
            var options = new DbContextOptionsBuilder<CMDBContext>()
                .UseSqlServer(connectionstring)
                .EnableSensitiveDataLogging()
                .Options;
            context = new CMDBContext(options);
        }
        public async Task<Admin> CreateNewAdmin(Account account, int level = 9, bool active = false)
        {
            var admin = await AdminHelper.CreateSimpleAdmin(context, account, Admin, level, active);
            return admin;
        }
        /// <summary>
        /// This will create a new Admin
        /// </summary>
        /// <param name="level">The level you wan to have the admin to have by default 9</param>
        /// <returns>Admin</returns>
        public async Task<Admin> CreateNewCMDBAdmin(int level = 9)
        {
            Admin = await AdminHelper.CreateCMDBAdmin(context, level);
            return Admin;
        }
        /// <summary>
        /// This function will return an Identity using the Id
        /// </summary>
        /// <param name="IdenId">The identityId</param>
        /// <returns>Identity</returns>
        public Identity GetIdentity(int IdenId)
        {
            var iden = context.Identities
                .Include(x => x.Type)
                .Include(x => x.Language)
                .Where(x => x.IdenId == IdenId)
                .FirstOrDefault();
            return iden;
        }
        /// <summary>
        /// This will return an AssetType using the Id
        /// </summary>
        /// <param name="AssetTypeID">The AssetTypeId</param>
        /// <returns></returns>
        public AssetType GetAssetType(int AssetTypeID)
        {
            var assetType = context.AssetTypes
                .Include(x => x.Category)
                .Where(x => x.TypeID == AssetTypeID)
                .FirstOrDefault();
            return assetType;
        }
        /// <summary>
        /// This will return the Account using the Id
        /// </summary>
        /// <param name="AccountId">The AccountId</param>
        /// <returns>Account</returns>
        public Account GetAccount(int AccountId)
        {
            var account = context.Accounts
                .Include(x => x.Application)
                .Include(x => x.Type)
                .Where(x => x.AccID == AccountId)
                .FirstOrDefault();
            return account;
        }
        /// <summary>
        /// This will return an AssetCategory using the Category
        /// </summary>
        /// <param name="category">Category</param>
        /// <returns>AssetCategory</returns>
        public AssetCategory GetAssetCategory(string category)
        {
            var Category = context.AssetCategories
                .Where(x => x.Category == category)
                .FirstOrDefault();
            return Category;
        }
        /// <summary>
        /// This will return an AssetCategory using the Id
        /// </summary>
        /// <param name="CatId">The Category Id</param>
        /// <returns>AssetCategory</returns>
        public AssetCategory GetAssetCategory(int CatId)
        {
            var Category = context.AssetCategories
                .Where(x => x.Id == CatId)
                .FirstOrDefault();
            return Category;
        }
        /// <summary>
        /// This will check or create a new AssetType
        /// </summary>
        /// <param name="vendor">The vendor</param>
        /// <param name="type">The Type</param>
        /// <param name="category">The category</param>
        /// <returns>AssetType</returns>
        public async Task<AssetType> GetOrCreateAssetType(string vendor, string type, AssetCategory category, Admin admin)
        {
            var assetTypes = context.AssetTypes
                .Include(x => x.Category)
                .Where(x => x.Vendor == vendor && x.Type == type && x.CategoryId == category.Id).ToList();
            if (assetTypes.Count == 0)
            {
                // Create new
                AssetType assetType = new()
                {
                    Vendor = vendor,
                    Type = type,
                    Category = category,
                    LastModifiedAdmin = admin
                };
                context.AssetTypes.Add(assetType);
                await context.SaveChangesAsync();
                return assetType;
            }
            else
                return assetTypes.FirstOrDefault();
        }
        public async Task<Menu> GetOrCreateMenu(string label, Admin admin)
        {
            var menus = context.Menus.Where(x => x.Label == label).ToList();
            if (menus.Count > 0)
            {
                return menus.First();
            }
            else
            {
                return await MenuHelper.CreateSimpleMenu(context, admin);
            }
        }
        public Permission GetPermission(string permission)
        {
            var permissions = context.Permissions
                .Where(x => x.Rights == permission)
                .FirstOrDefault();
            return permissions;
        }
        /// <summary>
        /// This will return a devivce using the Asset Tag
        /// </summary>
        /// <param name="AssetTag">AssetTag</param>
        /// <returns>Device</returns>
        public Device GetDevice(string AssetTag)
        {
            var Laptop = context.Devices
                .Include(x => x.Type)
                .Include(x => x.Category)
                .Where(x => x.AssetTag == AssetTag).FirstOrDefault();
            return Laptop;
        }
        /// <summary>
        /// This function will assign an Idenity to an Account
        /// </summary>
        /// <param name="identity">Identity</param>
        /// <param name="account">Account</param>
        /// <param name="admin">Admin</param>
        public async Task AssignIden2Account(Identity identity, Account account, Admin admin)
        {
            identity.LastModifiedAdmin = admin;
            account.LastModifiedAdmin = admin;
            context.IdenAccounts.Add(new()
            {
                Identity = identity,
                Account = account,
                ValidFrom = DateTime.UtcNow.AddDays(-1),
                ValidUntil = DateTime.UtcNow.AddYears(1),
                LastModifiedAdmin = admin
            });

            await context.SaveChangesAsync();
        }
        /// <summary>
        /// This function will assign a given Identity to a given device
        /// </summary>
        /// <param name="admin">The admin</param>
        /// <param name="device">The device</param>
        /// <param name="identity">The identity</param>
        public async Task AssignIdentity2Device(Admin admin, Device device, Identity identity)
        {
            identity.LastModifiedAdmin = admin;
            device.LastModifiedAdmin = admin;
            identity.Devices.Add(device);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// This function will assign a givan Identity to a given Mobile
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="mobile"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task AssignIdentity2Mobile(Admin admin, Mobile mobile, Identity identity)
        {
            identity.LastModifiedAdmin = admin;
            mobile.LastModifiedAdmin = admin;
            identity.Mobiles.Add(mobile);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// This function will check if there is a subscriptionType and if not create on
        /// </summary>
        /// <param name="admin">The Admin that will create it</param>
        /// <param name="type">The type</param>
        /// <returns></returns>
        public async Task<SubscriptionType> GetOrCreateSubscriptionType(Admin admin, string type)
        {
            SubscriptionType subscriptionType = context.SubscriptionTypes
                .Include(x => x.Category)
                .Where(x => x.Type == type).FirstOrDefault();
            subscriptionType ??= await CreateSubscriptionType(admin);

            return subscriptionType;
        }
        /// <summary>
        /// This function will assign a Subscription to a Identity
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="subscription"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task AssignIdentity2Subscription(Admin admin, Subscription subscription, Identity identity)
        {
            identity.LastModifiedAdmin = admin;
            subscription.LastModifiedAdmin = admin;
            identity.Subscriptions.Add(subscription);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// This function will assign a subscription to my mobile
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="mobile"></param>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public async Task AssignMobile2Subscription(Admin admin, Mobile mobile,Subscription subscription)
        {
            mobile.LastModifiedAdmin= admin;
            subscription.LastModifiedAdmin= admin;
            subscription.Mobile = mobile;
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// This function will assign a device 2 a key
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="device"></param>
        /// <param name="kensington"></param>
        /// <returns></returns>
        public async Task AssignDevice2Key(Admin admin, Device device, Kensington kensington)
        {
            device.LastModifiedAdmin = admin;
            kensington.LastModifiedAdmin = admin;
            kensington.AssetTag = device.AssetTag;
            await context.SaveChangesAsync();
        }
        public new void Dispose()
        {
            context.Dispose();
        }
        /// <summary>
        /// This function will create a subscription type
        /// </summary>
        /// <param name="admin">The admin</param>
        /// <param name="actice">Indicates if the subscriptiontype needs to be active or not</param>
        /// <returns></returns>
        private async Task<SubscriptionType> CreateSubscriptionType(Admin admin, bool actice = true)
        {
            AssetCategory assetCategory = context.AssetCategories.Where(x => x.Category == "Internet Subscription").First();
            SubscriptionType subscriptionType = await SubscriptionTypeHelper.CreateSimpleSubscriptionType(context, assetCategory, admin, actice);
            return subscriptionType;
        }
    }
}
