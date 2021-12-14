CREATE PROCEDURE  [ViewComponent].[GlobalCollege_SP_LifeatGCIViewComponentInformation]
    @InstitutionSetupId UNIQUEIDENTIFIER,
	@ViewComponentName NVARCHAR(200),
	@Id UNIQUEIDENTIFIER
AS


BEGIN TRY

SET NOCOUNT ON;


IF @ViewComponentName = 'LifeatGCI_Style_2'
BEGIN

	SELECT
			   (SELECT [Id],
			           [Title],
					   [ThumbnailImageLink],
					   [BannerImageLink],
					   [PlacementOrder],
					   [ShortDescription],
					   [DetailDescription]					  
				FROM [ContentManagement].[LifeAtInstitutionSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				ORDER BY CreatedDate DESC 				
				FOR XML PATH('LifeAtInstitutionSetupDTO'),TYPE, ELEMENTS,ROOT('LifeAtInstitutions'))
				FOR XML PATH('LifeatGCIViewComponentModel'),TYPE, ELEMENTS


END


	 IF @ViewComponentName = 'LifeatGCI_Style_1'
	 BEGIN

			SELECT
			   (SELECT [Id],
			           [Title],
					   [ThumbnailImageLink],
					   [BannerImageLink],
					   [PlacementOrder],
					   [ShortDescription],
					   [DetailDescription],
					   (SELECT [Id]
							  ,[InstitutionSetupId]
							  ,[LifeAtInstitutionSetupId]
							  ,[Title]
							  ,[LifeAtInstitutionAttributeType]
							  ,[ThumbnailImageLink]
							  ,[BannerImageLink]
							  ,[PlacementOrder]
							  ,[ShortDescription]
							  ,[DetailDescription]
							  ,[PlacementOrder]
						FROM [ContentManagement].[LifeAtInstitutionAttributeSetup]
						WHERE InstitutionSetupId = @InstitutionSetupId 
						AND LifeAtInstitutionSetupId = @Id
						AND RecordStatus=2
						FOR XML PATH('LifeAtInstitutionAttributeSetupDTO'),TYPE, ELEMENTS,ROOT('LifeAtInstitutionAttributeSetups'))
				FROM [ContentManagement].[LifeAtInstitutionSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				AND Id = @Id	
				FOR XML PATH('LifeAtInstitution'),TYPE, ELEMENTS)
				FOR XML PATH('LifeatGCIViewComponentModel'),TYPE, ELEMENTS


	 END
	 	 
END TRY

BEGIN CATCH
        
          INSERT INTO[dbo].[ExceptionLoggers]
                ( [Id]
                 ,[ExceptionMessage]
                 ,[ControllerName]
                 ,[ExceptionStackTrace]
                 ,[TotalModification]
                 ,[CreatedBy]
                 ,[ModifiedBy]
                 ,[AuthorisedBy]
                 ,[CreatedById]
                 ,[ModifiedById]
                 ,[AuthorisedById]
                 ,[CreatedDate]
                 ,[ModifiedDate]
                 ,[AuthorisedDate]
                 ,[EntityState]
                 ,[RecordStatus]
                 ,[DataEntry]
                 ,[ChangeLog]
                 )
             VALUES
                   ( NEWID(),
                     ERROR_MESSAGE(),
                    'DocumentCategory',
                     CONVERT(NVARCHAR(MAX), ERROR_LINE()) + ', ' + CONVERT(NVARCHAR(MAX), ERROR_NUMBER()) + ', ' + CONVERT(NVARCHAR(MAX), ERROR_STATE()),
                     1,
                    'administrator',
                    'administrator',
                    'administrator',
                    'DE69AA3E-CC18-430F-9818-6B7A45691ECF',
                    'DE69AA3E-CC18-430F-9818-6B7A45691ECF',
                    'DE69AA3E-CC18-430F-9818-6B7A45691ECF',
                     GETDATE(),
                     GETDATE(),
                     GETDATE(),
                     1,
                     2,
                     2,
                     NULL
                    )
        
END CATCH  
