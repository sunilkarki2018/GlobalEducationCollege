CREATE PROC [ViewComponent].[GlobalCollege_SP_DetailsViewComponentInformation] --'65947D3F-41A5-453C-A450-C26BB4373CC2','[ContentManagement].[BlogSetup]','9E8E9B16-E317-4163-8664-51C57DBE6D67'
		@InstitutionSetupId UNIQUEIDENTIFIER,		
		@TableName NVARCHAR(200),
		@Id NVARCHAR(50)
AS

BEGIN

DECLARE @DynamicSQL NVARCHAR(MAX), @XML XML


SET @DynamicSQL = 'SET @XML = (SELECT Id,ShortDescription,DetailDescription,BannerImageLink FROM '+@TableName+' WHERE Id = '+''''+@Id+'''
FOR XML PATH(''DetailModel''),TYPE, ELEMENTS)'

EXECUTE sp_executesql @DynamicSQL, N'@XML XML OUTPUT', @XML OUTPUT


SELECT		 @XML,
			 (SELECT Id,
					   Title,
					   ThumbnailImageLink,					   
					   ShortDescription,
					   BlogDate
				FROM [ContentManagement].[BlogSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				ORDER BY CreatedDate DESC 
				OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
				FOR XML PATH('BlogSetupDTO'),TYPE, ELEMENTS,ROOT('BlogList')),
			   (SELECT Id,
					   Title,
					   ThumbnailImageLink,
					   EventDate,
					   ShortDescription,
					   Time,
					   Venue					  
				FROM [ContentManagement].[EventSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				ORDER BY CreatedDate DESC 
				OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
				FOR XML PATH('EventSetupDTO'),TYPE, ELEMENTS,ROOT('EventList'))
				FOR XML PATH('WidgetsViewComponentModel'),TYPE, ELEMENTS 
END


