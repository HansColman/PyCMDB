BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Type]') AND [c].[name] = N'Discriminator');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Type] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Type] ALTER COLUMN [Discriminator] nvarchar(13) NOT NULL;
GO

ALTER TABLE [Kensington] ADD [AssetTag] nvarchar(max) NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[asset]') AND [c].[name] = N'Discriminator');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [asset] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [asset] ALTER COLUMN [Discriminator] nvarchar(8) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250311115106_KensingtonForeignKey', N'8.0.11');
GO

COMMIT;
GO


