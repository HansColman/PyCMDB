BEGIN TRANSACTION;
GO

ALTER TABLE [Language] DROP CONSTRAINT [FK_Language_Admin_LastModfiedAdminAdminId];
GO

ALTER TABLE [Log] DROP CONSTRAINT [FK_Log_Language_LanguageCode];
GO

DROP INDEX [IX_Log_LanguageCode] ON [Log];
GO

DROP INDEX [IX_Language_LastModfiedAdminAdminId] ON [Language];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Log]') AND [c].[name] = N'LanguageCode');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Log] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Log] DROP COLUMN [LanguageCode];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Language]') AND [c].[name] = N'Deactivate_reason');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Language] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Language] DROP COLUMN [Deactivate_reason];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Language]') AND [c].[name] = N'LastModfiedAdminAdminId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Language] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Language] DROP COLUMN [LastModfiedAdminAdminId];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Language]') AND [c].[name] = N'LastModifiedAdminId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Language] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Language] DROP COLUMN [LastModifiedAdminId];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Language]') AND [c].[name] = N'active');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Language] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Language] DROP COLUMN [active];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220206115531_ChangeLanguageTable', N'5.0.10');
GO

COMMIT;
GO