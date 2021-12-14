CREATE PROCEDURE [ViewComponent].[GlobalCollege_SP_TeamViewComponentInformation]
	@InstitutionSetupId UNIQUEIDENTIFIER,
	@ViewComponentName NVARCHAR(200),
	@Id UNIQUEIDENTIFIER,
	@TeamType NVARCHAR(200) = NULL,
	@FacultyId NVARCHAR(50) = NULL
AS


BEGIN TRY

SET NOCOUNT ON;


	 IF @ViewComponentName = 'team_style_1'
	 BEGIN

	SELECT		
			  (SELECT        Id,
							 FieldString1 TeamType,
							 ThumbnailImageLink,
							 [Name],
							 [TeachingSubject]
					 FROM [ContentManagement].[TeamSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId AND (FieldString1 = @TeamType OR FieldString2 = @FacultyId)
					 AND RecordStatus=2
					 ORDER BY PlacementOrder
					 FOR XML PATH('TeamSetupDTO'),TYPE,ELEMENTS,ROOT('TeamList'))
					 FOR XML PATH('TeamViewComponentModel'),TYPE, ELEMENTS

	 END

	 IF @ViewComponentName = 'team_style_2'
	 BEGIN

	 SELECT		
			  (SELECT         Id,
							 ThumbnailImageLink,
							 [Name],
							 Designation,
                             Email,
                             PhoneNumber,
                             [Address],
                             Specialization,
                             GraduatedUniversity
					 FROM [ContentManagement].[TeamSetup] 
					 WHERE InstitutionSetupId=@InstitutionSetupId AND (FieldString1 = @TeamType OR FieldString2 = @FacultyId)
					 AND RecordStatus=2
					 ORDER BY PlacementOrder
					 FOR XML PATH('TeamSetupDTO'),TYPE,ELEMENTS,ROOT('TeamList'))
					 FOR XML PATH('TeamViewComponentModel'),TYPE, ELEMENTS

	 END

	 IF @ViewComponentName = 'team_style_3'
	 BEGIN

	 SELECT
			 (SELECT         Id,
							 ThumbnailImageLink,
							 [Name],
							 Designation
					 FROM [ContentManagement].[TeamSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2  AND (FieldString1 = @TeamType OR FieldString2 = @FacultyId)
					 ORDER BY PlacementOrder
					 FOR XML PATH('TeamSetupDTO'),TYPE,ELEMENTS,ROOT('TeamList'))
					 FOR XML PATH('TeamViewComponentModel'),TYPE, ELEMENTS

	 END

	 IF @ViewComponentName = 'team_style_4'
	 BEGIN

	 SELECT
			(SELECT         Id,
							 ThumbnailImageLink,
							 [Name],
                             Designation [Subject],
                             ShortDescription [ShortDesc]					 
					 FROM [ContentManagement].[TeamSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2  AND (FieldString1 = @TeamType OR FieldString2 = @FacultyId)
					 ORDER BY CreatedDate DESC
					 FOR XML PATH('TeamSetupDTO'),TYPE,ELEMENTS,ROOT('TeamList'))
					 FOR XML PATH('TeamViewComponentModel'),TYPE, ELEMENTS

	 END

     IF @ViewComponentName = 'team_style_5'
	 BEGIN

	 SELECT
				(SELECT       Id,
							 ThumbnailImageLink,
							 [Name],
                             Designation [Subject]					 
					 FROM [ContentManagement].[TeamSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2  AND (FieldString1 = @TeamType OR FieldString2 = @FacultyId)
					 ORDER BY CreatedDate DESC
					 FOR XML PATH('TeamSetupDTO'),TYPE,ELEMENTS,ROOT('TeamList'))
					 FOR XML PATH('TeamViewComponentModel'),TYPE, ELEMENTS

	 END

	 IF @ViewComponentName = 'TeamProfile'
	 BEGIN

	 SELECT
				(SELECT  [Id]
						,[InstitutionSetupId]
						,[Name]
						,[Designation]
						,[EducationLevel]
						,[Email]
						,[PhoneNumber]
						,[Address]
						,[Specialization]
						,[GraduatedUniversity]
						,[TeachingSubject]
						,[CVDownloadLink]
						,[Website]
						,[ThumbnailImageLink]
						,[BannerImageLink]
						,[PlacementOrder]
						,[ShortDescription]
						,[FacebookLink]
						,[TwitterLink]
						,[SkypeLink]
						,[LinkedinLink]
						,[PersonalEmailAddress]	
						,(SELECT [Id]
								,[InstitutionSetupId]
								,[TeamSetupId]
								,[TeamAttributeType]
								,[Title]
								,[ThumbnailImageLink]
								,[BannerImageLink]
								,[PlacementOrder]
								,[ShortDescription]
								,[DetailDescription]
						FROM [ContentManagement].[TeamAttributeSetup]
						WHERE TeamSetupId = [ContentManagement].[TeamSetup].Id
						FOR XML PATH('TeamAttributeSetupDTO'),TYPE, ELEMENTS,ROOT('TeamAttributeSetups'))
					 FROM [ContentManagement].[TeamSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId AND Id = @Id
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 OFFSET 0 ROW FETCH NEXT 1 ROWS ONLY
					 FOR XML PATH('Profile'),TYPE,ELEMENTS)
					 FOR XML PATH('TeamViewComponentModel'),TYPE, ELEMENTS

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