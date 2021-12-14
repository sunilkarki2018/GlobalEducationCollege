CREATE TABLE [Setting].[StaticDataDetails] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [StaticDataMasterId] UNIQUEIDENTIFIER NOT NULL,
    [ColumnName]         NVARCHAR (300)   NOT NULL,
    [Title]              NVARCHAR (300)   NOT NULL,
    [Value]              NVARCHAR (300)   NOT NULL,
    [OrderValue]         NVARCHAR (300)   NOT NULL,
    [Parameter1]         NVARCHAR (300)   NULL,
    [Parameter2]         NVARCHAR (300)   NULL,
    [Parameter3]         NVARCHAR (300)   NULL,
    [Parameter4]         NVARCHAR (300)   NULL,
    [Parameter5]         NVARCHAR (300)   NULL,
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
    CONSTRAINT [PK_Setting.StaticDataDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Setting.StaticDataDetails_Administrator.ApplicationUser_AuthorisedById] FOREIGN KEY ([AuthorisedById]) REFERENCES [Administrator].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_Setting.StaticDataDetails_Administrator.ApplicationUser_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Administrator].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_Setting.StaticDataDetails_Administrator.ApplicationUser_ModifiedById] FOREIGN KEY ([ModifiedById]) REFERENCES [Administrator].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_Setting.StaticDataDetails_Setting.StaticDataMaster_StaticDataMasterId] FOREIGN KEY ([StaticDataMasterId]) REFERENCES [Setting].[StaticDataMaster] ([Id])
);






GO
CREATE NONCLUSTERED INDEX [IX_AuthorisedById]
    ON [Setting].[StaticDataDetails]([AuthorisedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ModifiedById]
    ON [Setting].[StaticDataDetails]([ModifiedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CreatedById]
    ON [Setting].[StaticDataDetails]([CreatedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StaticDataMasterId]
    ON [Setting].[StaticDataDetails]([StaticDataMasterId] ASC);


GO


