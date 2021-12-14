CREATE PROCEDURE [ViewComponent].[GlobalCollege_SP_MenuViewComponentInformation] --'65947D3F-41A5-453C-A450-C26BB4373CC2','menu_style_1',NULL
	@InstitutionSetupId UNIQUEIDENTIFIER,
	@ViewComponentName NVARCHAR(200),
    @Id UNIQUEIDENTIFIER

AS


BEGIN TRY

SET NOCOUNT ON;

	 IF @ViewComponentName = 'menu_style_1'
	 BEGIN

	 SELECT 
	        (SELECT LogoLink 
			 FROM ContentManagement.InstitutionSetup 
			 WHERE Id =@InstitutionSetupId AND RecordStatus =2
			 FOR XML PATH(''),TYPE, ELEMENTS),
			(SELECT  Id,
			         Title,
					 PlacementOrder,
					(SELECT Id,
							Title,
							MenuSetupId,
							SubMenuType,
							ParentSubMenuSetupId,
							RedirectLink,
							ThumbnailImageLink,
							PlacementOrder
					 FROM MenuManagement.SubMenuSetup 
					 WHERE MenuSetupId =  MenuManagement.MenuSetup.Id
					 AND RecordStatus =2
					 FOR XML PATH('SubMenuSetupDTO'),TYPE, ELEMENTS,ROOT('SubMenuSetups'))
			 FROM MenuManagement.MenuSetup 
			 WHERE InstitutionSetupId = @InstitutionSetupId
			 FOR XML PATH('MenuSetupDTO'),TYPE, ELEMENTS,ROOT('MenuList')
			 ),
			 (SELECT CityPrefix,
			         PhoneNumber 
			  FROM ContentManagement.InstitutionContactSetup 
			  WHERE InstitutionSetupId = @InstitutionSetupId
			  AND RecordStatus =2
			  order by PlacementOrder
			  FOR XML PATH('InstitutionContactSetupDTO'),TYPE, ELEMENTS,ROOT('PhoneNumberList'))
	FOR XML PATH('MenuViewComponentModel'),TYPE, ELEMENTS

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