IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AccountType] (
    [TypeID] int NOT NULL IDENTITY,
    [Type] varchar(255) NOT NULL,
    [Description] varchar(255) NOT NULL,
    [active] int NOT NULL DEFAULT 1,
    [Deactivate_reason] varchar(255) NULL,
    CONSTRAINT [PK_AccountType] PRIMARY KEY ([TypeID])
);
GO

CREATE TABLE [Application] (
    [AppID] int NOT NULL IDENTITY,
    [Name] varchar(255) NOT NULL,
    [active] int NOT NULL DEFAULT 1,
    [Deactivate_reason] varchar(255) NULL,
    CONSTRAINT [PK_Application] PRIMARY KEY ([AppID])
);
GO

CREATE TABLE [category] (
    [Id] int NOT NULL IDENTITY,
    [Category] varchar(255) NOT NULL,
    [Prefix] varchar(5) NULL,
    [active] int NOT NULL DEFAULT 1,
    [Deactivate_reason] varchar(255) NULL,
    CONSTRAINT [PK_AssetCagegory] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Configuration] (
    [Code] varchar(255) NOT NULL,
    [SubCode] varchar(255) NOT NULL,
    [CFN_Date] datetime2(0) NULL,
    [CFN_Number] int NULL,
    [CFN_Tekst] varchar(255) NULL,
    [Description] varchar(255) NULL,
    CONSTRAINT [PK_Configuration] PRIMARY KEY ([Code], [SubCode])
);
GO

CREATE TABLE [IdentityType] (
    [TypeID] int NOT NULL IDENTITY,
    [Type] varchar(255) NOT NULL,
    [Description] varchar(255) NOT NULL,
    [active] int NOT NULL DEFAULT 1,
    [Deactivate_reason] varchar(255) NULL,
    CONSTRAINT [PK_IdentityType] PRIMARY KEY ([TypeID])
);
GO

CREATE TABLE [Language] (
    [Code] nvarchar(450) NOT NULL,
    [Description] varchar(255) NOT NULL,
    [active] int NOT NULL DEFAULT 1,
    [Deactivate_reason] varchar(255) NULL,
    CONSTRAINT [PK_Language] PRIMARY KEY ([Code])
);
GO

CREATE TABLE [Menu] (
    [MenuId] int NOT NULL IDENTITY,
    [Label] varchar(255) NOT NULL,
    [URL] varchar(255) NULL,
    [ParentId] int NULL,
    CONSTRAINT [PK_Menu] PRIMARY KEY ([MenuId]),
    CONSTRAINT [FK_Menu_Menu] FOREIGN KEY ([ParentId]) REFERENCES [Menu] ([MenuId])
);
GO

