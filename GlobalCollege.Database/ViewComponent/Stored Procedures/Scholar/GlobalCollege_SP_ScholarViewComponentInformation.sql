CREATE PROCEDURE [ViewComponent].[GlobalCollege_SP_ScholarViewComponentInformation]
	@InstitutionSetupId UNIQUEIDENTIFIER,
	@ViewComponentName NVARCHAR(200),
	@Id UNIQUEIDENTIFIER
AS


BEGIN TRY

SET NOCOUNT ON;

	 IF @ViewComponentName = 'Scholar_Style_1'
	 BEGIN

			SELECT
			   (SELECT [Id]
                      ,[InstitutionSetupId]
                      ,[Title]
                      ,[ThumbnailImageLink]
                      ,[BannerImageLink]
                      ,[PlacementOrder]
                      ,[ShortDescription]
                      ,[DetailDescription]
					  ,(SELECT [Id]
							  ,[Title]
							  ,[ThumbnailImageLink]
							  ,[PlacementOrder]
							  ,[ShortDescription]
						FROM [ContentManagement].[ScholarshipsSources]
						WHERE ScholarSetupId = [ContentManagement].[ScholarSetup].Id
						ORDER BY CreatedDate DESC
						FOR XML PATH('ScholarshipsSourcesDTO'),TYPE, ELEMENTS,ROOT('ScholarshipsSourcess'))
					  ,(SELECT [Id]
							  ,[Title]
							  ,[ThumbnailImageLink]
							  ,[PlacementOrder]
							  ,[ShortDescription]
							  ,[DetailDescription]
						FROM [ContentManagement].[ScholarshipsAttributeSetup]
						WHERE ScholarSetupId = [ContentManagement].[ScholarSetup].Id
						ORDER BY CreatedDate DESC
						FOR XML PATH('ScholarshipsAttributeSetupDTO'),TYPE, ELEMENTS,ROOT('ScholarshipsAttributeSetups'))
					  ,(SELECT [Id]
							  ,[Title]
							  ,[ThumbnailImageLink]
							  ,[PlacementOrder]
							  ,[ShortDescription]
							  ,[DetailDescription]
						FROM [ContentManagement].[ScholarFAQSetup]
						WHERE ScholarSetupId = [ContentManagement].[ScholarSetup].Id
						ORDER BY CreatedDate DESC
						FOR XML PATH('ScholarFAQSetupDTO'),TYPE, ELEMENTS,ROOT('ScholarFAQSetups'))
				FROM [ContentManagement].[ScholarSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				ORDER BY CreatedDate DESC 	
				OFFSET 0 ROW FETCH NEXT 1 ROWS ONLY
				FOR XML PATH('Scholar'),TYPE, ELEMENTS)
				FOR XML PATH('ScholarViewComponentModel'),TYPE, ELEMENTS


	 END

	 IF @ViewComponentName = 'Scholar_Details_Style_2'
	 BEGIN
		SELECT
		     (SELECT Id,
					 Title,
					 ShortDescription,
					 ThumbnailImageLink,
					 [DetailDescription]
               FROM [ContentManagement].[ScholarshipsSources]
               WHERE InstitutionSetupId = @InstitutionSetupId
			   AND Id = @Id
               ORDER BY CreatedDate DESC 
               FOR XML PATH('ScholarshipsSources'),TYPE, ELEMENTS)
			FOR XML PATH('ScholarViewComponentModel'),TYPE, ELEMENTS

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