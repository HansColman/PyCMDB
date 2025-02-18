BEGIN TRANSACTION;
GO

ALTER TABLE [asset] DROP CONSTRAINT [FK_Device_Category];
GO

ALTER TABLE [asset] DROP CONSTRAINT [FK_Device_Type];
GO

ALTER TABLE [AssetType] DROP CONSTRAINT [FK_AssetType_category_CategoryId];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SubscriptionType]') AND [c].[name] = N'Type');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [SubscriptionType] DROP CONSTRAINT [' + @var0 + '];');
UPDATE [SubscriptionType] SET [Type] = N'' WHERE [Type] IS NULL;
ALTER TABLE [SubscriptionType] ALTER COLUMN [Type] nvarchar(max) NOT NULL;
ALTER TABLE [SubscriptionType] ADD DEFAULT N'' FOR [Type];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SubscriptionType]') AND [c].[name] = N'Provider');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [SubscriptionType] DROP CONSTRAINT [' + @var1 + '];');
UPDATE [SubscriptionType] SET [Provider] = N'' WHERE [Provider] IS NULL;
ALTER TABLE [SubscriptionType] ALTER COLUMN [Provider] nvarchar(max) NOT NULL;
ALTER TABLE [SubscriptionType] ADD DEFAULT N'' FOR [Provider];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AssetType]') AND [c].[name] = N'CategoryId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AssetType] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [AssetType] ALTER COLUMN [CategoryId] int NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[asset]') AND [c].[name] = N'TypeId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [asset] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [asset] ALTER COLUMN [TypeId] int NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[asset]') AND [c].[name] = N'CategoryId');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [asset] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [asset] ALTER COLUMN [CategoryId] int NULL;
GO

CREATE TABLE [IdentityAccountInfos] (
    [AccId] int NOT NULL,
    [UserId] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL
);
GO

ALTER TABLE [asset] ADD CONSTRAINT [FK_Device_Category] FOREIGN KEY ([CategoryId]) REFERENCES [category] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [asset] ADD CONSTRAINT [FK_Device_Type] FOREIGN KEY ([TypeId]) REFERENCES [AssetType] ([TypeID]) ON DELETE SET NULL;
GO

ALTER TABLE [AssetType] ADD CONSTRAINT [FK_AssetType_category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [category] ([Id]) ON DELETE SET NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240712072408_updateNullableFields', N'7.0.20');
GO

COMMIT;
GO


