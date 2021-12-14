CREATE PROCEDURE [ViewComponent].[GlobalCollege_SP_MessageViewComponentInformation]
	@InstitutionSetupId UNIQUEIDENTIFIER,
	@ViewComponentName NVARCHAR(200),
    @Id UNIQUEIDENTIFIER
AS


BEGIN TRY

SET NOCOUNT ON;

	 IF @ViewComponentName = 'message_style_1'
	 BEGIN

	 SELECT 
			(SELECT Id,
					BannerImageLink,
					Quote,
					ShortDescription,
					MessageBy,
					Designation,
					ThumbnailImageLink
			FROM [ContentManagement].MessageSetup
			WHERE InstitutionSetupId = @InstitutionSetupId			
			AND RecordStatus = 2 AND FieldString1='Homepage'
			ORDER BY CreatedDate DESC 
            OFFSET 0 ROW FETCH NEXT 1 ROWS ONLY
			FOR XML PATH('Message'),TYPE, ELEMENTS),
			(SELECT Id,
					Title,
					ShortDescription,
					DetailDescription,
					BannerImageLink,
					ThumbnailImageLink
		     FROM [ContentManagement].[AboutUsSetup]
		     WHERE InstitutionSetupId = @InstitutionSetupId	
			 AND RecordStatus = 2
		     ORDER BY CreatedDate DESC 
		     OFFSET 0 ROW FETCH NEXT 6 ROWS ONLY
		     FOR XML PATH('AboutUsSetupDTO'),TYPE, ELEMENTS,ROOT('AboutUsList'))
		  FOR XML PATH('MessageViewComponentModel'),TYPE, ELEMENTS		


	 END 

	 IF @ViewComponentName = 'message_style_2'
	 BEGIN

	 SELECT 
			(SELECT Id,
			        Title,
					BannerImageLink,
					Quote,
					DetailDescription,
					ThumbnailImageLink
			FROM [ContentManagement].MessageSetup
			WHERE InstitutionSetupId = @InstitutionSetupId			
			AND RecordStatus = 2
			ORDER BY CreatedDate DESC 
            OFFSET 0 ROW FETCH NEXT 1 ROWS ONLY
			FOR XML PATH('Message'),TYPE, ELEMENTS)
		  FOR XML PATH('MessageViewComponentModel'),TYPE, ELEMENTS		


	 END 


	 IF @ViewComponentName = 'message_style_3'
	 BEGIN

	 SELECT 
			(SELECT Id,
					BannerImageLink,
					Quote,
					DetailDescription,
					ThumbnailImageLink
			FROM [ContentManagement].MessageSetup
			WHERE InstitutionSetupId = @InstitutionSetupId			
			AND RecordStatus = 2
			ORDER BY CreatedDate DESC 
            OFFSET 0 ROW FETCH NEXT 1 ROWS ONLY
			FOR XML PATH('Message'),TYPE, ELEMENTS)
		  FOR XML PATH('MessageViewComponentModel'),TYPE, ELEMENTS		


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