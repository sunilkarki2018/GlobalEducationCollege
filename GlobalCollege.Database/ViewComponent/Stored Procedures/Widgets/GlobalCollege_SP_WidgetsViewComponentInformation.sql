CREATE PROCEDURE [ViewComponent].[GlobalCollege_SP_WidgetsViewComponentInformation] --'65947D3F-41A5-453C-A450-C26BB4373CC2','widget_style_11',NULL
		@InstitutionSetupId UNIQUEIDENTIFIER,		
		@ViewComponentName NVARCHAR(200),
		@Id UNIQUEIDENTIFIER
AS

BEGIN TRY

SET NOCOUNT ON;

	 IF @ViewComponentName = 'widget_style_1'
	 BEGIN

		SELECT(
            	SELECT       [Id]
                            ,[InstitutionSetupId]
                            ,[Title]
                            ,[PlacementOrder]
                            ,[ThumbnailImageLink]
                            ,[BannerImageLink]
                            ,[ShortDescription]
                            ,[DetailDescription]
					 FROM [ContentManagement].[FacultySetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 FOR XML PATH('FacultySetupDTO'),TYPE,ELEMENTS,ROOT('FacultyList'))
					 FOR XML PATH('WidgetsViewComponentModel'),TYPE,ELEMENTS

	 END

	 IF @ViewComponentName = 'widget_style_2'
	 BEGIN
			SELECT(
            	SELECT       Id,
							 EventDate,
                             Title,
                             [Time],
							 Venue
					 FROM [ContentManagement].[EventSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 OFFSET 0 ROW FETCH NEXT 4 ROWS ONLY
					 FOR XML PATH('EventSetupDTO'),TYPE,ELEMENTS,ROOT('EventList'))
					 FOR XML PATH('WidgetsViewComponentModel'),TYPE,ELEMENTS

	 END

	 IF @ViewComponentName = 'widget_style_3'
	 BEGIN

	 SELECT(
			SELECT      Id,
						Title,
						PlacementOrder
					 FROM [ContentManagement].[ProgramSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 OFFSET 0 ROW FETCH NEXT 4 ROWS ONLY
					 FOR XML PATH('ProgramSetupDTO'),TYPE,ELEMENTS,ROOT('ProgramList'))
					 FOR XML PATH('WidgetsViewComponentModel'),TYPE,ELEMENTS



	 END

	 IF @ViewComponentName = 'widget_style_4'
	 BEGIN
	       SELECT
				(SELECT  Id,
						ThumbnailImageLink
					 FROM [ContentManagement].[AdmissionSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 OFFSET 0 ROW FETCH NEXT 1 ROWS ONLY
					 FOR XML PATH('AdmissionSetupDTO'),TYPE,ELEMENTS,ROOT('AdmissionList'))
					 FOR XML PATH('WidgetsViewComponentModel'),TYPE,ELEMENTS


	 END

      IF @ViewComponentName = 'widget_style_5'
	 BEGIN

			SELECT 
			(    SELECT     Id,
					        EventDate,
					        [Time],
							Title
					    FROM [ContentManagement].[EventSetup]
				        WHERE InstitutionSetupId = @InstitutionSetupId
				        ORDER BY CreatedDate DESC 
				        OFFSET 0 ROW FETCH NEXT 3 ROWS ONLY
				        FOR XML PATH('Event'),TYPE, ELEMENTS,ROOT('EventsList')),
					 (SELECT Id,
					         ThumbnailImageLink,
					         BlogDate,
					         Author,
					         Title
				        FROM [ContentManagement].[BlogSetup]
				        WHERE InstitutionSetupId = @InstitutionSetupId
				        AND RecordStatus=2
				        ORDER BY CreatedDate DESC 
				        OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
				        FOR XML PATH('BlogSetupDTO'),TYPE, ELEMENTS,ROOT('BlogList'))
				   	FOR XML PATH('WidgetStyle5Information')

	 END

      IF @ViewComponentName = 'widget_style_6'
	 BEGIN

			SELECT 
			    (SELECT [Value] AffiliationDescription 
				 FROM Setting.StaticDataDetails WHERE ColumnName ='AffiliationDescription'
				 FOR XML PATH(''),TYPE, ELEMENTS),
				(SELECT    Id,
					       LogoLink,
						   PlacementOrder
					    FROM [ContentManagement].[InstitutionSetup]
						WHERE RecordStatus = 2
				        ORDER BY PlacementOrder 				        
				        FOR XML PATH('InstitutionSetupDTO'),TYPE, ELEMENTS,ROOT('InstitutionList')),
						(SELECT Id,
								ThumbnailImageLink
				        FROM [ContentManagement].[AffiliationSetup]
				        WHERE RecordStatus=2
				        ORDER BY CreatedDate DESC 				        
				        FOR XML PATH('AffiliationSetupDTO'),TYPE, ELEMENTS,ROOT('AffiliationList'))
				   	FOR XML PATH('WidgetsViewComponentModel'),TYPE, ELEMENTS


	 END

      IF @ViewComponentName = 'widget_style_7'
	 BEGIN

			SELECT @ViewComponentName

	 END

      IF @ViewComponentName = 'widget_style_8'
	 BEGIN
				SELECT
				(SELECT Id,
					   Title,
					   ShortDescription
				FROM [ContentManagement].[AdmissionSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				AND RecordStatus=2
				ORDER BY PlacementOrder
				OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY				
				FOR XML PATH('AdmissionSetupDTO'),TYPE, ELEMENTS,ROOT('AdmissionList'))
				FOR XML PATH('WidgetsViewComponentModel'),TYPE, ELEMENTS
				
	 END

      IF @ViewComponentName = 'widget_style_9'
	 BEGIN

	 SELECT
	       (SELECT [Value] AdmissionDescription 
				 FROM Setting.StaticDataDetails WHERE ColumnName ='AdmissionDescription'
				 FOR XML PATH(''),TYPE, ELEMENTS),
			 (SELECT   Id,
					   Title,
					   ShortDescription
				FROM [ContentManagement].[AdmissionSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				ORDER BY CreatedDate DESC 
				OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
				FOR XML PATH('AdmissionSetupDTO'),TYPE, ELEMENTS,ROOT('AdmissionList'))
				FOR XML PATH('WidgetsViewComponentModel'),TYPE, ELEMENTS

		

	 END

      IF @ViewComponentName = 'widget_style_10'
	 BEGIN

	 SELECT
			  (SELECT 	   Id,
						   Title,
						   ShortDescription [Percent]
				FROM [ContentManagement].[InstitutionAttributeSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId AND InstitutionAttributeType ='Skill'
				ORDER BY CreatedDate DESC 
				FOR XML PATH('InstitutionAttributeSetupDTO'),TYPE, ELEMENTS,ROOT('AcademicSkillList'))
				FOR XML PATH('WidgetsViewComponentModel'),TYPE, ELEMENTS
		

	 END

      IF @ViewComponentName = 'widget_style_11'
	 BEGIN
	 SELECT(
	  SELECT    Id,
				ThumbnailImageLink
				FROM [ContentManagement].[AdmissionSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				AND ThumbnailImageLink IS NOT NULL
				ORDER BY CreatedDate DESC 
				OFFSET 0 ROW FETCH NEXT 1 ROWS ONLY
				FOR XML PATH('AdmissionSetupDTO'),TYPE, ELEMENTS,ROOT('AdmissionList'))
	 FOR XML PATH('WidgetsViewComponentModel'),TYPE, ELEMENTS
		

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




