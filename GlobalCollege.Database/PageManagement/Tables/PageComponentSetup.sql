CREATE TABLE [PageManagement].[PageComponentSetup] (
    [Id]                     UNIQUEIDENTIFIER NOT NULL,
    [InstitutionSetupId]     UNIQUEIDENTIFIER NOT NULL,
    [PageSetupId]            UNIQUEIDENTIFIER NOT NULL,
    [ComponentSetupId]       UNIQUEIDENTIFIER NOT NULL,
    [ComponentType]          NVARCHAR (200)   NOT NULL,
    [DisplayOption]          BIT              NOT NULL,
    [ComponentPlacementType] INT              NOT NULL,
    [ComponentPresenceType]  INT              NOT NULL,
    [ThumbnailImageLink]     NVARCHAR (MAX)   NULL,
    [BannerImageLink]        NVARCHAR (MAX)   NULL,
    [PlacementOrder]         INT              NOT NULL,
    [TotalModification]      INT              NOT NULL,
    [FieldString1]           NVARCHAR (500)   NULL,
    [FieldString2]           NVARCHAR (500)   NULL,
    [FieldString3]           NVARCHAR (500)   NULL,
    [FieldString4]           NVARCHAR (500)   NULL,
    [FieldString5]           NVARCHAR (500)   NULL,
    [FieldString6]           NVARCHAR (500)   NULL,
    [FieldString7]           NVARCHAR (500)   NULL,
    [FieldString8]           NVARCHAR (500)   NULL,
    [FieldString9]           NVARCHAR (MAX)   NULL,
    [FieldString10]          NVARCHAR (500)   NULL,
    [FieldString11]          NVARCHAR (500)   NULL,
    [FieldString12]          NVARCHAR (500)   NULL,
    [FieldString13]          NVARCHAR (500)   NULL,
    [FieldString14]          NVARCHAR (500)   NULL,
    [FieldString15]          NVARCHAR (500)   NULL,
    [FieldString16]          NVARCHAR (500)   NULL,
    [FieldString17]          NVARCHAR (500)   NULL,
    [FieldString18]          NVARCHAR (500)   NULL,
    [FieldString19]          NVARCHAR (500)   NULL,
    [FieldString20]          NVARCHAR (500)   NULL,
    [CreatedBy]              NVARCHAR (500)   NOT NULL,
    [ModifiedBy]             NVARCHAR (500)   NOT NULL,
    [AuthorisedBy]           NVARCHAR (500)   NULL,
    [CreatedById]            UNIQUEIDENTIFIER NOT NULL,
    [ModifiedById]           UNIQUEIDENTIFIER NOT NULL,
    [AuthorisedById]         UNIQUEIDENTIFIER NULL,
    [CreatedDate]            DATETIME         NOT NULL,
    [ModifiedDate]           DATETIME         NOT NULL,
    [AuthorisedDate]         DATETIME         NULL,
    [EntityState]            INT              NOT NULL,
    [RecordStatus]           INT              NOT NULL,
    [DataEntry]              INT              NOT NULL,
    [RowVersion]             ROWVERSION       NOT NULL,
    [ChangeLog]              XML              NULL,
    [CacheDuration]          INT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PageManagement.PageComponentSetup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PageManagement.PageComponentSetup_Administrator.ApplicationUser_AuthorisedById] FOREIGN KEY ([AuthorisedById]) REFERENCES [Administrator].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_PageManagement.PageComponentSetup_Administrator.ApplicationUser_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Administrator].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_PageManagement.PageComponentSetup_Administrator.ApplicationUser_ModifiedById] FOREIGN KEY ([ModifiedById]) REFERENCES [Administrator].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_PageManagement.PageComponentSetup_ContentManagement.InstitutionSetup_InstitutionSetupId] FOREIGN KEY ([InstitutionSetupId]) REFERENCES [ContentManagement].[InstitutionSetup] ([Id]),
    CONSTRAINT [FK_PageManagement.PageComponentSetup_PageManagement.ComponentSetup_ComponentSetupId] FOREIGN KEY ([ComponentSetupId]) REFERENCES [PageManagement].[ComponentSetup] ([Id]),
    CONSTRAINT [FK_PageManagement.PageComponentSetup_PageManagement.PageSetup_PageSetupId] FOREIGN KEY ([PageSetupId]) REFERENCES [PageManagement].[PageSetup] ([Id])
);


























GO
CREATE NONCLUSTERED INDEX [IX_AuthorisedById]
    ON [PageManagement].[PageComponentSetup]([AuthorisedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ModifiedById]
    ON [PageManagement].[PageComponentSetup]([ModifiedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CreatedById]
    ON [PageManagement].[PageComponentSetup]([CreatedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PageSetupId]
    ON [PageManagement].[PageComponentSetup]([PageSetupId] ASC);


GO



GO
CREATE NONCLUSTERED INDEX [IX_ComponentSetupId]
    ON [PageManagement].[PageComponentSetup]([ComponentSetupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_InstitutionSetupId]
    ON [PageManagement].[PageComponentSetup]([InstitutionSetupId] ASC);

