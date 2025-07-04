﻿using CMDB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMDB.Infrastructure
{
    public class CMDBContext : DbContext
    {
        public CMDBContext(DbContextOptions<CMDBContext> options) : base(options)
        {
        }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<AssetCategory> AssetCategories { get; set; }
        public virtual DbSet<AssetType> AssetTypes { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Mobile> Mobiles { get; set; }
        public virtual DbSet<Identity> Identities { get; set; }
        public virtual DbSet<IdenAccount> IdenAccounts { get; set; }
        public virtual DbSet<GeneralType> Types { get; set; }
        public virtual DbSet<Kensington> Kensingtons { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolePerm> RolePerms { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<SubscriptionType> SubscriptionTypes { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<RAM> RAMs { get; set; }
        public virtual DbSet<IdentityAccountInfo> IdentityAccountInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CMDBContext).Assembly);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("server=.;database=CMDB;User Id=sa;Password=Gr7k6VKW92dteZ5n;Encrypt=False");
    }
}
