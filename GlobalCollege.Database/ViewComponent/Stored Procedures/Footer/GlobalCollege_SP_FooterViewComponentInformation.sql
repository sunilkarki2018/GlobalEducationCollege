CREATE PROCEDURE [ViewComponent].[GlobalCollege_SP_FooterViewComponentInformation]  --'65947D3F-41A5-453C-A450-C26BB4373CC2','footer_style_3',NULL
		@InstitutionSetupId UNIQUEIDENTIFIER,
		@ViewComponentName NVARCHAR(200),
        @Id UNIQUEIDENTIFIER
AS
	
	
BEGIN TRY

SET NOCOUNT ON;

	 IF @ViewComponentName = 'footer_style_1'
	 BEGIN

	 SELECT(
			SELECT Id,
					   Title,
					   StartDate					   
				FROM [ContentManagement].[AdmissionSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				and RecordStatus=2
				ORDER BY PlacementOrder
				
		FOR XML PATH('AdmissionSetupDTO'),TYPE, ELEMENTS,ROOT('AdmissionList'))
		FOR XML PATH('FooterViewComponentModel'),TYPE, ELEMENTS

				
	 END

	 IF @ViewComponentName = 'footer_style_2'
	 BEGIN

			SELECT @ViewComponentName

	 END

	 IF @ViewComponentName = 'footer_style_3'
	 BEGIN

     SELECT     (SELECT [Value] FooterDescription 
				 FROM Setting.StaticDataDetails WHERE ColumnName ='FooterDescription'
				 FOR XML PATH(''),TYPE, ELEMENTS),
			   (SELECT Id,
					 Title					   
				FROM [ContentManagement].[ProgramSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				ORDER BY PlacementOrder
		FOR XML PATH('ProgramSetupDTO'),TYPE, ELEMENTS,ROOT('ProgramList')),
		(SELECT ShortDescription,
				LogoLink,
		        (SELECT CityPrefix,
			            PhoneNumber  
				 FROM ContentManagement.InstitutionContactSetup
				 ORDER BY PlacementOrder DESC
				 OFFSET 0 ROW FETCH NEXT 1 ROWS ONLY	
				 FOR XML PATH('InstitutionContactSetupDTO'),TYPE, ELEMENTS,ROOT('InstitutionContactSetups')
				)
		 FROM ContentManagement.InstitutionSetup 
		 WHERE Id = @InstitutionSetupId
		 FOR XML PATH('Institution'),TYPE, ELEMENTS)
		FOR XML PATH('FooterViewComponentModel'),TYPE, ELEMENTS

	 END

	 IF @ViewComponentName = 'footer_style_4'
	 BEGIN

	 SELECT(
			SELECT Id,
					   Title,
					   StartDate,
					   BannerImageLink
				FROM [ContentManagement].[AdmissionSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				and RecordStatus=2
				ORDER BY PlacementOrder
				
		FOR XML PATH('AdmissionSetupDTO'),TYPE, ELEMENTS,ROOT('AdmissionList'))
		FOR XML PATH('FooterViewComponentModel'),TYPE, ELEMENTS

				
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


