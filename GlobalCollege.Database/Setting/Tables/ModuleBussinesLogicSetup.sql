CREATE TABLE [Setting].[ModuleBussinesLogicSetup] (
    [Id]                        UNIQUEIDENTIFIER NOT NULL,
    [ModuleSetupId]             UNIQUEIDENTIFIER NOT NULL,
    [Name]                      NVARCHAR (300)   NOT NULL,
    [ColumnName]                NVARCHAR (200)   NOT NULL,
    [Description]               NVARCHAR (300)   NOT NULL,
    [DataType]                  NVARCHAR (100)   NOT NULL,
    [StringLength]              INT              NOT NULL,
    [Required]                  BIT              NOT NULL,
    [Position]                  INT              NOT NULL,
    [HtmlDataType]              NVARCHAR (100)   NOT NULL,
    [HtmlSize]                  INT              NOT NULL,
    [LabelIcon]                 NVARCHAR (100)   NULL,
    [DefaultValue]              NVARCHAR (MAX)   NULL,
    [FilePath]                  NVARCHAR (MAX)   NULL,
    [CanUpdate]                 BIT              NOT NULL,
    [IsParentColumn]            BIT              NOT NULL,
    [HelpMessage]               NVARCHAR (300)   NOT NULL,
    [SummaryHeader]             BIT              NOT NULL,
    [ParameterForSummaryHeader] BIT              NOT NULL,
    [IsForeignKey]              BIT              NOT NULL,
    [ForeignTable]              NVARCHAR (200)   NULL,
    [DataSource]                NVARCHAR (MAX)   NULL,
    [IsStaticDropDown]          BIT              NOT NULL,
    [ParameterisedDataSorce]    BIT              NOT NULL,
    [Parameters]                NVARCHAR (MAX)   NULL,
    [TotalModification]         INT              NOT NULL,
    [FieldString1]              NVARCHAR (500)   NULL,
    [FieldString2]              NVARCHAR (500)   NULL,
    [FieldString3]              NVARCHAR (500)   NULL,
    [FieldString4]              NVARCHAR (500)   NULL,
    [FieldString5]              NVARCHAR (500)   NULL,
    [FieldString6]              NVARCHAR (500)   NULL,
    [FieldString7]              NVARCHAR (500)   NULL,
    [FieldString8]              NVARCHAR (500)   NULL,
    [FieldString9]              NVARCHAR (MAX)   NULL,
    [FieldString10]             NVARCHAR (500)   NULL,
    [FieldString11]             NVARCHAR (500)   NULL,
    [FieldString12]             NVARCHAR (500)   NULL,
    [FieldString13]             NVARCHAR (500)   NULL,
    [FieldString14]             NVARCHAR (500)   NULL,
    [FieldString15]             NVARCHAR (500)   NULL,
    [FieldString16]             NVARCHAR (500)   NULL,
    [FieldString17]             NVARCHAR (500)   NULL,
    [FieldString18]             NVARCHAR (500)   NULL,
    [FieldString19]             NVARCHAR (500)   NULL,
    [FieldString20]             NVARCHAR (500)   NULL,
    [CreatedBy]                 NVARCHAR (500)   NOT NULL,
    [ModifiedBy]                NVARCHAR (500)   NOT NULL,
    [AuthorisedBy]              NVARCHAR (500)   NULL,
    [CreatedById]               UNIQUEIDENTIFIER NOT NULL,
    [ModifiedById]              UNIQUEIDENTIFIER NOT NULL,
    [AuthorisedById]            UNIQUEIDENTIFIER NULL,
    [CreatedDate]               DATETIME         NOT NULL,
    [ModifiedDate]              DATETIME         NOT NULL,
    [AuthorisedDate]            DATETIME         NULL,
    [EntityState]               INT              NOT NULL,
    [RecordStatus]              INT              NOT NULL,
    [DataEntry]                 INT              NOT NULL,
    [RowVersion]                ROWVERSION       NOT NULL,
    [ChangeLog]                 XML              NULL,
    CONSTRAINT [PK_Setting.ModuleBussinesLogicSetup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Setting.ModuleBussinesLogicSetup_Administrator.ApplicationUser_AuthorisedById] FOREIGN KEY ([AuthorisedById]) REFERENCES [Administrator].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_Setting.ModuleBussinesLogicSetup_Administrator.ApplicationUser_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Administrator].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_Setting.ModuleBussinesLogicSetup_Administrator.ApplicationUser_ModifiedById] FOREIGN KEY ([ModifiedById]) REFERENCES [Administrator].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_Setting.ModuleBussinesLogicSetup_Setting.ModuleSetup_ModuleSetupId] FOREIGN KEY ([ModuleSetupId]) REFERENCES [Setting].[ModuleSetup] ([Id])
);






GO
CREATE NONCLUSTERED INDEX [IX_AuthorisedById]
    ON [Setting].[ModuleBussinesLogicSetup]([AuthorisedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ModifiedById]
    ON [Setting].[ModuleBussinesLogicSetup]([ModifiedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CreatedById]
    ON [Setting].[ModuleBussinesLogicSetup]([CreatedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ModuleSetupId]
    ON [Setting].[ModuleBussinesLogicSetup]([ModuleSetupId] ASC);


GO


