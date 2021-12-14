CREATE TABLE [Administrator].[ApplicationUserLogin] (
    [LoginProvider] NVARCHAR (128)   NOT NULL,
    [ProviderKey]   NVARCHAR (128)   NOT NULL,
    [UserId]        UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Administrator.ApplicationUserLogin] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
    CONSTRAINT [FK_Administrator.ApplicationUserLogin_Administrator.ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [Administrator].[ApplicationUser] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [Administrator].[ApplicationUserLogin]([UserId] ASC);