CREATE TABLE [Permission] (
    [Id] int NOT NULL IDENTITY,
    [Rights] varchar(255) NOT NULL,
    [Description] varchar(255) NULL,
    CONSTRAINT [PK_PPermission] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [RAM] (
    [Id] int NOT NULL IDENTITY,
    [Value] int NOT NULL,
    [Display] varchar(255) NOT NULL,
    CONSTRAINT [PK_RAM] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [RoleType] (
    [TypeId] int NOT NULL IDENTITY,
    [Type] varchar(255) NULL,
    [Description] varchar(255) NULL,
    [active] int NOT NULL DEFAULT 1,
    [Deactivate_reason] varchar(255) NULL,
    CONSTRAINT [PK_RoleType] PRIMARY KEY ([TypeId])
);
GO

CREATE TABLE [Account] (
    [AccID] int NOT NULL IDENTITY,
    [TypeId] int NOT NULL,
    [ApplicationId] int NOT NULL,
    [UserID] varchar(255) NOT NULL,
    [active] int NOT NULL DEFAULT 1,
    [Deactivate_reason] varchar(255) NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY ([AccID]),
    CONSTRAINT [FK_Account_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application] ([AppID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Account_Type] FOREIGN KEY ([TypeId]) REFERENCES [AccountType] ([TypeID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AssetType] (
    [TypeID] int NOT NULL IDENTITY,
    [Vendor] varchar(255) NOT NULL,
    [Type] varchar(255) NOT NULL,
    [CategoryId] int NOT NULL,
    [active] int NOT NULL DEFAULT 1,
    [Deactivate_reason] varchar(255) NULL,
    CONSTRAINT [PK_AssetType] PRIMARY KEY ([TypeID]),
    CONSTRAINT [FK_AssetType_category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [category] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [SubscriptionType] (
    [Id] int NOT NULL IDENTITY,
    [Type] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Provider] nvarchar(max) NULL,
    [AssetCategoryId] int NULL,
    [active] int NOT NULL DEFAULT 1,
    [Deactivate_reason] varchar(255) NULL,
    CONSTRAINT [PK_SubscriptionType] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AssetType_Category] FOREIGN KEY ([AssetCategoryId]) REFERENCES [category] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Identity] (
    [IdenId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [EMail] nvarchar(max) NOT NULL,
    [UserID] nvarchar(max) NOT NULL,
    [Company] nvarchar(max) NOT NULL,
    [LanguageCode] nvarchar(450) NOT NULL,
    [TypeId] int NULL,
    [active] int NOT NULL DEFAULT 1,
    [Deactivate_reason] varchar(255) NULL,
    CONSTRAINT [PK_Identity] PRIMARY KEY ([IdenId]),
    CONSTRAINT [FK_Identity_Language] FOREIGN KEY ([LanguageCode]) REFERENCES [Language] ([Code]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Identity_Type] FOREIGN KEY ([TypeId]) REFERENCES [IdentityType] ([TypeID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [RolePerm] (
    [Id] int NOT NULL IDENTITY,
    [Level] int NOT NULL,
    [MenuId] int NOT NULL,
    [PermissionId] int NOT NULL,
    CONSTRAINT [PK_RolePerm] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RolePerm_Menu] FOREIGN KEY ([MenuId]) REFERENCES [Menu] ([MenuId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_RolePerm_Permission] FOREIGN KEY ([PermissionId]) REFERENCES [Permission] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Role] (
    [RoleId] int NOT NULL IDENTITY,
    [Name] varchar(255) NULL,
    [Description] varchar(255) NULL,
    [TypeId] int NULL,
    [active] int NOT NULL DEFAULT 1,
    [Deactivate_reason] varchar(255) NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([RoleId]),
    CONSTRAINT [FK_Role_Type] FOREIGN KEY ([TypeId]) REFERENCES [RoleType] ([TypeId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Admin] (
    [Admin_id] int NOT NULL IDENTITY,
    [AccountId] int NOT NULL,
    [Level] int NOT NULL,
    [Password] varchar(255) NULL,
    [DateSet] datetime2(0) NOT NULL,
    [active] int NOT NULL DEFAULT 1,
    [Deactivate_reason] varchar(255) NULL,
    CONSTRAINT [PK_Admin] PRIMARY KEY ([Admin_id]),
    CONSTRAINT [FK_Admin_Account] FOREIGN KEY ([AccountId]) REFERENCES [Account] ([AccID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Desktop] (
    [AssetTag] nvarchar(450) NOT NULL,
    [MAC] varchar(255) NULL,
    [RAM] varchar(255) NOT NULL,
    [active] int NOT NULL DEFAULT 1,
    [Deactivate_reason] varchar(255) NULL,
    [SerialNumber] varchar(255) NOT NULL,
    [TypeId] int NOT NULL,
    [CategoryId] int NULL,
    [IdentityId] int NULL,
    CONSTRAINT [PK_Desktop_Asset] PRIMARY KEY ([AssetTag]),
    CONSTRAINT [FK_Desktop_Category] FOREIGN KEY ([CategoryId]) REFERENCES [category] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Desktop_Identity] FOREIGN KEY ([IdentityId]) REFERENCES [Identity] ([IdenId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Desktop_Type] FOREIGN KEY ([TypeId]) REFERENCES [AssetType] ([TypeID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Docking] (
    [AssetTag] nvarchar(450) NOT NULL,
    [active] int NOT NULL DEFAULT 1,
    [Deactivate_reason] varchar(255) NULL,
    [SerialNumber] varchar(255) NOT NULL,
    [TypeId] int NOT NULL,
    [CategoryId] int NULL,
    [IdentityId] int NULL,
    CONSTRAINT [PK_Docking_Asset] PRIMARY KEY ([AssetTag]),
    CONSTRAINT [FK_Docking_Category] FOREIGN KEY ([CategoryId]) REFERENCES [category] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Docking_Identity] FOREIGN KEY ([IdentityId]) REFERENCES [Identity] ([IdenId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Docking_Type] FOREIGN KEY ([TypeId]) REFERENCES [AssetType] ([TypeID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [IdenAccount] (
    [ID] int NOT NULL IDENTITY,
    [ValidFrom] datetime2(0) NOT NULL,
    [ValidUntil] datetime2(0) NOT NULL,
    [IdentityId] int NULL,
    [AccountId] int NOT NULL,
    CONSTRAINT [PK_IdenAccount] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_IdenAccount_Account_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Account] ([AccID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_IdenAccount_Identity_IdentityId] FOREIGN KEY ([IdentityId]) REFERENCES [Identity] ([IdenId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Laptop] (
    [AssetTag] nvarchar(450) NOT NULL,
    [MAC] varchar(255) NULL,
    [RAM] varchar(255) NOT NULL,
    [active] int NOT NULL DEFAULT 1,
    [Deactivate_reason] varchar(255) NULL,
    [SerialNumber] varchar(255) NOT NULL,
    [TypeId] int NOT NULL,
    [CategoryId] int NULL,
    [IdentityId] int NULL,
    CONSTRAINT [PK_Laptop_Asset] PRIMARY KEY ([AssetTag]),
    CONSTRAINT [FK_Laptop_Category] FOREIGN KEY ([CategoryId]) REFERENCES [category] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Laptop_Identity] FOREIGN KEY ([IdentityId]) REFERENCES [Identity] ([IdenId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Laptop_Type] FOREIGN KEY ([TypeId]) REFERENCES [AssetType] ([TypeID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Mobile] (
    [IMEI] int NOT NULL IDENTITY,
    [TypeId] int NULL,
    [IdentityId] int NULL,
    [CategoryId] int NULL,
    [active] int NOT NULL DEFAULT 1,
    [Deactivate_reason] varchar(255) NULL,
    CONSTRAINT [PK_Mobile] PRIMARY KEY ([IMEI]),
    CONSTRAINT [FK_Mobile_Category] FOREIGN KEY ([CategoryId]) REFERENCES [category] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Mobile_Identity] FOREIGN KEY ([IdentityId]) REFERENCES [Identity] ([IdenId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Mobile_Type] FOREIGN KEY ([TypeId]) REFERENCES [AssetType] ([TypeID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Screen] (
    [AssetTag] nvarchar(450) NOT NULL,
    [active] int NOT NULL DEFAULT 1,
    [Deactivate_reason] varchar(255) NULL,
    [SerialNumber] varchar(255) NOT NULL,
    [TypeId] int NOT NULL,
    [CategoryId] int NULL,
    [IdentityId] int NULL,
    CONSTRAINT [PK_Screen_Asset] PRIMARY KEY ([AssetTag]),
    CONSTRAINT [FK_Screen_Category] FOREIGN KEY ([CategoryId]) REFERENCES [category] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Screen_Identity] FOREIGN KEY ([IdentityId]) REFERENCES [Identity] ([IdenId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Screen_Type] FOREIGN KEY ([TypeId]) REFERENCES [AssetType] ([TypeID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Token] (
    [AssetTag] nvarchar(450) NOT NULL,
    [active] int NOT NULL DEFAULT 1,
    [Deactivate_reason] varchar(255) NULL,
    [SerialNumber] varchar(255) NOT NULL,
    [TypeId] int NOT NULL,
    [CategoryId] int NULL,
    [IdentityId] int NULL,
    CONSTRAINT [PK_Token_Asset] PRIMARY KEY ([AssetTag]),
    CONSTRAINT [FK_Token_Category] FOREIGN KEY ([CategoryId]) REFERENCES [category] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Token_Identity] FOREIGN KEY ([IdentityId]) REFERENCES [Identity] ([IdenId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Token_Type] FOREIGN KEY ([TypeId]) REFERENCES [AssetType] ([TypeID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Subscription] (
    [Id] int NOT NULL IDENTITY,
    [SubsctiptionTypeId] int NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [IdentityId] int NULL,
    [MobileId] int NULL,
    [AssetCategoryId] int NULL,
    [active] int NOT NULL DEFAULT 1,
    [Deactivate_reason] varchar(255) NULL,
    CONSTRAINT [PK_Subscription] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Subscription_Category] FOREIGN KEY ([AssetCategoryId]) REFERENCES [category] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Subscription_Identity] FOREIGN KEY ([IdentityId]) REFERENCES [Identity] ([IdenId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Subscription_Mobile] FOREIGN KEY ([MobileId]) REFERENCES [Mobile] ([IMEI]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Subscription_Type] FOREIGN KEY ([SubsctiptionTypeId]) REFERENCES [SubscriptionType] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Kensington] (
    [KeyID] int NOT NULL IDENTITY,
    [SerialNumber] varchar(255) NOT NULL,
    [LaptopAssetTag] nvarchar(450) NULL,
    [DesktopAssetTag] nvarchar(450) NULL,
    [DockingAssetTag] nvarchar(450) NULL,
    [ScreenAssetTag] nvarchar(450) NULL,
    [AmountOfKeys] int NOT NULL,
    [HasLock] bit NOT NULL,
    [TypeId] int NOT NULL,
    [CategoryId] int NULL,
    [active] int NOT NULL DEFAULT 1,
    [Deactivate_reason] varchar(255) NULL,
    CONSTRAINT [PK_Kensington] PRIMARY KEY ([KeyID]),
    CONSTRAINT [FK_Kensington_Category] FOREIGN KEY ([CategoryId]) REFERENCES [category] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Kensington_Type] FOREIGN KEY ([TypeId]) REFERENCES [AssetType] ([TypeID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Key_Deskop] FOREIGN KEY ([DesktopAssetTag]) REFERENCES [Desktop] ([AssetTag]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Key_Docking] FOREIGN KEY ([DockingAssetTag]) REFERENCES [Docking] ([AssetTag]) ON DELETE SET NULL,
    CONSTRAINT [FK_Key_Laptop] FOREIGN KEY ([LaptopAssetTag]) REFERENCES [Laptop] ([AssetTag]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Key_Screen] FOREIGN KEY ([ScreenAssetTag]) REFERENCES [Screen] ([AssetTag]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Log] (
    [Id] int NOT NULL IDENTITY,
    [LogDate] datetime2 NOT NULL,
    [LogText] nvarchar(max) NOT NULL,
    [AccountId] int NULL,
    [AccountTypeId] int NULL,
    [AdminId] int NULL,
    [ApplicationId] int NULL,
    [AssetCategoryId] int NULL,
    [AssetTypeId] int NULL,
    [LaptopAssetTag] nvarchar(450) NULL,
    [DesktopAssetTag] nvarchar(450) NULL,
    [ScreenAssetTag] nvarchar(450) NULL,
    [DockingAssetTag] nvarchar(450) NULL,
    [TokenAssetTag] nvarchar(450) NULL,
    [IdentityId] int NULL,
    [IdentityTypeId] int NULL,
    [KensingtonId] int NULL,
    [MenuId] int NULL,
    [MobileId] int NULL,
    [PermissionId] int NULL,
    [SubsriptionId] int NULL,
    [SubscriptionTypeId] int NULL,
    [RoleId] int NULL,
    [RoleTypeId] int NULL,
    [LanguageCode] nvarchar(450) NULL,
    CONSTRAINT [PK_Log] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Log_Account] FOREIGN KEY ([AccountId]) REFERENCES [Account] ([AccID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_AccounType] FOREIGN KEY ([AccountTypeId]) REFERENCES [AccountType] ([TypeID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_Admin] FOREIGN KEY ([AdminId]) REFERENCES [Admin] ([Admin_id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application] ([AppID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_AssetType] FOREIGN KEY ([AssetTypeId]) REFERENCES [AssetType] ([TypeID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_Category] FOREIGN KEY ([AssetCategoryId]) REFERENCES [category] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_Desktop] FOREIGN KEY ([DesktopAssetTag]) REFERENCES [Desktop] ([AssetTag]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_Docking] FOREIGN KEY ([DockingAssetTag]) REFERENCES [Docking] ([AssetTag]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_Identity] FOREIGN KEY ([IdentityId]) REFERENCES [Identity] ([IdenId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_IdentityType] FOREIGN KEY ([IdentityTypeId]) REFERENCES [IdentityType] ([TypeID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_KensingTone] FOREIGN KEY ([KensingtonId]) REFERENCES [Kensington] ([KeyID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_Language_LanguageCode] FOREIGN KEY ([LanguageCode]) REFERENCES [Language] ([Code]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_Laptop] FOREIGN KEY ([LaptopAssetTag]) REFERENCES [Laptop] ([AssetTag]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_Menu] FOREIGN KEY ([MenuId]) REFERENCES [Menu] ([MenuId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_Mobile] FOREIGN KEY ([MobileId]) REFERENCES [Mobile] ([IMEI]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_Permission] FOREIGN KEY ([PermissionId]) REFERENCES [Permission] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_Role] FOREIGN KEY ([RoleId]) REFERENCES [Role] ([RoleId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_RoleType] FOREIGN KEY ([RoleTypeId]) REFERENCES [RoleType] ([TypeId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_Screen] FOREIGN KEY ([ScreenAssetTag]) REFERENCES [Screen] ([AssetTag]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_Subscription] FOREIGN KEY ([SubsriptionId]) REFERENCES [Subscription] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_SubscriptionType] FOREIGN KEY ([SubscriptionTypeId]) REFERENCES [SubscriptionType] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Log_Token] FOREIGN KEY ([TokenAssetTag]) REFERENCES [Token] ([AssetTag]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Account_ApplicationId] ON [Account] ([ApplicationId]);
GO

CREATE INDEX [IX_Account_TypeId] ON [Account] ([TypeId]);
GO

CREATE INDEX [IX_Admin_AccountId] ON [Admin] ([AccountId]);
GO

CREATE INDEX [IX_AssetType_CategoryId] ON [AssetType] ([CategoryId]);
GO

CREATE INDEX [IX_Desktop_CategoryId] ON [Desktop] ([CategoryId]);
GO

CREATE INDEX [IX_Desktop_IdentityId] ON [Desktop] ([IdentityId]);
GO

CREATE INDEX [IX_Desktop_TypeId] ON [Desktop] ([TypeId]);
GO

CREATE INDEX [IX_Docking_CategoryId] ON [Docking] ([CategoryId]);
GO

CREATE INDEX [IX_Docking_IdentityId] ON [Docking] ([IdentityId]);
GO

CREATE INDEX [IX_Docking_TypeId] ON [Docking] ([TypeId]);
GO

CREATE UNIQUE INDEX [IX_IdenAccount_AccountId_IdentityId_ValidFrom_ValidUntil] ON [IdenAccount] ([AccountId], [IdentityId], [ValidFrom], [ValidUntil]) WHERE [IdentityId] IS NOT NULL;
GO

CREATE INDEX [IX_IdenAccount_IdentityId] ON [IdenAccount] ([IdentityId]);
GO

CREATE INDEX [IX_Identity_LanguageCode] ON [Identity] ([LanguageCode]);
GO

CREATE INDEX [IX_Identity_TypeId] ON [Identity] ([TypeId]);
GO

CREATE INDEX [IX_Kensington_CategoryId] ON [Kensington] ([CategoryId]);
GO

CREATE INDEX [IX_Kensington_DesktopAssetTag] ON [Kensington] ([DesktopAssetTag]);
GO

CREATE INDEX [IX_Kensington_DockingAssetTag] ON [Kensington] ([DockingAssetTag]);
GO

CREATE INDEX [IX_Kensington_LaptopAssetTag] ON [Kensington] ([LaptopAssetTag]);
GO

CREATE INDEX [IX_Kensington_ScreenAssetTag] ON [Kensington] ([ScreenAssetTag]);
GO

CREATE INDEX [IX_Kensington_TypeId] ON [Kensington] ([TypeId]);
GO

CREATE INDEX [IX_Laptop_CategoryId] ON [Laptop] ([CategoryId]);
GO

CREATE INDEX [IX_Laptop_IdentityId] ON [Laptop] ([IdentityId]);
GO

CREATE INDEX [IX_Laptop_TypeId] ON [Laptop] ([TypeId]);
GO

CREATE INDEX [IX_Log_AccountId] ON [Log] ([AccountId]);
GO

CREATE INDEX [IX_Log_AccountTypeId] ON [Log] ([AccountTypeId]);
GO

CREATE INDEX [IX_Log_AdminId] ON [Log] ([AdminId]);
GO

CREATE INDEX [IX_Log_ApplicationId] ON [Log] ([ApplicationId]);
GO

CREATE INDEX [IX_Log_AssetCategoryId] ON [Log] ([AssetCategoryId]);
GO

CREATE INDEX [IX_Log_AssetTypeId] ON [Log] ([AssetTypeId]);
GO

CREATE INDEX [IX_Log_DesktopAssetTag] ON [Log] ([DesktopAssetTag]);
GO

CREATE INDEX [IX_Log_DockingAssetTag] ON [Log] ([DockingAssetTag]);
GO

CREATE INDEX [IX_Log_IdentityId] ON [Log] ([IdentityId]);
GO

CREATE INDEX [IX_Log_IdentityTypeId] ON [Log] ([IdentityTypeId]);
GO

CREATE INDEX [IX_Log_KensingtonId] ON [Log] ([KensingtonId]);
GO

CREATE INDEX [IX_Log_LanguageCode] ON [Log] ([LanguageCode]);
GO

CREATE INDEX [IX_Log_LaptopAssetTag] ON [Log] ([LaptopAssetTag]);
GO

CREATE INDEX [IX_Log_MenuId] ON [Log] ([MenuId]);
GO

CREATE INDEX [IX_Log_MobileId] ON [Log] ([MobileId]);
GO

CREATE INDEX [IX_Log_PermissionId] ON [Log] ([PermissionId]);
GO

CREATE INDEX [IX_Log_RoleId] ON [Log] ([RoleId]);
GO

CREATE INDEX [IX_Log_RoleTypeId] ON [Log] ([RoleTypeId]);
GO

CREATE INDEX [IX_Log_ScreenAssetTag] ON [Log] ([ScreenAssetTag]);
GO

CREATE INDEX [IX_Log_SubscriptionTypeId] ON [Log] ([SubscriptionTypeId]);
GO

CREATE INDEX [IX_Log_SubsriptionId] ON [Log] ([SubsriptionId]);
GO

CREATE INDEX [IX_Log_TokenAssetTag] ON [Log] ([TokenAssetTag]);
GO

CREATE INDEX [IX_Menu_ParentId] ON [Menu] ([ParentId]);
GO

CREATE INDEX [IX_Mobile_CategoryId] ON [Mobile] ([CategoryId]);
GO

CREATE INDEX [IX_Mobile_IdentityId] ON [Mobile] ([IdentityId]);
GO

CREATE INDEX [IX_Mobile_TypeId] ON [Mobile] ([TypeId]);
GO

CREATE INDEX [IX_Role_TypeId] ON [Role] ([TypeId]);
GO

CREATE INDEX [IX_RolePerm_MenuId] ON [RolePerm] ([MenuId]);
GO

CREATE INDEX [IX_RolePerm_PermissionId] ON [RolePerm] ([PermissionId]);
GO

CREATE INDEX [IX_Screen_CategoryId] ON [Screen] ([CategoryId]);
GO

CREATE INDEX [IX_Screen_IdentityId] ON [Screen] ([IdentityId]);
GO

CREATE INDEX [IX_Screen_TypeId] ON [Screen] ([TypeId]);
GO

CREATE INDEX [IX_Subscription_AssetCategoryId] ON [Subscription] ([AssetCategoryId]);
GO

CREATE INDEX [IX_Subscription_IdentityId] ON [Subscription] ([IdentityId]);
GO

CREATE INDEX [IX_Subscription_MobileId] ON [Subscription] ([MobileId]);
GO

CREATE INDEX [IX_Subscription_SubsctiptionTypeId] ON [Subscription] ([SubsctiptionTypeId]);
GO

CREATE INDEX [IX_SubscriptionType_AssetCategoryId] ON [SubscriptionType] ([AssetCategoryId]);
GO

CREATE INDEX [IX_Token_CategoryId] ON [Token] ([CategoryId]);
GO

CREATE INDEX [IX_Token_IdentityId] ON [Token] ([IdentityId]);
GO

CREATE INDEX [IX_Token_TypeId] ON [Token] ([TypeId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210825190403_InitialCreate', N'5.0.8');
GO

COMMIT;
GO


