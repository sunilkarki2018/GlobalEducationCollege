CREATE PROCEDURE [ViewComponent].[GlobalCollege_SP_FAQViewComponentInformation] --'65947D3F-41A5-453C-A450-C26BB4373CC2','FAQ_Style_1',NULL
    @InstitutionSetupId UNIQUEIDENTIFIER,
	@ViewComponentName NVARCHAR(200),
	@Id UNIQUEIDENTIFIER
AS


BEGIN TRY

SET NOCOUNT ON;

	 IF @ViewComponentName = 'FAQ_Style_1'
	 BEGIN

			SELECT
			   (SELECT  Id,
						[Title],
						[FAQType],
					    [ThumbnailImageLink],
					    [BannerImageLink],
					    [PlacementOrder],
					    [ShortDescription],
					    [DetailDescription]						
				FROM [ContentManagement].[FAQSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				ORDER BY PlacementOrder 				
				FOR XML PATH('FAQSetupDTO'),TYPE, ELEMENTS,ROOT('FAQs'))
				FOR XML PATH('FAQViewComponentModel'),TYPE, ELEMENTS


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