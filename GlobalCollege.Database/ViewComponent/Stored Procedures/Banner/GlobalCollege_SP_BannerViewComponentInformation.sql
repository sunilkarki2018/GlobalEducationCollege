CREATE PROCEDURE [ViewComponent].[GlobalCollege_SP_BannerViewComponentInformation]
	@InstitutionSetupId UNIQUEIDENTIFIER,
	@ViewComponentName NVARCHAR(200),
    @Id UNIQUEIDENTIFIER
	

AS


BEGIN TRY

SET NOCOUNT ON;

	 IF @ViewComponentName = 'banner_style_1'
	 BEGIN

	 SELECT 
			(SELECT Id,
					BannerImageLink,
					Name,
					ShortDescription						 
			FROM [ContentManagement].BannerSetup
			WHERE InstitutionSetupId = @InstitutionSetupId
			AND BannerType='Video'
			AND RecordStatus = 2
			ORDER BY CreatedDate DESC 
            OFFSET 0 ROW FETCH NEXT 1 ROWS ONLY
			FOR XML PATH('VideoBanner'),TYPE, ELEMENTS),
			(SELECT Id,
					Title
		     FROM [ContentManagement].[NewsSetup]
		     WHERE InstitutionSetupId = @InstitutionSetupId			 
		     ORDER BY CreatedDate DESC 
		     OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
		     FOR XML PATH('NewsSetupDTO'),TYPE, ELEMENTS,ROOT('NewsList'))
	FOR XML PATH('BannerViewComponentModel'),TYPE, ELEMENTS
			


	 END

	 IF @ViewComponentName = 'banner_style_2'
	 BEGIN

	SELECT 

			(SELECT  Id,
                    BannerImageLink
            FROM [ContentManagement].[BannerSetup]
			WHERE InstitutionSetupId = @InstitutionSetupId
			AND RecordStatus = 2 AND BannerType='Carousel'
			ORDER BY CreatedDate DESC             
			FOR XML PATH('BannerSetupDTO'),TYPE, ELEMENTS,ROOT('BannerList'))
	FOR XML PATH('BannerViewComponentModel'),TYPE, ELEMENTS



	 END

	 IF @ViewComponentName = 'banner_style_3'
	 BEGIN

     SELECT 

			(SELECT  Id,
					 Name,
                     BannerImageLink
            FROM [ContentManagement].[BannerSetup]
			WHERE InstitutionSetupId = @InstitutionSetupId
			AND RecordStatus = 2 AND BannerType='Carousel'
			ORDER BY CreatedDate DESC             
			FOR XML PATH('BannerSetupDTO'),TYPE, ELEMENTS,ROOT('BannerList'))
	 FOR XML PATH('BannerViewComponentModel'),TYPE, ELEMENTS

	 END

	 IF @ViewComponentName = 'banner_style_4'
	 BEGIN

     SELECT 

			(SELECT  Id,
					 Name,
                     BannerImageLink
            FROM [ContentManagement].[BannerSetup]
			WHERE InstitutionSetupId = @InstitutionSetupId
			AND RecordStatus = 2 AND BannerType='Carousel'
			ORDER BY CreatedDate DESC             
			FOR XML PATH('BannerSetupDTO'),TYPE, ELEMENTS,ROOT('BannerList'))
	 FOR XML PATH('BannerViewComponentModel'),TYPE, ELEMENTS

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




