CREATE TABLE [Administrator].[ApplicationRole] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [RoleCode]          NVARCHAR (20)    NOT NULL,
    [Remarks]           NVARCHAR (300)   NOT NULL,
    [FieldString1]      NVARCHAR (500)   NULL,
    [FieldString2]      NVARCHAR (500)   NULL,
    [FieldString3]      NVARCHAR (500)   NULL,
    [FieldString4]      NVARCHAR (500)   NULL,
    [FieldString5]      NVARCHAR (500)   NULL,
    [CreatedBy]         NVARCHAR (500)   NOT NULL,
    [ModifiedBy]        NVARCHAR (500)   NOT NULL,
    [AuthorisedBy]      NVARCHAR (500)   NULL,
    [CreatedById]       UNIQUEIDENTIFIER NOT NULL,
    [ModifiedById]      UNIQUEIDENTIFIER NOT NULL,
    [AuthorisedById]    UNIQUEIDENTIFIER NULL,
    [CreatedDate]       DATETIME         NOT NULL,
    [ModifiedDate]      DATETIME         NOT NULL,
    [AuthorisedDate]    DATETIME         NULL,
    [RecordStatus]      INT              NOT NULL,
    [DataEntry]         INT              NOT NULL,
    [RowVersion]        ROWVERSION       NOT NULL,
    [Name]              NVARCHAR (256)   NOT NULL,
    [TotalModification] INT              DEFAULT ((0)) NOT NULL,
    [EntityState]       INT              DEFAULT ((0)) NOT NULL,
    [ChangeLog]         XML              NULL,
    CONSTRAINT [PK_Administrator.ApplicationRole] PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [Administrator].[ApplicationRole]([Name] ASC);

