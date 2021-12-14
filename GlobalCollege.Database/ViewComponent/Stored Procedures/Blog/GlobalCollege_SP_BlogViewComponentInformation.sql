CREATE PROCEDURE [ViewComponent].[GlobalCollege_SP_BlogViewComponentInformation]
	@InstitutionSetupId UNIQUEIDENTIFIER,
	@ViewComponentName NVARCHAR(200),
	@Id UNIQUEIDENTIFIER
	

AS


BEGIN TRY

SET NOCOUNT ON;

	 IF @ViewComponentName = 'contentblog_style_1'
	 BEGIN

				SELECT   (SELECT [Id]
								,[Title]
								,[ThumbnailImageLink]
								,[BannerImageLink]
								,[Author]
								,[ProgramName]
								,[BlogDate]
								,[PlacementOrder]
								,[ShortDescription]
								,[DetailDescription]
			              FROM ContentManagement.BlogSetup 
			              WHERE  InstitutionSetupId = @InstitutionSetupId
						  AND Id = @Id
						  FOR XML PATH('Blog'),TYPE, ELEMENTS
						  ),
						  (SELECT Id,
								 Title,
								 ThumbnailImageLink,
								 EventDate,
								 Venue,
								 PlacementOrder
						  FROM [ContentManagement].[EventSetup]
						  WHERE InstitutionSetupId = @InstitutionSetupId
						  ORDER BY CreatedDate DESC 
						  OFFSET 0 ROW FETCH NEXT 3 ROWS ONLY			
						  FOR XML PATH('EventSetupDTO'),TYPE, ELEMENTS,ROOT('Events')),
						 (SELECT Id,
								 Title,
								 ThumbnailImageLink,
								 Author,
								 [ProgramName]
						  FROM [ContentManagement].[BlogSetup]
						  WHERE InstitutionSetupId = @InstitutionSetupId
						  ORDER BY CreatedDate DESC 
						  OFFSET 0 ROW FETCH NEXT 3 ROWS ONLY			
						  FOR XML PATH('BlogSetupDTO'),TYPE, ELEMENTS,ROOT('BlogList')),
						  (SELECT Id,
								 Title,
								 ThumbnailImageLink,
								  PlacementOrder
						  FROM [ContentManagement].[ProgramSetup]
						  WHERE InstitutionSetupId = @InstitutionSetupId		
						  FOR XML PATH('ProgramSetupDTO'),TYPE, ELEMENTS,ROOT('Programs')),
						  (SELECT Id,
								 Title,
								 ThumbnailImageLink,
								  PlacementOrder
						  FROM [ContentManagement].[AdmissionSetup]
						  WHERE InstitutionSetupId = @InstitutionSetupId			
						  FOR XML PATH('AdmissionSetupDTO'),TYPE, ELEMENTS,ROOT('Admissions'))
						  FOR XML PATH('BlogViewComponentModel'),TYPE, ELEMENTS

	 END

	 IF @ViewComponentName = 'contentblog_style_2'
	 BEGIN

			SELECT       (SELECT Id,
								 Title,
								 ThumbnailImageLink,
								 Author,
								 [ProgramName]
						  FROM [ContentManagement].[BlogSetup]
						  WHERE InstitutionSetupId = @InstitutionSetupId
						  ORDER BY CreatedDate DESC 						  			
						  FOR XML PATH('BlogSetupDTO'),TYPE, ELEMENTS,ROOT('BlogList')),			
			            (SELECT Id,
								 Title,
								 ThumbnailImageLink,
								 EventDate,
								 Venue,
								 PlacementOrder
						  FROM [ContentManagement].[EventSetup]
						  WHERE InstitutionSetupId = @InstitutionSetupId
						  ORDER BY CreatedDate DESC 
						  OFFSET 0 ROW FETCH NEXT 3 ROWS ONLY			
						  FOR XML PATH('EventSetupDTO'),TYPE, ELEMENTS,ROOT('Events')),						
						  (SELECT Id,
								 Title,
								 ThumbnailImageLink,
								  PlacementOrder
						  FROM [ContentManagement].[ProgramSetup]
						  WHERE InstitutionSetupId = @InstitutionSetupId		
						  FOR XML PATH('ProgramSetupDTO'),TYPE, ELEMENTS,ROOT('Programs')),
						  (SELECT Id,
								 Title,
								 ThumbnailImageLink,
								  PlacementOrder
						  FROM [ContentManagement].[AdmissionSetup]
						  WHERE InstitutionSetupId = @InstitutionSetupId			
						  FOR XML PATH('AdmissionSetupDTO'),TYPE, ELEMENTS,ROOT('Admissions'))
						  FOR XML PATH('BlogViewComponentModel'),TYPE, ELEMENTS

	 END

	 IF @ViewComponentName = 'contentblog_style_3'
	 BEGIN

			SELECT @ViewComponentName

	 END

	 IF @ViewComponentName = 'contentblog_style_4'
	 BEGIN

			SELECT @ViewComponentName

	 END

	 IF @ViewComponentName = 'contentblog_style_5'
	 BEGIN

			SELECT       Id,
						 ShortDescription
			FROM [ContentManagement].[BlogSetup]
			WHERE InstitutionSetupId = @InstitutionSetupId
			AND RecordStatus = 2
			ORDER BY CreatedDate DESC 
            OFFSET 0 ROW FETCH NEXT 1 ROWS ONLY
			FOR XML PATH('contentblogstyle5')

	 END

	 IF @ViewComponentName = 'contentblog_style_6'
	 BEGIN

			SELECT
			   (SELECT Id,
					   Title,
					   ShortDescription
				FROM [ContentManagement].[ScholarshipsSources]
				WHERE InstitutionSetupId = @InstitutionSetupId
				ORDER BY CreatedDate DESC 
				OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
				FOR XML PATH('Scholarships'),TYPE, ELEMENTS,ROOT('ScholorBlockList'))

	 END

	 IF @ViewComponentName = 'contentblog_style_7'
	 BEGIN

			SELECT       Id,
						 ShortDescription,
						 ThumbnailImageLink
			FROM [ContentManagement].[AboutUsSetup]
			WHERE InstitutionSetupId = @InstitutionSetupId
			AND RecordStatus = 2
			ORDER BY CreatedDate DESC 
            OFFSET 0 ROW FETCH NEXT 1 ROWS ONLY
			FOR XML PATH('contentblogstyle7')

	 END

	 IF @ViewComponentName = 'contentblog_style_8'
	 BEGIN

			SELECT @ViewComponentName

	 END

	 IF @ViewComponentName = 'contentblog_style_9'
	 BEGIN

			SELECT       Id,
						 ThumbnailImageLink,
						 ''ShortDescription
						 FROM [ContentManagement].[InstitutionSetup]
			WHERE Id = @InstitutionSetupId
			AND RecordStatus = 2
			ORDER BY CreatedDate DESC 
            OFFSET 0 ROW FETCH NEXT 1 ROWS ONLY
			FOR XML PATH('contentblogstyle8')

	 END

	 IF @ViewComponentName = 'contentblog_style_10'
	 BEGIN

			SELECT       Id,
						 ShortDescription
			FROM [ContentManagement].[AboutUsSetup]
			WHERE InstitutionSetupId = @InstitutionSetupId
			AND RecordStatus = 2
			ORDER BY CreatedDate DESC 
            OFFSET 0 ROW FETCH NEXT 1 ROWS ONLY
			FOR XML PATH('contentblogstyle10')

	 END

	 IF @ViewComponentName = 'contentblog_style_11'
	 BEGIN

			SELECT       Id,
						 MessageBy,
						 Designation,
						 ShortDescription
			FROM [ContentManagement].[MessageSetup]
			WHERE InstitutionSetupId = @InstitutionSetupId
			AND RecordStatus = 2
			ORDER BY CreatedDate DESC 
            OFFSET 0 ROW FETCH NEXT 1 ROWS ONLY
			FOR XML PATH('contentblogstyle11')

	 END

	 IF @ViewComponentName = 'contentblog_style_12'
	 BEGIN

			SELECT       Id,
						 MessageBy,
						 ''Quote
			FROM [ContentManagement].[MessageSetup]
			WHERE InstitutionSetupId = @InstitutionSetupId
			AND RecordStatus = 2
			ORDER BY CreatedDate DESC 
            OFFSET 0 ROW FETCH NEXT 1 ROWS ONLY
			FOR XML PATH('contentblogstyle12')

	 END

	 IF @ViewComponentName = 'contentblog_style_13'
	 BEGIN

			SELECT       Id,
						 ThumbnailImageLink
			FROM [ContentManagement].InstitutionSetup
			WHERE Id = @InstitutionSetupId
			AND RecordStatus = 2
			ORDER BY CreatedDate DESC 
            OFFSET 0 ROW FETCH NEXT 1 ROWS ONLY
			FOR XML PATH('contentblogstyle13')

	 END

	 IF @ViewComponentName = 'contentblog_style_14'
	 BEGIN

			SELECT
			   (SELECT Id,
					   Title
				FROM [ContentManagement].[CourseSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				ORDER BY CreatedDate DESC 
				OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
				FOR XML PATH('Course'),TYPE, ELEMENTS,ROOT('CourseList'))


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
