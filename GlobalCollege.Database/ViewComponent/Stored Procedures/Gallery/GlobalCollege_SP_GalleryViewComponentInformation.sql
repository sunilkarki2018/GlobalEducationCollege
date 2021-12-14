CREATE PROCEDURE [ViewComponent].[GlobalCollege_SP_GalleryViewComponentInformation]
	@InstitutionSetupId UNIQUEIDENTIFIER,
	@ViewComponentName NVARCHAR(200),
	@Id UNIQUEIDENTIFIER
AS

BEGIN TRY

SET NOCOUNT ON;

	 IF @ViewComponentName = 'GalleryCategory_Style_1'
	 BEGIN

			SELECT
			   (SELECT Id,
					   ThumbnailImageLink,
					   Title
				FROM [ContentManagement].[GalleryCategorySetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				ORDER BY CreatedDate DESC 				
				FOR XML PATH('GalleryCategorySetupDTO'),TYPE, ELEMENTS,ROOT('GalleryCategories'))
				FOR XML PATH('GalleryViewComponentModel'),TYPE, ELEMENTS


	 END

	 IF @ViewComponentName = 'GalleryCategoryDetails_Style_2'
	 BEGIN
		SELECT
		     (SELECT Id,
					 Title,
					 ShortDescription,
					 ThumbnailImageLink,
					 (SELECT Id,					   
					   Title,
					   ThumbnailImageLink,
					   BannerImageLink
				FROM [ContentManagement].[GallerySetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				AND GalleryCategorySetupId = [ContentManagement].[GalleryCategorySetup].Id
				ORDER BY CreatedDate DESC 				
				FOR XML PATH('GallerySetupDTO'),TYPE, ELEMENTS,ROOT('GallerySetups'))
               FROM [ContentManagement].[GalleryCategorySetup]
               WHERE InstitutionSetupId = @InstitutionSetupId
			   AND Id = @Id
               ORDER BY CreatedDate DESC 
               FOR XML PATH('GalleryCategory'),TYPE, ELEMENTS)
			FOR XML PATH('GalleryViewComponentModel'),TYPE, ELEMENTS

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