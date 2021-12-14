CREATE TABLE [Administrator].[ApplicationUserRole] (
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Administrator.ApplicationUserRole] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_Administrator.ApplicationUserRole_Administrator.ApplicationRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Administrator].[ApplicationRole] ([Id]),
    CONSTRAINT [FK_Administrator.ApplicationUserRole_Administrator.ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [Administrator].[ApplicationUser] ([Id])
);






GO
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [Administrator].[ApplicationUserRole]([RoleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [Administrator].[ApplicationUserRole]([UserId] ASC);

