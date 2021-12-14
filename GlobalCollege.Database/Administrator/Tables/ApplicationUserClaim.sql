CREATE TABLE [Administrator].[ApplicationUserClaim] (
    [Id]         INT              IDENTITY (1, 1) NOT NULL,
    [UserId]     UNIQUEIDENTIFIER NOT NULL,
    [ClaimType]  NVARCHAR (MAX)   NULL,
    [ClaimValue] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Administrator.ApplicationUserClaim] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Administrator.ApplicationUserClaim_Administrator.ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [Administrator].[ApplicationUser] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [Administrator].[ApplicationUserClaim]([UserId] ASC);

