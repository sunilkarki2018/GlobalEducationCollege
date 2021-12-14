CREATE PROCEDURE [ViewComponent].[GlobalCollege_SP_TestimonialsViewComponentInformation]  --'65947D3F-41A5-453C-A450-C26BB4373CC2','testimonials_style_5',NULL
		@InstitutionSetupId UNIQUEIDENTIFIER,
		@ViewComponentName NVARCHAR(200),
		@Id UNIQUEIDENTIFIER
AS


BEGIN TRY

SET NOCOUNT ON;

	 IF @ViewComponentName = 'testimonials_style_1'
	 BEGIN
	 SELECT
			(SELECT [Value] Description 
				 FROM Setting.StaticDataDetails WHERE ColumnName ='Testimonial1Description'
				 FOR XML PATH(''),TYPE, ELEMENTS),
			(SELECT          Id,
							 ThumbnailImageLink,
							 Name,
							 ShortStory [ShortDesc]
					 FROM [ContentManagement].[TestimonialSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 OFFSET 0 ROW FETCH NEXT 4 ROWS ONLY
					 FOR XML PATH('TestimonialSetupDTO'),TYPE,ELEMENTS,ROOT('TestimonialList'))
					 FOR XML PATH('TestimonialsViewComponentModel'),TYPE,ELEMENTS

	 END

	 IF @ViewComponentName = 'testimonials_style_2'
	 BEGIN
	 SELECT
			(SELECT           Id,
							 ShortStory,
                             ThumbnailImageLink,
							 Name,
							 Designation [Designation]
					 FROM [ContentManagement].[TestimonialSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 OFFSET 0 ROW FETCH NEXT 4 ROWS ONLY
					 FOR XML PATH('TestimonialSetupDTO'),TYPE,ELEMENTS,ROOT('TestimonialList'))
					 FOR XML PATH('TestimonialsViewComponentModel'),TYPE,ELEMENTS

	 END

	 IF @ViewComponentName = 'testimonials_style_3'
	 BEGIN

	 SELECT
			(SELECT           Id,
                             ThumbnailImageLink
							 ShortStory,
							 [Name]
					 FROM [ContentManagement].[TestimonialSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 OFFSET 0 ROW FETCH NEXT 4 ROWS ONLY
					 FOR XML PATH('TestimonialSetupDTO'),TYPE,ELEMENTS,ROOT('TestimonialList'))
					 FOR XML PATH('TestimonialsViewComponentModel'),TYPE,ELEMENTS

	 END

	 IF @ViewComponentName = 'testimonials_style_4'
	 BEGIN

	 SELECT
			(SELECT           Id,
                             ThumbnailImageLink
							 ShortStory,
							 [Name],
							 Designation [Designation]
					 FROM [ContentManagement].[TestimonialSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 OFFSET 0 ROW FETCH NEXT 1 ROWS ONLY
					 FOR XML PATH('TestimonialSetupDTO'),TYPE,ELEMENTS,ROOT('TestimonialList'))
					 FOR XML PATH('TestimonialsViewComponentModel'),TYPE,ELEMENTS

	 END

      IF @ViewComponentName = 'testimonials_style_5'
	 BEGIN
			SELECT(
			SELECT           Id,
                             ShortStory,
							 [Name],
							 Designation [CurrentSchoolBatch],
							 ThumbnailImageLink,
							 [Year],
							 Designation
					 FROM [ContentManagement].[TestimonialSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 FOR XML PATH('TestimonialSetupDTO'),TYPE,ELEMENTS,ROOT('TestimonialsList'))
					 FOR XML PATH('TestimonialsViewComponentModel'),TYPE,ELEMENTS

	 END

    IF @ViewComponentName = 'testimonials_style_6'
	 BEGIN

	 SELECT
			(SELECT           Id,    
							 ThumbnailImageLink,
							 [Name],
							 Designation [Profession],
							 ShortStory [ShortDesc]
					 FROM [ContentManagement].[TestimonialSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 OFFSET 0 ROW FETCH NEXT 4 ROWS ONLY
					 FOR XML PATH('TestimonialSetupDTO'),TYPE,ELEMENTS,ROOT('TestimonialList'))
					 FOR XML PATH('TestimonialsViewComponentModel'),TYPE,ELEMENTS

	 END

	 IF @ViewComponentName = 'testimonials_style_7'
	 BEGIN

			SELECT(
			SELECT           Id,
                             ShortStory,
							 [Name],
							 Designation [CurrentSchoolBatch],
							 ThumbnailImageLink,
							 [Year],
							 Designation,
							 ProgramName
					 FROM [ContentManagement].[TestimonialSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 FOR XML PATH('TestimonialSetupDTO'),TYPE,ELEMENTS,ROOT('TestimonialsList'))
					 FOR XML PATH('TestimonialsViewComponentModel'),TYPE,ELEMENTS

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


