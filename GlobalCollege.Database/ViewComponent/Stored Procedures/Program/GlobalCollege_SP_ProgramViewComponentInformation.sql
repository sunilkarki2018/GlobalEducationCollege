CREATE PROCEDURE [ViewComponent].[GlobalCollege_SP_ProgramViewComponentInformation] --'65947D3F-41A5-453C-A450-C26BB4373CC2','Program_style_2','bc507146-c371-4f30-80c1-2e2958c8e4c6'
	@InstitutionSetupId UNIQUEIDENTIFIER,
	@ViewComponentName NVARCHAR(200),
    @Id UNIQUEIDENTIFIER

AS


BEGIN TRY

SET NOCOUNT ON;



	 IF @ViewComponentName = 'Program_style_1'
	 BEGIN

			SELECT
			   (SELECT Id,
					   ThumbnailImageLink,
					   Title
				FROM [ContentManagement].[ProgramSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				ORDER BY PlacementOrder  				
				FOR XML PATH('ProgramSetupDTO'),TYPE, ELEMENTS,ROOT('Programs'))
				FOR XML PATH('ProgramViewComponentModel'),TYPE, ELEMENTS


	 END

	 IF @ViewComponentName = 'Program_style_2'
	 BEGIN
		SELECT
			(SELECT [Value] ProgramDescription 
				 FROM Setting.StaticDataDetails WHERE ColumnName ='Program2Description'
				 FOR XML PATH(''),TYPE, ELEMENTS),
		     (SELECT Id,
					 Title,
					 ShortDescription,
					 ThumbnailImageLink	
               FROM [ContentManagement].[ProgramSetup]
               WHERE InstitutionSetupId = @InstitutionSetupId
               ORDER BY CreatedDate DESC 
               OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
               FOR XML PATH('ProgramSetupDTO'),TYPE, ELEMENTS,ROOT('Programs'))
			FOR XML PATH('ProgramViewComponentModel'),TYPE, ELEMENTS

	 END

	 IF @ViewComponentName = 'Program_style_3'
	 BEGIN

	 SELECT
				(SELECT [Value] ProgramDescription 
				 FROM Setting.StaticDataDetails WHERE ColumnName ='Program3Description'
				 FOR XML PATH(''),TYPE, ELEMENTS),			
				 (SELECT Id,
				  Title,
				  ShortDescription
				 FROM [ContentManagement].[ProgramSetup]
				 WHERE InstitutionSetupId = @InstitutionSetupId
				 ORDER BY CreatedDate DESC 				 
				 FOR XML PATH('ProgramSetupDTO'),TYPE, ELEMENTS,ROOT('Programs'))
				 FOR XML PATH('ProgramViewComponentModel'),TYPE, ELEMENTS

	 END

	 IF @ViewComponentName = 'Program_style_4'
	 BEGIN

			 SELECT
				(SELECT [Value] ProgramDescription 
				 FROM Setting.StaticDataDetails WHERE ColumnName ='Program4Description'
				 FOR XML PATH(''),TYPE, ELEMENTS),
						 (SELECT Id,
								 Title,
								 ThumbnailImageLink
						  FROM [ContentManagement].[ProgramSetup]
						  WHERE InstitutionSetupId = @InstitutionSetupId
						  ORDER BY CreatedDate DESC
						  OFFSET 0 ROW FETCH NEXT 5 ROWS ONLY			
						  FOR XML PATH('ProgramSetupDTO'),TYPE, ELEMENTS,ROOT('Programs'))
			 FOR XML PATH('ProgramViewComponentModel'),TYPE, ELEMENTS

	 END

	 IF @ViewComponentName = 'Program_style_5'
	 BEGIN

			 SELECT
				(SELECT [Value] ProgramDescription 
				 FROM Setting.StaticDataDetails WHERE ColumnName ='Program5Description'
				 FOR XML PATH(''),TYPE, ELEMENTS),
						 (SELECT Id,
								 ThumbnailImageLink,
								 Title,
								 ShortDescription
						  FROM [ContentManagement].[ProgramSetup]
						  WHERE InstitutionSetupId = @InstitutionSetupId
						  ORDER BY CreatedDate DESC 						  
						  FOR XML PATH('ProgramSetupDTO'),TYPE, ELEMENTS,ROOT('Programs'))
			 FOR XML PATH('ProgramViewComponentModel'),TYPE, ELEMENTS

	 END

	 IF @ViewComponentName = 'Program_style_6'
	 BEGIN

			SELECT
			    (SELECT Id,
						 ThumbnailImageLink,
						 Title,
						 ShortDescription
            FROM [ContentManagement].[ProgramSetup]
            WHERE InstitutionSetupId = @InstitutionSetupId
            ORDER BY CreatedDate DESC 
            FOR XML PATH('ProgramSetupDTO'),TYPE, ELEMENTS,ROOT('Programs'))
			FOR XML PATH('ProgramViewComponentModel'),TYPE, ELEMENTS

	 END

	 IF @ViewComponentName = 'Program_Deatils_style_7'
	 BEGIN

	SELECT 

			(SELECT  Id,
					 Title,
                     BannerImageLink,
					 (SELECT Id,
							 Title,
							 DetailDescription,
							 PlacementOrder
					  FROM ContentManagement.ProgramAttributeSetup
					  WHERE ProgramSetupId = @Id
					  FOR XML PATH('ProgramAttributeSetupDTO'),TYPE, ELEMENTS,ROOT('ProgramAttributeSetups')
					  )
            FROM [ContentManagement].[ProgramSetup]
			WHERE InstitutionSetupId = @InstitutionSetupId
			AND RecordStatus = 2 AND Id = @Id
			ORDER BY CreatedDate DESC             
			FOR XML PATH('Program'),TYPE, ELEMENTS)
	FOR XML PATH('ProgramViewComponentModel'),TYPE, ELEMENTS

	END

	IF @ViewComponentName = 'Program_style_8'
	 BEGIN

			SELECT
			    (SELECT Id,
						 ThumbnailImageLink,
						 Title,
						 ShortDescription
            FROM [ContentManagement].[ProgramSetup]
            WHERE InstitutionSetupId = @InstitutionSetupId
            ORDER BY CreatedDate DESC 
            FOR XML PATH('ProgramSetupDTO'),TYPE, ELEMENTS,ROOT('Programs'))
			FOR XML PATH('ProgramViewComponentModel'),TYPE, ELEMENTS

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