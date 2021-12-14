CREATE PROCEDURE [ViewComponent].[GlobalCollege_SP_ResearchViewComponentInformation]
	@InstitutionSetupId UNIQUEIDENTIFIER,
	@ViewComponentName NVARCHAR(200),
	@Id UNIQUEIDENTIFIER
AS


BEGIN TRY

SET NOCOUNT ON;

	 IF @ViewComponentName = 'research_deatils_1'
	 BEGIN

	 DECLARE @ResearchCategoryId UNIQUEIDENTIFIER = (SELECT ResearchCategoryId FROM ContentManagement.ResearchSetup WHERE Id =@Id)

			SELECT
			   (SELECT [Id]
					  ,[InstitutionSetupId]
					  ,[ResearchCategoryId]
					  ,[Title]
					  ,[Author]
					  ,[TeamSetupId]
					  ,[ThumbnailImageLink]
					  ,[BannerImageLink]
					  ,[PlacementOrder]
					  ,[ShortDescription]
					  ,[DetailDescription]
					  ,[AuthorThumbnailImageLink]
					  ,[Designation]
					  ,[Duration]
					  ,[Website]
					  ,[DonwnloadLink]
					  ,(SELECT [Id]
						      ,[InstitutionSetupId]
						      ,[Title]
						      ,[PlacementOrder] 
					    FROM [ContentManagement].[ResearchSetup] 
					    WHERE Id = [ContentManagement].[ResearchSetup].ResearchCategoryId
						FOR XML PATH('ResearchCategory'),TYPE, ELEMENTS)
				FROM [ContentManagement].[ResearchSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				AND Id = @Id
				ORDER BY CreatedDate DESC 				
				FOR XML PATH('Research'),TYPE, ELEMENTS),
			    (SELECT [Id]
					    ,[InstitutionSetupId]
					    ,[ResearchCategoryId]
					    ,[Title]
					    ,[Author]
					    ,[TeamSetupId]
					    ,[ThumbnailImageLink]
					    ,[BannerImageLink]
					    ,[PlacementOrder]
					    ,[ShortDescription]	
						,(SELECT [Id]
						      ,[InstitutionSetupId]
						      ,[Title]
						      ,[PlacementOrder] 
					    FROM [ContentManagement].[ResearchSetup] 
					    WHERE Id = @ResearchCategoryId
						FOR XML PATH('ResearchCategory'),TYPE, ELEMENTS)
				FROM [ContentManagement].[ResearchSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				AND ResearchCategoryId = @ResearchCategoryId
				ORDER BY CreatedDate DESC 				
				FOR XML PATH('ResearchSetupDTO'),TYPE, ELEMENTS,ROOT('RealtedResearchList'))
				FOR XML PATH('ResearchViewComponentModel'),TYPE, ELEMENTS


	 END

	 IF @ViewComponentName = 'research_category_style_2'
	 BEGIN
		SELECT
		     (SELECT  Id,					   
					   Title,
					   ThumbnailImageLink,
					   BannerImageLink,
					   PlacementOrder
				FROM [ContentManagement].[ResearchCategory]
				WHERE InstitutionSetupId = @InstitutionSetupId
				ORDER BY CreatedDate DESC 				
				FOR XML PATH('ResearchCategoryDTO'),TYPE, ELEMENTS,ROOT('ResearchCategories'))
			FOR XML PATH('ResearchViewComponentModel'),TYPE, ELEMENTS

	 END

	 
	 IF @ViewComponentName = 'Research_ListByCategory_Style_3'
	 BEGIN
		SELECT
		     (SELECT Id,
					 Title,
					 ShortDescription,
					 ThumbnailImageLink,
					 (SELECT Id,					   
					         Title,
					         ThumbnailImageLink,
					         BannerImageLink,
					         PlacementOrder
				FROM [ContentManagement].[ResearchSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				AND ResearchCategoryId = [ContentManagement].[ResearchCategory].Id
				ORDER BY CreatedDate DESC 				
				FOR XML PATH('ResearchSetupDTO'),TYPE, ELEMENTS,ROOT('ResearchSetups'))
               FROM [ContentManagement].[ResearchCategory]
               WHERE InstitutionSetupId = @InstitutionSetupId
			   AND Id = @Id
               ORDER BY CreatedDate DESC 
               FOR XML PATH('ResearchCategory'),TYPE, ELEMENTS)
			FOR XML PATH('ResearchViewComponentModel'),TYPE, ELEMENTS

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