CREATE PROCEDURE [ViewComponent].[GlobalCollege_SP_CourseViewComponentInformation]
	@InstitutionSetupId UNIQUEIDENTIFIER,
	@ViewComponentName NVARCHAR(200),
	@Id UNIQUEIDENTIFIER
	

AS


BEGIN TRY

SET NOCOUNT ON;

	 IF @ViewComponentName = 'course_style_1'
	 BEGIN

			SELECT
			   (SELECT Id,
					   ThumbnailImageLink,
					   Title
				FROM [ContentManagement].[CourseSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				ORDER BY CreatedDate DESC 				
				FOR XML PATH('CourseSetupDTO'),TYPE, ELEMENTS,ROOT('CourseList'))
				FOR XML PATH('CourseViewComponentModel'),TYPE, ELEMENTS


	 END

	 IF @ViewComponentName = 'course_style_2'
	 BEGIN
		SELECT
			(SELECT [Value] CourseDescription 
				 FROM Setting.StaticDataDetails WHERE ColumnName ='Course2Description'
				 FOR XML PATH(''),TYPE, ELEMENTS),
		     (SELECT Id,
					 Title,
					 ShortDescription,
					 ThumbnailImageLink	
               FROM [ContentManagement].[CourseSetup]
               WHERE InstitutionSetupId = @InstitutionSetupId
               ORDER BY CreatedDate DESC 
               OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
               FOR XML PATH('CourseSetupDTO'),TYPE, ELEMENTS,ROOT('CourseList'))
			FOR XML PATH('CourseViewComponentModel'),TYPE, ELEMENTS

	 END

	 IF @ViewComponentName = 'course_style_3'
	 BEGIN

	 SELECT
				(SELECT [Value] CourseDescription 
				 FROM Setting.StaticDataDetails WHERE ColumnName ='Course3Description'
				 FOR XML PATH(''),TYPE, ELEMENTS),			
				 (SELECT Id,
				  Title,
				  ShortDescription
				 FROM [ContentManagement].[CourseSetup]
				 WHERE InstitutionSetupId = @InstitutionSetupId
				 ORDER BY CreatedDate DESC 				 
				 FOR XML PATH('CourseSetupDTO'),TYPE, ELEMENTS,ROOT('CourseList'))
				 FOR XML PATH('CourseViewComponentModel'),TYPE, ELEMENTS

	 END

	 IF @ViewComponentName = 'course_style_4'
	 BEGIN

			 SELECT
				(SELECT [Value] CourseDescription 
				 FROM Setting.StaticDataDetails WHERE ColumnName ='Course4Description'
				 FOR XML PATH(''),TYPE, ELEMENTS),
						 (SELECT Id,
								 Title,
								 ThumbnailImageLink
						  FROM [ContentManagement].[CourseSetup]
						  WHERE InstitutionSetupId = @InstitutionSetupId
						  ORDER BY CreatedDate DESC
						  OFFSET 0 ROW FETCH NEXT 5 ROWS ONLY			
						  FOR XML PATH('CourseSetupDTO'),TYPE, ELEMENTS,ROOT('CourseList'))
			 FOR XML PATH('CourseViewComponentModel'),TYPE, ELEMENTS

	 END

	 IF @ViewComponentName = 'course_style_5'
	 BEGIN

			 SELECT
				(SELECT [Value] CourseDescription 
				 FROM Setting.StaticDataDetails WHERE ColumnName ='Course5Description'
				 FOR XML PATH(''),TYPE, ELEMENTS),
						 (SELECT Id,
								 ThumbnailImageLink,
								 Title,
								 ShortDescription
						  FROM [ContentManagement].[CourseSetup]
						  WHERE InstitutionSetupId = @InstitutionSetupId
						  ORDER BY CreatedDate DESC 						  
						  FOR XML PATH('CourseSetupDTO'),TYPE, ELEMENTS,ROOT('CourseList'))
			 FOR XML PATH('CourseViewComponentModel'),TYPE, ELEMENTS

	 END

	 IF @ViewComponentName = 'course_style_6'
	 BEGIN

			SELECT
			    (SELECT Id,
						 ThumbnailImageLink,
						 Title,
						 ShortDescription
            FROM [ContentManagement].[CourseSetup]
            WHERE InstitutionSetupId = @InstitutionSetupId
            ORDER BY CreatedDate DESC 
            FOR XML PATH('CourseSetupDTO'),TYPE, ELEMENTS,ROOT('CourseList'))
			FOR XML PATH('CourseViewComponentModel'),TYPE, ELEMENTS

	 END

	IF @ViewComponentName = 'course_details_style_8'
	 BEGIN

	SELECT  (SELECT [Value] EventDescription 
			 FROM Setting.StaticDataDetails WHERE ColumnName ='CourseApplyDescription'
			 FOR XML PATH(''),TYPE, ELEMENTS),
			(SELECT  [Id],
					 [Title],
					 [Semester],
					 [Credit],
					 [Method],
					 [SyllabusDownloadlink],
					 [ThumbnailImageLink],
					 [BannerImageLink],
					 [PlacementOrder],
					 [CourseLogoLink],
					 (SELECT Id,
							 Title,
							 DetailDescription,
							 PlacementOrder
					  FROM ContentManagement.CourseAttributeSetup
					  WHERE CourseSetupId = @Id
					  FOR XML PATH('CourseAttributeSetupDTO'),TYPE, ELEMENTS,ROOT('CourseAttributeSetups')
					  )
            FROM [ContentManagement].[CourseSetup]
			WHERE InstitutionSetupId = @InstitutionSetupId
			AND RecordStatus = 2 AND Id = @Id
			ORDER BY CreatedDate DESC             
			FOR XML PATH('Course'),TYPE, ELEMENTS)
	FOR XML PATH('CourseViewComponentModel'),TYPE, ELEMENTS

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
