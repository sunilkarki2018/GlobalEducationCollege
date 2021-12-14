CREATE PROCEDURE [ViewComponent].[GlobalCollege_SP_FactsViewComponentInformation]
	@InstitutionSetupId UNIQUEIDENTIFIER,
	@ViewComponentName NVARCHAR(200),
	@Id UNIQUEIDENTIFIER
	

AS


BEGIN TRY

SET NOCOUNT ON;

  IF @ViewComponentName = 'facts_style_1'
	 BEGIN

	 SELECT
			(SELECT       Id,
						 StudentsEnrolled,
						 CertifiedTeachers,
						 CompleteCourses,
						 TotalProgram,
						 ForeignFollowers
			FROM [ContentManagement].[InstitutionSetup]
			WHERE Id = @InstitutionSetupId
			AND RecordStatus = 2
			ORDER BY CreatedDate DESC 
			FOR XML PATH('Facts'),TYPE, ELEMENTS)
			FOR XML PATH('FactsViewComponentModel'),TYPE, ELEMENTS


	 END

	 IF @ViewComponentName = 'facts_style_2'
	 BEGIN

	 SELECT
	       (SELECT       Id,
						 StudentsEnrolled,
						 CertifiedTeachers,
						 PassingtoUniversities,
						 ParentsSatisfaction
			FROM [ContentManagement].[InstitutionSetup]
			WHERE Id = @InstitutionSetupId
			AND RecordStatus = 2
			ORDER BY CreatedDate DESC 
			FOR XML PATH('Facts'),TYPE, ELEMENTS)
			FOR XML PATH('FactsViewComponentModel'),TYPE, ELEMENTS

	 END

	 IF @ViewComponentName = 'facts_style_3'
	 BEGIN

	 SELECT
	       (SELECT       Id,
						 StudentsEnrolled,
						 CertifiedTeachers,
						 CompleteCourses,
						 TotalProgram
			FROM [ContentManagement].[InstitutionSetup]
			WHERE Id = @InstitutionSetupId
			AND RecordStatus = 2
			ORDER BY CreatedDate DESC 
			FOR XML PATH('Facts'),TYPE, ELEMENTS)
			FOR XML PATH('FactsViewComponentModel'),TYPE, ELEMENTS

	 END

	 IF @ViewComponentName = 'facts_style_4'
	 BEGIN

	 SELECT
	       (SELECT        Id,
						 StudentsEnrolled,
						 CertifiedTeachers,
						 CompleteCourses,
						 TotalProgram
			FROM [ContentManagement].[InstitutionSetup]
			WHERE Id = @InstitutionSetupId
			AND RecordStatus = 2
			ORDER BY CreatedDate DESC 
			FOR XML PATH('Facts'),TYPE, ELEMENTS)
			FOR XML PATH('FactsViewComponentModel'),TYPE, ELEMENTS

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