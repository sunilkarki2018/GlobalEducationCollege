CREATE PROCEDURE [ViewComponent].[GlobalCollege_SP_EventsViewComponentInformation]
	@InstitutionSetupId UNIQUEIDENTIFIER,
	@ViewComponentName NVARCHAR(200),
	@Id UNIQUEIDENTIFIER
	

AS


BEGIN TRY

SET NOCOUNT ON;

	 IF @ViewComponentName = 'event_style_1'
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
				FOR XML PATH('EventsViewComponentModel'),TYPE, ELEMENTS 

	 END

	 IF @ViewComponentName = 'event_style_2'
	 BEGIN

			SELECT
			 (SELECT Id,
					   Title,
					   ThumbnailImageLink,					   
					   ShortDescription,
					   NewsDate
				FROM [ContentManagement].[NewsSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				ORDER BY CreatedDate DESC 
				OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
				FOR XML PATH('NewsSetupDTO'),TYPE, ELEMENTS,ROOT('NewsList')),
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
				FOR XML PATH('EventsViewComponentModel'),TYPE, ELEMENTS 


	 END

	 IF @ViewComponentName = 'event_style_3'
	 BEGIN

			SELECT
			 (SELECT Id,
					   Title,
					   ThumbnailImageLink,					   
					   ShortDescription,
					   NewsDate
				FROM [ContentManagement].[NewsSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				ORDER BY CreatedDate DESC 
				OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
				FOR XML PATH('NewsSetupDTO'),TYPE, ELEMENTS,ROOT('NewsList')),
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
				FOR XML PATH('EventsViewComponentModel'),TYPE, ELEMENTS 

	 END

	 IF @ViewComponentName = 'event_style_4'
	 BEGIN

			SELECT
			 (SELECT Id,
					   Title,
					   ThumbnailImageLink,					   
					   ShortDescription,
					   NewsDate
				FROM [ContentManagement].[NewsSetup]
				WHERE InstitutionSetupId = @InstitutionSetupId
				ORDER BY CreatedDate DESC 
				OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
				FOR XML PATH('NewsSetupDTO'),TYPE, ELEMENTS,ROOT('NewsList')),
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
				FOR XML PATH('EventsViewComponentModel'),TYPE, ELEMENTS 

	 END

	 IF @ViewComponentName = 'event_style_5'
	 BEGIN

			SELECT
			 (SELECT Id,
					   Title,
					   ThumbnailImageLink,					   
					   ShortDescription,
					   BlogDate,
					   Author,
					   ProgramName
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
				FOR XML PATH('EventsViewComponentModel'),TYPE, ELEMENTS 

	 END

	 IF @ViewComponentName = 'event_style_6'
	 BEGIN

			SELECT
			    (SELECT [Value] EventDescription 
				 FROM Setting.StaticDataDetails WHERE ColumnName ='Event6Description'
				 FOR XML PATH(''),TYPE, ELEMENTS),
						 (SELECT Id,
								 Title,
								 ShortDescription,
								 Venue,
								 EventDate	
						  FROM [ContentManagement].[EventSetup]
						  WHERE InstitutionSetupId = @InstitutionSetupId
						  ORDER BY CreatedDate DESC 
						  OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
						  FOR XML PATH('Event'),TYPE, ELEMENTS,ROOT('EventList'))
						  FOR XML PATH('EventsViewComponentModel'),TYPE, ELEMENTS 

	 END

	 IF @ViewComponentName = 'event_style_7'
	 BEGIN

			SELECT
			(SELECT Id,
				    Title,
				    ShortDescription,
				    Venue,
				    EventDate	
			FROM [ContentManagement].[EventSetup]
			WHERE InstitutionSetupId = @InstitutionSetupId
			ORDER BY CreatedDate DESC 
			OFFSET 0 ROW FETCH NEXT 2 ROWS ONLY
			FOR XML PATH('Event'),TYPE, ELEMENTS,ROOT('EventList'))
			FOR XML PATH('EventsViewComponentModel'),TYPE, ELEMENTS 

	 END ----
	 IF @ViewComponentName = 'events_style_details'
	 BEGIN

			SELECT   (SELECT [Id]
						    ,[InstitutionSetupId]
						    ,[Title]
						    ,[ThumbnailImageLink]
						    ,[BannerImageLink]
						    ,[EventDate]
						    ,[Time]
						    ,[Venue]
						    ,[PlacementOrder]
						    ,[ShortDescription]
						    ,[DetailDescription]
			              FROM ContentManagement.EventSetup 
			              WHERE  InstitutionSetupId = @InstitutionSetupId
						  AND Id = @Id
						  FOR XML PATH('Event'),TYPE, ELEMENTS
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
						  FOR XML PATH('AdmissionSetupDTO'),TYPE, ELEMENTS,ROOT('Admissions'))
						  FOR XML PATH('EventsViewComponentModel'),TYPE, ELEMENTS

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
