CREATE PROCEDURE [ViewComponent].[GlobalCollege_SP_WhyusViewComponentInformation] --'65947D3F-41A5-453C-A450-C26BB4373CC2','whyus_style_1',null
		@InstitutionSetupId UNIQUEIDENTIFIER,		
		@ViewComponentName NVARCHAR(200),
		@Id UNIQUEIDENTIFIER
AS

BEGIN TRY

SET NOCOUNT ON;

	 IF @ViewComponentName = 'whyus_style_1'
	 BEGIN
	 SELECT
	 		(SELECT     Id,
						ThumbnailImageLink,
						Title,
						ShortDescription,
						DetailDescription,
						PlacementOrder
					 FROM [ContentManagement].[InstitutionAttributeSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
                     AND InstitutionAttributeType='whyus'
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 --OFFSET 0 ROW FETCH NEXT 6 ROWS ONLY
					 FOR XML PATH('InstitutionAttributeSetupDTO'),TYPE,ELEMENTS,ROOT('InstitutionAttributeList'))
					 FOR XML PATH('WhyusViewComponentModel'),TYPE,ELEMENTS

	 END

	 IF @ViewComponentName = 'whyus_style_2'
	 BEGIN

	 SELECT
			(SELECT           Id,
							 Title,
							 ShortDescription,
							 PlacementOrder
					 FROM [ContentManagement].[InstitutionAttributeSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
                     AND InstitutionAttributeType='whyus'
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 --OFFSET 0 ROW FETCH NEXT 6 ROWS ONLY
					 FOR XML PATH('InstitutionAttributeSetupDTO'),TYPE,ELEMENTS,ROOT('InstitutionAttributeList'))
					 FOR XML PATH('WhyusViewComponentModel'),TYPE,ELEMENTS


	 END

	 IF @ViewComponentName = 'whyus_style_3'
	 BEGIN

				SELECT   (SELECT    Id,
                             ThumbnailImageLink,
							 Title,
							 ShortDescription,
							 PlacementOrder
					 FROM [ContentManagement].[InstitutionAttributeSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
                     AND InstitutionAttributeType='whyus'
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 OFFSET 0 ROW FETCH NEXT 6 ROWS ONLY			
					 FOR XML PATH('InstitutionAttributeSetupDTO'),TYPE,ELEMENTS,ROOT('InstitutionAttributeList'))
				FOR XML PATH('WhyusViewComponentModel'),TYPE,ELEMENTS


	 END

	 IF @ViewComponentName = 'whyus_style_4'
	 BEGIN

	 SELECT
				(SELECT       Id,
							 ThumbnailImageLink
							 Title,
							 ShortDescription,
							 PlacementOrder
					 FROM [ContentManagement].[InstitutionAttributeSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
                     AND InstitutionAttributeType='whyus'
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 OFFSET 0 ROW FETCH NEXT 4 ROWS ONLY
					 FOR XML PATH('InstitutionAttributeSetupDTO'),TYPE,ELEMENTS,ROOT('InstitutionAttributeList'))
					 FOR XML PATH('WhyusViewComponentModel'),TYPE,ELEMENTS



	 END

      IF @ViewComponentName = 'whyus_style_5'
	 BEGIN

	 SELECT
			(SELECT			 Id,
							 Title,
							 PlacementOrder
					 FROM [ContentManagement].[InstitutionAttributeSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
                     AND InstitutionAttributeType='whyus'
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 OFFSET 0 ROW FETCH NEXT 4 ROWS ONLY
					 FOR XML PATH('InstitutionAttributeSetupDTO'),TYPE,ELEMENTS,ROOT('InstitutionAttributeList'))
					 FOR XML PATH('WhyusViewComponentModel'),TYPE,ELEMENTS

	 END

	 IF @ViewComponentName = 'whyus_style_6'
	 BEGIN

	 SELECT
			(SELECT			 Id,
							 ThumbnailImageLink,
							 Title,
							 ShortDescription,
							 PlacementOrder
					 FROM [ContentManagement].[FacilitySetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 OFFSET 0 ROW FETCH NEXT 4 ROWS ONLY
					 FOR XML PATH('FacilitySetupDTO'),TYPE,ELEMENTS,ROOT('Facilities'))
					 FOR XML PATH('WhyusViewComponentModel'),TYPE,ELEMENTS


	 END

	 IF @ViewComponentName = 'whyus_style_7'
	 BEGIN

	 SELECT
			(SELECT			 Id,
							 ThumbnailImageLink,
							 Title,
							 ShortDescription,
							 PlacementOrder
					 FROM [ContentManagement].[InstitutionAttributeSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
                     AND InstitutionAttributeType='whyus'
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 OFFSET 0 ROW FETCH NEXT 4 ROWS ONLY
					 FOR XML PATH('InstitutionAttributeSetupDTO'),TYPE,ELEMENTS,ROOT('InstitutionAttributeList'))
					 FOR XML PATH('WhyusViewComponentModel'),TYPE,ELEMENTS				

	 END

	 IF @ViewComponentName = 'whyus_style_8'
	 BEGIN

	 	 SELECT
			(SELECT			 Id,
							 ThumbnailImageLink,
							 Title,
							 ShortDescription,
							 PlacementOrder
					 FROM [ContentManagement].[FacilitySetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 OFFSET 0 ROW FETCH NEXT 4 ROWS ONLY
					 FOR XML PATH('FacilitySetupDTO'),TYPE,ELEMENTS,ROOT('Facilities'))
					 FOR XML PATH('WhyusViewComponentModel'),TYPE,ELEMENTS	

	 END 

	 IF @ViewComponentName = 'Department_Page'
	 BEGIN

	 	 SELECT 
		  (SELECT			 Id,
							 ThumbnailImageLink,
							 Title,
							 ShortDescription,
							 DetailDescription,
							 ThumbnailImageLink,
							 PlacementOrder
					 FROM [ContentManagement].[InstitutionAttributeSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2 AND InstitutionAttributeType='Administation'
					 ORDER BY CreatedDate DESC
					 FOR XML PATH('Administration'),TYPE,ELEMENTS),
		 (SELECT			 Id,
							 ThumbnailImageLink,
							 Title,
							 ShortDescription,
							 DetailDescription,
							 PlacementOrder
					 FROM [ContentManagement].[InstitutionAttributeSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2 AND InstitutionAttributeType='WhyStudyHere'
					 ORDER BY CreatedDate DESC
					 FOR XML PATH('WhyStudyHere'),TYPE,ELEMENTS),
		  (SELECT			 Id,
							 ThumbnailImageLink,
							 Title,
							 ShortDescription,
							 DetailDescription,
							 PlacementOrder
					 FROM [ContentManagement].[InstitutionAttributeSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2 AND InstitutionAttributeType='Administration Tabs'
					 ORDER BY CreatedDate DESC
					 FOR XML PATH('InstitutionAttributeSetupDTO'),TYPE,ELEMENTS,ROOT('InstitutionAttributeList')),
			(SELECT			 Id,
							 ThumbnailImageLink,
							 Title,
							 ShortDescription,
							 DetailDescription,
							 PlacementOrder
					 FROM [ContentManagement].[FacultySetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 FOR XML PATH('FacultySetupDTO'),TYPE,ELEMENTS,ROOT('Faculties'))
					 FOR XML PATH('WhyusViewComponentModel'),TYPE,ELEMENTS	

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


