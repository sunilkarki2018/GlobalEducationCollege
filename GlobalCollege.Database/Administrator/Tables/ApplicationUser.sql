CREATE TABLE [Administrator].[ApplicationUser] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
    [FullName]             NVARCHAR (500)   NOT NULL,
    [UserRegistrationId]   UNIQUEIDENTIFIER NULL,
    [InstitutionSetupId]   UNIQUEIDENTIFIER NOT NULL,
    [SubscriptionDetails]  NVARCHAR (MAX)   NULL,
    [ADUsername]           NVARCHAR (256)   NULL,
    [ADEnable]             BIT              NOT NULL,
    [CreatedBy]            NVARCHAR (500)   NOT NULL,
    [ModifiedBy]           NVARCHAR (500)   NOT NULL,
    [AuthorisedBy]         NVARCHAR (500)   NULL,
    [CreatedById]          UNIQUEIDENTIFIER NOT NULL,
    [ModifiedById]         UNIQUEIDENTIFIER NOT NULL,
    [AuthorisedById]       UNIQUEIDENTIFIER NULL,
    [CreatedDate]          DATETIME         NOT NULL,
    [ModifiedDate]         DATETIME         NOT NULL,
    [AuthorisedDate]       DATETIME         NULL,
    [RecordStatus]         INT              NOT NULL,
    [DataEntry]            INT              NOT NULL,
    [RowVersion]           ROWVERSION       NOT NULL,
    [Email]                NVARCHAR (256)   NULL,
    [EmailConfirmed]       BIT              NOT NULL,
    [PasswordHash]         NVARCHAR (MAX)   NULL,
    [SecurityStamp]        NVARCHAR (MAX)   NULL,
    [PhoneNumber]          NVARCHAR (MAX)   NULL,
    [PhoneNumberConfirmed] BIT              NOT NULL,
    [TwoFactorEnabled]     BIT              NOT NULL,
    [LockoutEndDateUtc]    DATETIME         NULL,
    [LockoutEnabled]       BIT              NOT NULL,
    [AccessFailedCount]    INT              NOT NULL,
    [UserName]             NVARCHAR (256)   NOT NULL,
    [TotalModification]    INT              DEFAULT ((0)) NOT NULL,
    [EntityState]          INT              DEFAULT ((0)) NOT NULL,
    [ChangeLog]            XML              NULL,
    CONSTRAINT [PK_Administrator.ApplicationUser] PRIMARY KEY CLUSTERED ([Id] ASC)
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [Administrator].[ApplicationUser]([UserName] ASC);

