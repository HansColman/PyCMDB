BEGIN TRANSACTION;
GO

ALTER TABLE [Log] DROP CONSTRAINT [FK_Log_RolePerm]
GO


ALTER TABLE [Log] ADD CONSTRAINT [FK_Log_RolePerm] FOREIGN KEY ([RolePermId]) REFERENCES [RolePerm] ([Id]) ON DELETE SET NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250605071223_CorectRoleLink', N'8.0.11');
GO

COMMIT;
GO