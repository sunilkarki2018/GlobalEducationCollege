CREATE TABLE [ContentManagement].[TeamAttributeSetup] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [InstitutionSetupId] UNIQUEIDENTIFIER NOT NULL,
    [TeamSetupId]        UNIQUEIDENTIFIER NOT NULL,
    [TeamAttributeType]  NVARCHAR (200)   NOT NULL,
    [Title]              NVARCHAR (200)   NOT NULL,
    [ThumbnailImageLink] NVARCHAR (MAX)   NULL,
    [BannerImageLink]    NVARCHAR (MAX)   NULL,
    [PlacementOrder]     INT              NOT NULL,
    [ShortDescription]   NVARCHAR (MAX)   NOT NULL,
    [DetailDescription]  NVARCHAR (MAX)   NULL,
    [TotalModification]  INT              NOT NULL,
    [FieldString1]       NVARCHAR (500)   NULL,
    [FieldString2]       NVARCHAR (500)   NULL,
    [FieldString3]       NVARCHAR (500)   NULL,
    [FieldString4]       NVARCHAR (500)   NULL,
    [FieldString5]       NVARCHAR (500)   NULL,
    [FieldString6]       NVARCHAR (500)   NULL,
    [FieldString7]       NVARCHAR (500)   NULL,
    [FieldString8]       NVARCHAR (500)   NULL,
    [FieldString9]       NVARCHAR (MAX)   NULL,
    [FieldString10]      NVARCHAR (500)   NULL,
    [FieldString11]      NVARCHAR (500)   NULL,
    [FieldString12]      NVARCHAR (500)   NULL,
    [FieldString13]      NVARCHAR (500)   NULL,
    [FieldString14]      NVARCHAR (500)   NULL,
    [FieldString15]      NVARCHAR (500)   NULL,
    [FieldString16]      NVARCHAR (500)   NULL,
    [FieldString17]      NVARCHAR (500)   NULL,
    [FieldString18]      NVARCHAR (500)   NULL,
    [FieldString19]      NVARCHAR (500)   NULL,
    [FieldString20]      NVARCHAR (500)   NULL,
    [CreatedBy]          NVARCHAR (500)   NOT NULL,
    [ModifiedBy]         NVARCHAR (500)   NOT NULL,
    [AuthorisedBy]       NVARCHAR (500)   NULL,
    [CreatedById]        UNIQUEIDENTIFIER NOT NULL,
    [ModifiedById]       UNIQUEIDENTIFIER NOT NULL,
    [AuthorisedById]     UNIQUEIDENTIFIER NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    [ModifiedDate]       DATETIME         NOT NULL,
    [AuthorisedDate]     DATETIME         NULL,
    [EntityState]        INT              NOT NULL,
    [RecordStatus]       INT              NOT NULL,
    [DataEntry]          INT              NOT NULL,
    [RowVersion]         ROWVERSION       NOT NULL,
    [ChangeLog]          XML              NULL,
    CONSTRAINT [PK_ContentManagement.TeamAttributeSetup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContentManagement.TeamAttributeSetup_Administrator.ApplicationUser_AuthorisedById] FOREIGN KEY ([AuthorisedById]) REFERENCES [Administrator].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_ContentManagement.TeamAttributeSetup_Administrator.ApplicationUser_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Administrator].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_ContentManagement.TeamAttributeSetup_Administrator.ApplicationUser_ModifiedById] FOREIGN KEY ([ModifiedById]) REFERENCES [Administrator].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_ContentManagement.TeamAttributeSetup_ContentManagement.InstitutionSetup_InstitutionSetupId] FOREIGN KEY ([InstitutionSetupId]) REFERENCES [ContentManagement].[InstitutionSetup] ([Id]),
    CONSTRAINT [FK_ContentManagement.TeamAttributeSetup_ContentManagement.TeamSetup_TeamSetupId] FOREIGN KEY ([TeamSetupId]) REFERENCES [ContentManagement].[TeamSetup] ([Id])
);














GO
CREATE NONCLUSTERED INDEX [IX_AuthorisedById]
    ON [ContentManagement].[TeamAttributeSetup]([AuthorisedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ModifiedById]
    ON [ContentManagement].[TeamAttributeSetup]([ModifiedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CreatedById]
    ON [ContentManagement].[TeamAttributeSetup]([CreatedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TeamSetupId]
    ON [ContentManagement].[TeamAttributeSetup]([TeamSetupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_InstitutionSetupId]
    ON [ContentManagement].[TeamAttributeSetup]([InstitutionSetupId] ASC);

