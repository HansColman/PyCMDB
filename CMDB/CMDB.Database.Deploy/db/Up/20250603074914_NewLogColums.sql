BEGIN TRANSACTION;
GO

ALTER TABLE [Log] ADD [RolePermId] int NULL;
GO

ALTER TABLE [Log] ADD CONSTRAINT [FK_Log_RolePerm] FOREIGN KEY ([RoleId]) REFERENCES [RolePerm] ([Id]) ON DELETE SET NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250603074914_NewLogColums', N'8.0.11');
GO

COMMIT;
GO


