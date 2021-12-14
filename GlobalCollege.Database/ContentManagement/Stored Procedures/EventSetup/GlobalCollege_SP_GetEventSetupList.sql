CREATE PROCEDURE [ContentManagement].[GlobalCollege_SP_GetEventSetupList]
     @InstitutionSetupId UNIQUEIDENTIFIER = NULL
    ,@EventType NVARCHAR(100) = NULL
    ,@Title NVARCHAR(200) = NULL
   ,@EventDate DATE = NULL
    ,@Time NVARCHAR(200) = NULL
    ,@Venue NVARCHAR(200) = NULL
    ,@RecordStatus INT = NULL
    ,@PageNumber INT
    ,@PageSize INT
    ,@Debug INT = 0
AS

BEGIN TRY

 SET NOCOUNT ON;

 DECLARE @ColumnList NVARCHAR(MAX),
         @TSQL NVARCHAR(MAX),
         @ParameterList NVARCHAR(MAX),
         @Where NVARCHAR(MAX) = '',
         @TotalRows DECIMAL(18, 2),
         @NextLine CHAR(2) = CHAR(13) + CHAR(10),
         @TCount INT


         SET @PageNumber = ISNULL(@PageNumber, 1)
         SET @PageSize = ISNULL(@PageSize, 20)   
         SET @TotalRows = 20 * 1.0; 


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @InstitutionSetupId IS NOT NULL THEN 'AND EventSetup.InstitutionSetupId = @InstitutionSetupId ' ELSE '' END
     + CASE WHEN @EventType IS NOT NULL THEN 'AND EventSetup.EventType  LIKE  @EventType ' ELSE '' END
     + CASE WHEN @Title IS NOT NULL THEN 'AND EventSetup.Title  LIKE  @Title ' ELSE '' END
     + CASE WHEN @EventDate IS NOT NULL THEN 'AND EventSetup.EventDate = @EventDate ' ELSE '' END
     + CASE WHEN @Time IS NOT NULL THEN 'AND EventSetup.Time  LIKE  @Time ' ELSE '' END
     + CASE WHEN @Venue IS NOT NULL THEN 'AND EventSetup.Venue  LIKE  @Venue ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND EventSetup.RecordStatus = @RecordStatus ' ELSE ' AND EventSetup.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH EventSetup_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[InstitutionSetupId]
                               ,[EventType]
                               ,[Title]
                               ,[EventDate]
                               ,[Time]
                               ,[Venue]
                               ,[PlacementOrder]
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

                         FROM[ContentManagement].[EventSetup](NOLOCK) AS EventSetup '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM EventSetup_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   EventSetup.*
                        FROM EventSetup_CTE AS EventSetup
                        CROSS JOIN Count_CTE
                        
                        ORDER BY ModifiedDate DESC
                        
                        
                        OFFSET @PageSize * (@PageNumber - 1) ROWS
                        FETCH NEXT @PageSize ROWS ONLY 
	                     
	                     ';  


            IF @Debug = 1

            BEGIN
              PRINT @TSQL

            END
                   SET @ParameterList = N'@InstitutionSetupId UNIQUEIDENTIFIER = NULL
                   ,@EventType NVARCHAR(100) = NULL
                   ,@Title NVARCHAR(200) = NULL
                   ,@EventDate DATE = NULL
                   ,@Time NVARCHAR(200) = NULL
                   ,@Venue NVARCHAR(200) = NULL

				   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @InstitutionSetupId,
                   @EventType,
                   @Title,
                   @EventDate,
                   @Time,
                   @Venue,
       
                   @RecordStatus,
                   @PageNumber,
                   @PageSize,
                   @TotalRows

            END TRY




        BEGIN CATCH
        
        
        
        
        
          INSERT INTO[dbo].[ExceptionLoggers]
                ([Id]
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
                   (NEWID(),
                    ERROR_MESSAGE(),
                    'EventSetup',
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