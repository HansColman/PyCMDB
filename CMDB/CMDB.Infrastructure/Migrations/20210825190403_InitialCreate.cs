﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMDB.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountType",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "varchar(255)", nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", nullable: false),
                    active = table.Column<int>(type: "int", maxLength: 1, nullable: false, defaultValue: 1),
                    Deactivate_reason = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountType", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    AppID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    active = table.Column<int>(type: "int", maxLength: 1, nullable: false, defaultValue: 1),
                    Deactivate_reason = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.AppID);
                });

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "varchar(255)", nullable: false),
                    Prefix = table.Column<string>(type: "varchar(5)", nullable: true),
                    active = table.Column<int>(type: "int", maxLength: 1, nullable: false, defaultValue: 1),
                    Deactivate_reason = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetCagegory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configuration",
                columns: table => new
                {
                    Code = table.Column<string>(type: "varchar(255)", nullable: false),
                    SubCode = table.Column<string>(type: "varchar(255)", nullable: false),
                    CFN_Date = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    CFN_Number = table.Column<int>(type: "int", nullable: true),
                    CFN_Tekst = table.Column<string>(type: "varchar(255)", nullable: true),
                    Description = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuration", x => new { x.Code, x.SubCode });
                });

            migrationBuilder.CreateTable(
                name: "IdentityType",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "varchar(255)", nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", nullable: false),
                    active = table.Column<int>(type: "int", maxLength: 1, nullable: false, defaultValue: 1),
                    Deactivate_reason = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityType", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", nullable: false),
                    active = table.Column<int>(type: "int", maxLength: 1, nullable: false, defaultValue: 1),
                    Deactivate_reason = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "varchar(255)", nullable: false),
                    URL = table.Column<string>(type: "varchar(255)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.MenuId);
                    table.ForeignKey(
                        name: "FK_Menu_Menu",
                        column: x => x.ParentId,
                        principalTable: "Menu",
                        principalColumn: "MenuId");
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rights = table.Column<string>(type: "varchar(255)", nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PPermission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RAM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Display = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleType",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "varchar(255)", nullable: true),
                    Description = table.Column<string>(type: "varchar(255)", nullable: true),
                    active = table.Column<int>(type: "int", maxLength: 1, nullable: false, defaultValue: 1),
                    Deactivate_reason = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleType", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "varchar(255)", nullable: false),
                    active = table.Column<int>(type: "int", maxLength: 1, nullable: false, defaultValue: 1),
                    Deactivate_reason = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccID);
                    table.ForeignKey(
                        name: "FK_Account_Application",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "AppID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Account_Type",
                        column: x => x.TypeId,
                        principalTable: "AccountType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssetType",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vendor = table.Column<string>(type: "varchar(255)", nullable: false),
                    Type = table.Column<string>(type: "varchar(255)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<int>(type: "int", maxLength: 1, nullable: false, defaultValue: 1),
                    Deactivate_reason = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetType", x => x.TypeID);
                    table.ForeignKey(
                        name: "FK_AssetType_category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssetCategoryId = table.Column<int>(type: "int", nullable: true),
                    active = table.Column<int>(type: "int", maxLength: 1, nullable: false, defaultValue: 1),
                    Deactivate_reason = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetType_Category",
                        column: x => x.AssetCategoryId,
                        principalTable: "category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Identity",
                columns: table => new
                {
                    IdenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    active = table.Column<int>(type: "int", maxLength: 1, nullable: false, defaultValue: 1),
                    Deactivate_reason = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity", x => x.IdenId);
                    table.ForeignKey(
                        name: "FK_Identity_Language",
                        column: x => x.LanguageCode,
                        principalTable: "Language",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Identity_Type",
                        column: x => x.TypeId,
                        principalTable: "IdentityType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePerm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePerm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePerm_Menu",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePerm_Permission",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: true),
                    Description = table.Column<string>(type: "varchar(255)", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    active = table.Column<int>(type: "int", maxLength: 1, nullable: false, defaultValue: 1),
                    Deactivate_reason = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_Role_Type",
                        column: x => x.TypeId,
                        principalTable: "RoleType",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Admin_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", nullable: true),
                    DateSet = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    active = table.Column<int>(type: "int", maxLength: 1, nullable: false, defaultValue: 1),
                    Deactivate_reason = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Admin_id);
                    table.ForeignKey(
                        name: "FK_Admin_Account",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Desktop",
                columns: table => new
                {
                    AssetTag = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MAC = table.Column<string>(type: "varchar(255)", nullable: true),
                    RAM = table.Column<string>(type: "varchar(255)", nullable: false),
                    active = table.Column<int>(type: "int", maxLength: 1, nullable: false, defaultValue: 1),
                    Deactivate_reason = table.Column<string>(type: "varchar(255)", nullable: true),
                    SerialNumber = table.Column<string>(type: "varchar(255)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    IdentityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desktop_Asset", x => x.AssetTag);
                    table.ForeignKey(
                        name: "FK_Desktop_Category",
                        column: x => x.CategoryId,
                        principalTable: "category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Desktop_Identity",
                        column: x => x.IdentityId,
                        principalTable: "Identity",
                        principalColumn: "IdenId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Desktop_Type",
                        column: x => x.TypeId,
                        principalTable: "AssetType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Docking",
                columns: table => new
                {
                    AssetTag = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    active = table.Column<int>(type: "int", maxLength: 1, nullable: false, defaultValue: 1),
                    Deactivate_reason = table.Column<string>(type: "varchar(255)", nullable: true),
                    SerialNumber = table.Column<string>(type: "varchar(255)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    IdentityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docking_Asset", x => x.AssetTag);
                    table.ForeignKey(
                        name: "FK_Docking_Category",
                        column: x => x.CategoryId,
                        principalTable: "category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Docking_Identity",
                        column: x => x.IdentityId,
                        principalTable: "Identity",
                        principalColumn: "IdenId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Docking_Type",
                        column: x => x.TypeId,
                        principalTable: "AssetType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdenAccount",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValidFrom = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    IdentityId = table.Column<int>(type: "int", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdenAccount", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IdenAccount_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdenAccount_Identity_IdentityId",
                        column: x => x.IdentityId,
                        principalTable: "Identity",
                        principalColumn: "IdenId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Laptop",
                columns: table => new
                {
                    AssetTag = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MAC = table.Column<string>(type: "varchar(255)", nullable: true),
                    RAM = table.Column<string>(type: "varchar(255)", nullable: false),
                    active = table.Column<int>(type: "int", maxLength: 1, nullable: false, defaultValue: 1),
                    Deactivate_reason = table.Column<string>(type: "varchar(255)", nullable: true),
                    SerialNumber = table.Column<string>(type: "varchar(255)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    IdentityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laptop_Asset", x => x.AssetTag);
                    table.ForeignKey(
                        name: "FK_Laptop_Category",
                        column: x => x.CategoryId,
                        principalTable: "category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Laptop_Identity",
                        column: x => x.IdentityId,
                        principalTable: "Identity",
                        principalColumn: "IdenId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Laptop_Type",
                        column: x => x.TypeId,
                        principalTable: "AssetType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mobile",
                columns: table => new
                {
                    IMEI = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    IdentityId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    active = table.Column<int>(type: "int", maxLength: 1, nullable: false, defaultValue: 1),
                    Deactivate_reason = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mobile", x => x.IMEI);
                    table.ForeignKey(
                        name: "FK_Mobile_Category",
                        column: x => x.CategoryId,
                        principalTable: "category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mobile_Identity",
                        column: x => x.IdentityId,
                        principalTable: "Identity",
                        principalColumn: "IdenId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mobile_Type",
                        column: x => x.TypeId,
                        principalTable: "AssetType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Screen",
                columns: table => new
                {
                    AssetTag = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    active = table.Column<int>(type: "int", maxLength: 1, nullable: false, defaultValue: 1),
                    Deactivate_reason = table.Column<string>(type: "varchar(255)", nullable: true),
                    SerialNumber = table.Column<string>(type: "varchar(255)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    IdentityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screen_Asset", x => x.AssetTag);
                    table.ForeignKey(
                        name: "FK_Screen_Category",
                        column: x => x.CategoryId,
                        principalTable: "category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Screen_Identity",
                        column: x => x.IdentityId,
                        principalTable: "Identity",
                        principalColumn: "IdenId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Screen_Type",
                        column: x => x.TypeId,
                        principalTable: "AssetType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    AssetTag = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    active = table.Column<int>(type: "int", maxLength: 1, nullable: false, defaultValue: 1),
                    Deactivate_reason = table.Column<string>(type: "varchar(255)", nullable: true),
                    SerialNumber = table.Column<string>(type: "varchar(255)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    IdentityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token_Asset", x => x.AssetTag);
                    table.ForeignKey(
                        name: "FK_Token_Category",
                        column: x => x.CategoryId,
                        principalTable: "category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Token_Identity",
                        column: x => x.IdentityId,
                        principalTable: "Identity",
                        principalColumn: "IdenId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Token_Type",
                        column: x => x.TypeId,
                        principalTable: "AssetType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubsctiptionTypeId = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentityId = table.Column<int>(type: "int", nullable: true),
                    MobileId = table.Column<int>(type: "int", nullable: true),
                    AssetCategoryId = table.Column<int>(type: "int", nullable: true),
                    active = table.Column<int>(type: "int", maxLength: 1, nullable: false, defaultValue: 1),
                    Deactivate_reason = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscription_Category",
                        column: x => x.AssetCategoryId,
                        principalTable: "category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscription_Identity",
                        column: x => x.IdentityId,
                        principalTable: "Identity",
                        principalColumn: "IdenId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscription_Mobile",
                        column: x => x.MobileId,
                        principalTable: "Mobile",
                        principalColumn: "IMEI",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscription_Type",
                        column: x => x.SubsctiptionTypeId,
                        principalTable: "SubscriptionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kensington",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "varchar(255)", nullable: false),
                    LaptopAssetTag = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DesktopAssetTag = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DockingAssetTag = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ScreenAssetTag = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AmountOfKeys = table.Column<int>(type: "int", nullable: false),
                    HasLock = table.Column<bool>(type: "bit", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    active = table.Column<int>(type: "int", maxLength: 1, nullable: false, defaultValue: 1),
                    Deactivate_reason = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kensington", x => x.KeyID);
                    table.ForeignKey(
                        name: "FK_Kensington_Category",
                        column: x => x.CategoryId,
                        principalTable: "category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kensington_Type",
                        column: x => x.TypeId,
                        principalTable: "AssetType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Key_Deskop",
                        column: x => x.DesktopAssetTag,
                        principalTable: "Desktop",
                        principalColumn: "AssetTag",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Key_Docking",
                        column: x => x.DockingAssetTag,
                        principalTable: "Docking",
                        principalColumn: "AssetTag",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Key_Laptop",
                        column: x => x.LaptopAssetTag,
                        principalTable: "Laptop",
                        principalColumn: "AssetTag",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Key_Screen",
                        column: x => x.ScreenAssetTag,
                        principalTable: "Screen",
                        principalColumn: "AssetTag",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    AccountTypeId = table.Column<int>(type: "int", nullable: true),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    ApplicationId = table.Column<int>(type: "int", nullable: true),
                    AssetCategoryId = table.Column<int>(type: "int", nullable: true),
                    AssetTypeId = table.Column<int>(type: "int", nullable: true),
                    LaptopAssetTag = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DesktopAssetTag = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ScreenAssetTag = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DockingAssetTag = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TokenAssetTag = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdentityId = table.Column<int>(type: "int", nullable: true),
                    IdentityTypeId = table.Column<int>(type: "int", nullable: true),
                    KensingtonId = table.Column<int>(type: "int", nullable: true),
                    MenuId = table.Column<int>(type: "int", nullable: true),
                    MobileId = table.Column<int>(type: "int", nullable: true),
                    PermissionId = table.Column<int>(type: "int", nullable: true),
                    SubsriptionId = table.Column<int>(type: "int", nullable: true),
                    SubscriptionTypeId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    RoleTypeId = table.Column<int>(type: "int", nullable: true),
                    LanguageCode = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Log_Account",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_AccounType",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_Admin",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Admin_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_Application",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "AppID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_AssetType",
                        column: x => x.AssetTypeId,
                        principalTable: "AssetType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_Category",
                        column: x => x.AssetCategoryId,
                        principalTable: "category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_Desktop",
                        column: x => x.DesktopAssetTag,
                        principalTable: "Desktop",
                        principalColumn: "AssetTag",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_Docking",
                        column: x => x.DockingAssetTag,
                        principalTable: "Docking",
                        principalColumn: "AssetTag",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_Identity",
                        column: x => x.IdentityId,
                        principalTable: "Identity",
                        principalColumn: "IdenId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_IdentityType",
                        column: x => x.IdentityTypeId,
                        principalTable: "IdentityType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_KensingTone",
                        column: x => x.KensingtonId,
                        principalTable: "Kensington",
                        principalColumn: "KeyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_Language_LanguageCode",
                        column: x => x.LanguageCode,
                        principalTable: "Language",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_Laptop",
                        column: x => x.LaptopAssetTag,
                        principalTable: "Laptop",
                        principalColumn: "AssetTag",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_Menu",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_Mobile",
                        column: x => x.MobileId,
                        principalTable: "Mobile",
                        principalColumn: "IMEI",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_Permission",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_RoleType",
                        column: x => x.RoleTypeId,
                        principalTable: "RoleType",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_Screen",
                        column: x => x.ScreenAssetTag,
                        principalTable: "Screen",
                        principalColumn: "AssetTag",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_Subscription",
                        column: x => x.SubsriptionId,
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_SubscriptionType",
                        column: x => x.SubscriptionTypeId,
                        principalTable: "SubscriptionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_Token",
                        column: x => x.TokenAssetTag,
                        principalTable: "Token",
                        principalColumn: "AssetTag",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_ApplicationId",
                table: "Account",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_TypeId",
                table: "Account",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_AccountId",
                table: "Admin",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetType_CategoryId",
                table: "AssetType",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Desktop_CategoryId",
                table: "Desktop",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Desktop_IdentityId",
                table: "Desktop",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_Desktop_TypeId",
                table: "Desktop",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Docking_CategoryId",
                table: "Docking",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Docking_IdentityId",
                table: "Docking",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_Docking_TypeId",
                table: "Docking",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IdenAccount_AccountId_IdentityId_ValidFrom_ValidUntil",
                table: "IdenAccount",
                columns: new[] { "AccountId", "IdentityId", "ValidFrom", "ValidUntil" },
                unique: true,
                filter: "[IdentityId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IdenAccount_IdentityId",
                table: "IdenAccount",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_LanguageCode",
                table: "Identity",
                column: "LanguageCode");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_TypeId",
                table: "Identity",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Kensington_CategoryId",
                table: "Kensington",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Kensington_DesktopAssetTag",
                table: "Kensington",
                column: "DesktopAssetTag");

            migrationBuilder.CreateIndex(
                name: "IX_Kensington_DockingAssetTag",
                table: "Kensington",
                column: "DockingAssetTag");

            migrationBuilder.CreateIndex(
                name: "IX_Kensington_LaptopAssetTag",
                table: "Kensington",
                column: "LaptopAssetTag");

            migrationBuilder.CreateIndex(
                name: "IX_Kensington_ScreenAssetTag",
                table: "Kensington",
                column: "ScreenAssetTag");

            migrationBuilder.CreateIndex(
                name: "IX_Kensington_TypeId",
                table: "Kensington",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptop_CategoryId",
                table: "Laptop",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptop_IdentityId",
                table: "Laptop",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptop_TypeId",
                table: "Laptop",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_AccountId",
                table: "Log",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_AccountTypeId",
                table: "Log",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_AdminId",
                table: "Log",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_ApplicationId",
                table: "Log",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_AssetCategoryId",
                table: "Log",
                column: "AssetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_AssetTypeId",
                table: "Log",
                column: "AssetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_DesktopAssetTag",
                table: "Log",
                column: "DesktopAssetTag");

            migrationBuilder.CreateIndex(
                name: "IX_Log_DockingAssetTag",
                table: "Log",
                column: "DockingAssetTag");

            migrationBuilder.CreateIndex(
                name: "IX_Log_IdentityId",
                table: "Log",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_IdentityTypeId",
                table: "Log",
                column: "IdentityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_KensingtonId",
                table: "Log",
                column: "KensingtonId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_LanguageCode",
                table: "Log",
                column: "LanguageCode");

            migrationBuilder.CreateIndex(
                name: "IX_Log_LaptopAssetTag",
                table: "Log",
                column: "LaptopAssetTag");

            migrationBuilder.CreateIndex(
                name: "IX_Log_MenuId",
                table: "Log",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_MobileId",
                table: "Log",
                column: "MobileId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_PermissionId",
                table: "Log",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_RoleId",
                table: "Log",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_RoleTypeId",
                table: "Log",
                column: "RoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_ScreenAssetTag",
                table: "Log",
                column: "ScreenAssetTag");

            migrationBuilder.CreateIndex(
                name: "IX_Log_SubscriptionTypeId",
                table: "Log",
                column: "SubscriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_SubsriptionId",
                table: "Log",
                column: "SubsriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_TokenAssetTag",
                table: "Log",
                column: "TokenAssetTag");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ParentId",
                table: "Menu",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Mobile_CategoryId",
                table: "Mobile",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Mobile_IdentityId",
                table: "Mobile",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_Mobile_TypeId",
                table: "Mobile",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_TypeId",
                table: "Role",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePerm_MenuId",
                table: "RolePerm",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePerm_PermissionId",
                table: "RolePerm",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Screen_CategoryId",
                table: "Screen",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Screen_IdentityId",
                table: "Screen",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_Screen_TypeId",
                table: "Screen",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_AssetCategoryId",
                table: "Subscription",
                column: "AssetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_IdentityId",
                table: "Subscription",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_MobileId",
                table: "Subscription",
                column: "MobileId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_SubsctiptionTypeId",
                table: "Subscription",
                column: "SubsctiptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionType_AssetCategoryId",
                table: "SubscriptionType",
                column: "AssetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Token_CategoryId",
                table: "Token",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Token_IdentityId",
                table: "Token",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_Token_TypeId",
                table: "Token",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configuration");

            migrationBuilder.DropTable(
                name: "IdenAccount");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "RAM");

            migrationBuilder.DropTable(
                name: "RolePerm");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Kensington");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Desktop");

            migrationBuilder.DropTable(
                name: "Docking");

            migrationBuilder.DropTable(
                name: "Laptop");

            migrationBuilder.DropTable(
                name: "Screen");

            migrationBuilder.DropTable(
                name: "RoleType");

            migrationBuilder.DropTable(
                name: "Mobile");

            migrationBuilder.DropTable(
                name: "SubscriptionType");

            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropTable(
                name: "AccountType");

            migrationBuilder.DropTable(
                name: "Identity");

            migrationBuilder.DropTable(
                name: "AssetType");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "IdentityType");

            migrationBuilder.DropTable(
                name: "category");
        }
    }
}