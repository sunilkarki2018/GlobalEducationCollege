CREATE PROCEDURE [ViewComponent].[GlobalCollege_SP_NewsViewComponentInformation]  --'65947D3F-41A5-453C-A450-C26BB4373CC2','news_style_1',NULL
	@InstitutionSetupId UNIQUEIDENTIFIER,
	@ViewComponentName NVARCHAR(200),
	@Id UNIQUEIDENTIFIER
AS
	
BEGIN TRY

SET NOCOUNT ON;

	
	 IF @ViewComponentName = 'news_style_1'
	 BEGIN

			SELECT       (SELECT [Id]
								,[Title]
								,[ThumbnailImageLink]
								,[BannerImageLink]
								,[EventDate]
								,[Time]
								,[Venue]
								,[PlacementOrder]
								,[ShortDescription]
			              FROM ContentManagement.EventSetup 
			              WHERE  InstitutionSetupId = @InstitutionSetupId
						  ORDER BY CreatedDate DESC
						  OFFSET 0 ROW FETCH NEXT 1 ROWS ONLY	
						  FOR XML PATH('News'),TYPE, ELEMENTS
						  ),
						 (SELECT Id,
								 Title,
								 ThumbnailImageLink,
								 Author,
								 [ProgramName]
						  FROM [ContentManagement].[BlogSetup]
						  WHERE InstitutionSetupId = @InstitutionSetupId
						  ORDER BY CreatedDate DESC 
						  OFFSET 0 ROW FETCH NEXT 3 ROWS ONLY			
						  FOR XML PATH('BlogSetupDTO'),TYPE, ELEMENTS,ROOT('BlogList'))
						  FOR XML PATH('NewsViewComponentModel'),TYPE, ELEMENTS
			

	 END

	 IF @ViewComponentName = 'news_style_2'
	 BEGIN
	 	SELECT
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
				FOR XML PATH('NewsViewComponentModel'),TYPE, ELEMENTS 			

	 END

	 IF @ViewComponentName = 'news_style_3'
	 BEGIN

			SELECT (SELECT Id,
					       ThumbnailImageLink,
					       BlogDate,
					       Title
				    FROM [ContentManagement].[BlogSetup]
				    WHERE InstitutionSetupId = @InstitutionSetupId
				    AND RecordStatus=2
				    ORDER BY CreatedDate DESC 
				    OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
				    FOR XML PATH('BlogSetupDTO'),TYPE, ELEMENTS,ROOT('BlogList')),
				(SELECT Id,
					    ThumbnailImageLink,
					    NewsDate,
					    Title
				    FROM [ContentManagement].[NewsSetup]
				    WHERE InstitutionSetupId = @InstitutionSetupId
				    ORDER BY CreatedDate DESC 
				    OFFSET 0 ROW FETCH NEXT 3 ROWS ONLY
				    FOR XML PATH('NewsSetupDTO'),TYPE, ELEMENTS,ROOT('NewsList')),
				(SELECT Id,
						BannerImageLink
				FROM [ContentManagement].[BannerSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				AND RecordStatus=2
				AND BannerType='VideoList'
				ORDER BY CreatedDate DESC 
				OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
				FOR XML PATH('BannerSetupDTO'),TYPE, ELEMENTS,ROOT('BannerList'))
				FOR XML PATH('NewsViewComponentModel'),TYPE, ELEMENTS 		


	 END

	 IF @ViewComponentName = 'news_style_4'
	 BEGIN

			SELECT (
					SELECT   Id,
							 ThumbnailImageLink,
							 Title,
							 ShortDescription [ShortDesc]
					 FROM [ContentManagement].[NewsSetup]
					 WHERE InstitutionSetupId=@InstitutionSetupId
					 AND RecordStatus=2
					 ORDER BY CreatedDate DESC
					 OFFSET 0 ROW FETCH NEXT 4 ROWS ONLY
					 FOR XML PATH('NewsSetupDTO'),TYPE,ELEMENTS,ROOT('NewsList'))
					 FOR XML PATH('NewsViewComponentModel'),TYPE, ELEMENTS 		

	 END

      IF @ViewComponentName = 'news_style_5'
	 BEGIN

			SELECT (SELECT Id,
					         ThumbnailImageLink,
					         NewsDate,
					         Author,
						     ShortDescription,
					         Title,
							 Tags
				        FROM [ContentManagement].[NewsSetup]
				        WHERE InstitutionSetupId = @InstitutionSetupId
				        AND RecordStatus=2
				        ORDER BY CreatedDate DESC 
				        OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
				        FOR XML PATH('NewsSetupDTO'),TYPE, ELEMENTS,ROOT('NewsList')),
						(SELECT    Id,
					              EventDate,
					              Title,
					              [Time],
					    	      Venue
					    FROM [ContentManagement].[EventSetup]
				        WHERE InstitutionSetupId = @InstitutionSetupId
				        ORDER BY CreatedDate DESC 
				        OFFSET 0 ROW FETCH NEXT 3 ROWS ONLY
				        FOR XML PATH('EventSetupDTO'),TYPE, ELEMENTS,ROOT('EventsList'))
						FOR XML PATH('NewsViewComponentModel'),TYPE, ELEMENTS 		
				

	 END

      IF @ViewComponentName = 'news_style_6'
	 BEGIN

				SELECT (     SELECT Id,
					         BlogDate,
							 Title,
					         ShortDescription
				        FROM [ContentManagement].[BlogSetup]
				        WHERE InstitutionSetupId = @InstitutionSetupId
				        AND RecordStatus=2
				        ORDER BY CreatedDate DESC 
				        OFFSET 0 ROW FETCH NEXT 3 ROWS ONLY
				        FOR XML PATH('BlogSetupDTO'),TYPE, ELEMENTS,ROOT('BlogList')),
				   (    SELECT    Id,
								  ThumbnailImageLink,
								  Title,
								  ShortDescription,
					              NewsDate,
								  Tags
					    FROM [ContentManagement].[NewsSetup]
				        WHERE InstitutionSetupId = @InstitutionSetupId
				        ORDER BY CreatedDate DESC 
				        OFFSET 0 ROW FETCH NEXT 3 ROWS ONLY
				        FOR XML PATH('NewsSetupDTO'),TYPE, ELEMENTS,ROOT('NewsList'))
			FOR XML PATH('NewsViewComponentModel'),TYPE, ELEMENTS 	

	 END

      IF @ViewComponentName = 'news_style_7'
	 BEGIN

			SELECT
			   (SELECT Id,
					   ThumbnailImageLink,
					   BlogDate,
					   Title
				FROM [ContentManagement].[BlogSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				ORDER BY CreatedDate DESC 
				OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
				FOR XML PATH('BlogSetupDTO'),TYPE, ELEMENTS,ROOT('BlogList')),
				(SELECT Id,
					   ThumbnailImageLink,
					   StartDate,
					   Title
				FROM [ContentManagement].[AdmissionSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				ORDER BY CreatedDate DESC 
				OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
				FOR XML PATH('AdmissionSetupDTO'),TYPE, ELEMENTS,ROOT('AdmissionList')),
				(SELECT Id,
				        DATEPART(DAY,EventDate) [Day],
						DATENAME(MONTH,EventDate)[Month],
					   [Title],
					   Venue,
					   [Time]
				FROM [ContentManagement].[EventSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				ORDER BY CreatedDate DESC 
				OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
				FOR XML PATH('EventSetupDTO'),TYPE, ELEMENTS,ROOT('EventList'))
				FOR XML PATH('NewsViewComponentModel'),TYPE, ELEMENTS 	

	 END

      IF @ViewComponentName = 'news_style_8'
	 BEGIN

				SELECT
			          (SELECT     Id,
								  ThumbnailImageLink,
								  Title,
								  ShortDescription [ShortDesc],
					              NewsDate,
					    	      Tags
					    FROM [ContentManagement].[NewsSetup]
				        WHERE InstitutionSetupId = @InstitutionSetupId
						AND RecordStatus=2
				        ORDER BY CreatedDate DESC 
				        OFFSET 0 ROW FETCH NEXT 3 ROWS ONLY
				        FOR XML PATH('NewsSetupDTO'),TYPE, ELEMENTS,ROOT('NewsList')),

				     (SELECT      Id,
				                  ThumbnailImageLink,
					              Title,
								  EventDate
					              Venue
				        FROM [ContentManagement].[EventSetup]
				        WHERE InstitutionSetupId = @InstitutionSetupId
				        AND RecordStatus=2
						ORDER BY CreatedDate DESC 
				        OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
				        FOR XML PATH('EventSetupDTO'),TYPE, ELEMENTS,ROOT('EventList'))
					FOR XML PATH('NewsViewComponentModel'),TYPE, ELEMENTS 

	 END
	  IF @ViewComponentName = 'news_style_details'
	 BEGIN

			SELECT   (SELECT [Id]
						    ,[InstitutionSetupId]
						    ,[Title]
						    ,[ThumbnailImageLink]
						    ,[BannerImageLink]
						    ,[NewsDate]
						    ,[PlacementOrder]
						    ,[ShortDescription]
						    ,[DetailDescription]
			              FROM ContentManagement.NewsSetup 
			              WHERE  InstitutionSetupId = @InstitutionSetupId
						  AND Id = @Id
						  FOR XML PATH('NewsDetails'),TYPE, ELEMENTS
						  ),
						  (SELECT Id,
								 Title,
								 ThumbnailImageLink,
								 EventDate,
								 Venue,
								 PlacementOrder
						  FROM [ContentManagement].[EventSetup]
						  WHERE InstitutionSetupId = @InstitutionSetupId
						  AND EventDate> GETDATE()
						  ORDER BY CreatedDate DESC 
						  OFFSET 0 ROW FETCH NEXT 3 ROWS ONLY			
						  FOR XML PATH('EventSetupDTO'),TYPE, ELEMENTS,ROOT('EventList')),
						  (SELECT Id,
								 Title,
								 ThumbnailImageLink,
								  PlacementOrder
						  FROM [ContentManagement].[AdmissionSetup]
						  WHERE InstitutionSetupId = @InstitutionSetupId			
						  FOR XML PATH('AdmissionSetupDTO'),TYPE, ELEMENTS,ROOT('AdmissionList'))
						  FOR XML PATH('NewsViewComponentModel'),TYPE, ELEMENTS

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

