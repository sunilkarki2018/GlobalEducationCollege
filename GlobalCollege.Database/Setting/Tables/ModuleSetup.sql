CREATE TABLE [Setting].[ModuleSetup] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
    [ModuleTypeSetupId]    UNIQUEIDENTIFIER NOT NULL,
    [Name]                 NVARCHAR (200)   NOT NULL,
    [ModuleCode]           NVARCHAR (20)    NOT NULL,
    [Description]          NVARCHAR (300)   NOT NULL,
    [DatabaseTable]        NVARCHAR (200)   NOT NULL,
    [ApplicationClass]     NVARCHAR (200)   NOT NULL,
    [EntryType]            NVARCHAR (5)     NOT NULL,
    [IsParent]             BIT              NOT NULL,
    [ParentModule]         NVARCHAR (200)   NULL,
    [ChangeLogRequired]    BIT              NOT NULL,
    [MakerCheckerRequired] BIT              NOT NULL,
    [TotalModification]    INT              NOT NULL,
    [FieldString1]         NVARCHAR (500)   NULL,
    [FieldString2]         NVARCHAR (500)   NULL,
    [FieldString3]         NVARCHAR (500)   NULL,
    [FieldString4]         NVARCHAR (500)   NULL,
    [FieldString5]         NVARCHAR (500)   NULL,
    [FieldString6]         NVARCHAR (500)   NULL,
    [FieldString7]         NVARCHAR (500)   NULL,
    [FieldString8]         NVARCHAR (500)   NULL,
    [FieldString9]         NVARCHAR (MAX)   NULL,
    [FieldString10]        NVARCHAR (500)   NULL,
    [FieldString11]        NVARCHAR (500)   NULL,
    [FieldString12]        NVARCHAR (500)   NULL,
    [FieldString13]        NVARCHAR (500)   NULL,
    [FieldString14]        NVARCHAR (500)   NULL,
    [FieldString15]        NVARCHAR (500)   NULL,
    [FieldString16]        NVARCHAR (500)   NULL,
    [FieldString17]        NVARCHAR (500)   NULL,
    [FieldString18]        NVARCHAR (500)   NULL,
    [FieldString19]        NVARCHAR (500)   NULL,
    [FieldString20]        NVARCHAR (500)   NULL,
    [CreatedBy]            NVARCHAR (500)   NOT NULL,
    [ModifiedBy]           NVARCHAR (500)   NOT NULL,
    [AuthorisedBy]         NVARCHAR (500)   NULL,
    [CreatedById]          UNIQUEIDENTIFIER NOT NULL,
    [ModifiedById]         UNIQUEIDENTIFIER NOT NULL,
    [AuthorisedById]       UNIQUEIDENTIFIER NULL,
    [CreatedDate]          DATETIME         NOT NULL,
    [ModifiedDate]         DATETIME         NOT NULL,
    [AuthorisedDate]       DATETIME         NULL,
    [EntityState]          INT              NOT NULL,
    [RecordStatus]         INT              NOT NULL,
    [DataEntry]            INT              NOT NULL,
    [RowVersion]           ROWVERSION       NOT NULL,
    [ChangeLog]            XML              NULL,
    CONSTRAINT [PK_Setting.ModuleSetup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Setting.ModuleSetup_Administrator.ApplicationUser_AuthorisedById] FOREIGN KEY ([AuthorisedById]) REFERENCES [Administrator].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_Setting.ModuleSetup_Administrator.ApplicationUser_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Administrator].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_Setting.ModuleSetup_Administrator.ApplicationUser_ModifiedById] FOREIGN KEY ([ModifiedById]) REFERENCES [Administrator].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_Setting.ModuleSetup_Setting.ModuleTypeSetup_ModuleTypeSetupId] FOREIGN KEY ([ModuleTypeSetupId]) REFERENCES [Setting].[ModuleTypeSetup] ([Id])
);






GO
CREATE NONCLUSTERED INDEX [IX_AuthorisedById]
    ON [Setting].[ModuleSetup]([AuthorisedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ModifiedById]
    ON [Setting].[ModuleSetup]([ModifiedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CreatedById]
    ON [Setting].[ModuleSetup]([CreatedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ModuleTypeSetupId]
    ON [Setting].[ModuleSetup]([ModuleTypeSetupId] ASC);


GO


