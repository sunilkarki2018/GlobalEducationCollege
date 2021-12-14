CREATE TABLE [ContentManagement].[InstitutionSetup] (
    [Id]                    UNIQUEIDENTIFIER NOT NULL,
    [RegisteredName]        NVARCHAR (200)   NOT NULL,
    [InstitutionType]       NVARCHAR (200)   NOT NULL,
    [CommericalName]        NVARCHAR (200)   NOT NULL,
    [URL]                   NVARCHAR (MAX)   NOT NULL,
    [IncorporationPerson]   NVARCHAR (200)   NULL,
    [KnownSince]            NVARCHAR (200)   NULL,
    [InstitutionSector]     NVARCHAR (200)   NULL,
    [InstitutionSubsector]  NVARCHAR (200)   NULL,
    [NatureofInstitution]   NVARCHAR (200)   NULL,
    [PermitIssueDate]       DATETIME         NULL,
    [PermitValidThrough]    DATETIME         NOT NULL,
    [IssuingAuthority]      NVARCHAR (200)   NULL,
    [ForeignFollowers]      NVARCHAR (200)   NULL,
    [CertifiedTeachers]     NVARCHAR (200)   NULL,
    [StudentsEnrolled]      NVARCHAR (200)   NULL,
    [CompleteCourses]       NVARCHAR (200)   NULL,
    [TotalProgram]          NVARCHAR (200)   NULL,
    [PassingtoUniversities] NVARCHAR (200)   NULL,
    [ParentsSatisfaction]   NVARCHAR (200)   NULL,
    [ThumbnailImageLink]    NVARCHAR (MAX)   NULL,
    [BannerImageLink]       NVARCHAR (MAX)   NULL,
    [LogoLink]              NVARCHAR (MAX)   NOT NULL,
    [PlacementOrder]        INT              NOT NULL,
    [ShortDescription]      NVARCHAR (MAX)   NOT NULL,
    [DetailDescription]     NVARCHAR (MAX)   NULL,
    [TotalModification]     INT              NOT NULL,
    [FieldString1]          NVARCHAR (500)   NULL,
    [FieldString2]          NVARCHAR (500)   NULL,
    [FieldString3]          NVARCHAR (500)   NULL,
    [FieldString4]          NVARCHAR (500)   NULL,
    [FieldString5]          NVARCHAR (500)   NULL,
    [FieldString6]          NVARCHAR (500)   NULL,
    [FieldString7]          NVARCHAR (500)   NULL,
    [FieldString8]          NVARCHAR (500)   NULL,
    [FieldString9]          NVARCHAR (MAX)   NULL,
    [FieldString10]         NVARCHAR (500)   NULL,
    [FieldString11]         NVARCHAR (500)   NULL,
    [FieldString12]         NVARCHAR (500)   NULL,
    [FieldString13]         NVARCHAR (500)   NULL,
    [FieldString14]         NVARCHAR (500)   NULL,
    [FieldString15]         NVARCHAR (500)   NULL,
    [FieldString16]         NVARCHAR (500)   NULL,
    [FieldString17]         NVARCHAR (500)   NULL,
    [FieldString18]         NVARCHAR (500)   NULL,
    [FieldString19]         NVARCHAR (500)   NULL,
    [FieldString20]         NVARCHAR (500)   NULL,
    [CreatedBy]             NVARCHAR (500)   NOT NULL,
    [ModifiedBy]            NVARCHAR (500)   NOT NULL,
    [AuthorisedBy]          NVARCHAR (500)   NULL,
    [CreatedById]           UNIQUEIDENTIFIER NOT NULL,
    [ModifiedById]          UNIQUEIDENTIFIER NOT NULL,
    [AuthorisedById]        UNIQUEIDENTIFIER NULL,
    [CreatedDate]           DATETIME         NOT NULL,
    [ModifiedDate]          DATETIME         NOT NULL,
    [AuthorisedDate]        DATETIME         NULL,
    [EntityState]           INT              NOT NULL,
    [RecordStatus]          INT              NOT NULL,
    [DataEntry]             INT              NOT NULL,
    [RowVersion]            ROWVERSION       NOT NULL,
    [ChangeLog]             XML              NULL,
    CONSTRAINT [PK_ContentManagement.InstitutionSetup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContentManagement.InstitutionSetup_Administrator.ApplicationUser_AuthorisedById] FOREIGN KEY ([AuthorisedById]) REFERENCES [Administrator].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_ContentManagement.InstitutionSetup_Administrator.ApplicationUser_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Administrator].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_ContentManagement.InstitutionSetup_Administrator.ApplicationUser_ModifiedById] FOREIGN KEY ([ModifiedById]) REFERENCES [Administrator].[ApplicationUser] ([Id])
);








GO
CREATE NONCLUSTERED INDEX [IX_AuthorisedById]
    ON [ContentManagement].[InstitutionSetup]([AuthorisedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ModifiedById]
    ON [ContentManagement].[InstitutionSetup]([ModifiedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CreatedById]
    ON [ContentManagement].[InstitutionSetup]([CreatedById] ASC);

