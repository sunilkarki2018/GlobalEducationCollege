CREATE PROCEDURE [PageManagement].[GlobalCollege_SP_GetLayoutComponentSetupList]
     @InstitutionSetupId UNIQUEIDENTIFIER = NULL
   ,@LayoutSetupId UNIQUEIDENTIFIER = NULL
   ,@ComponentSetupId UNIQUEIDENTIFIER = NULL
    ,@ComponentType NVARCHAR(200) = NULL
   ,@ComponentPlacementType INT = NULL
   ,@ComponentPresenceType INT = NULL
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


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @InstitutionSetupId IS NOT NULL THEN 'AND LayoutComponentSetup.InstitutionSetupId = @InstitutionSetupId ' ELSE '' END
     + CASE WHEN @LayoutSetupId IS NOT NULL THEN 'AND LayoutComponentSetup.LayoutSetupId = @LayoutSetupId ' ELSE '' END
     + CASE WHEN @ComponentSetupId IS NOT NULL THEN 'AND LayoutComponentSetup.ComponentSetupId = @ComponentSetupId ' ELSE '' END
     + CASE WHEN @ComponentType IS NOT NULL THEN 'AND LayoutComponentSetup.ComponentType  LIKE  @ComponentType ' ELSE '' END
     + CASE WHEN @ComponentPlacementType IS NOT NULL THEN 'AND LayoutComponentSetup.ComponentPlacementType = @ComponentPlacementType ' ELSE '' END
     + CASE WHEN @ComponentPresenceType IS NOT NULL THEN 'AND LayoutComponentSetup.ComponentPresenceType = @ComponentPresenceType ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND LayoutComponentSetup.RecordStatus = @RecordStatus ' ELSE ' AND LayoutComponentSetup.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH LayoutComponentSetup_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[InstitutionSetupId]
                               ,[LayoutSetupId]
							   ,(SELECT ComponentTitle FROM [PageManagement].[ComponentSetup] WHERE Id = ComponentSetupId) [Component Name]
                               ,[ComponentSetupId]
                               ,[ComponentType]
                               ,[ComponentPlacementType]
                               ,[ComponentPresenceType]
                               ,[CacheDuration]
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

                         FROM[PageManagement].[LayoutComponentSetup](NOLOCK) AS LayoutComponentSetup '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM LayoutComponentSetup_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   LayoutComponentSetup.*
                        FROM LayoutComponentSetup_CTE AS LayoutComponentSetup
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
                   ,@LayoutSetupId UNIQUEIDENTIFIER = NULL
                   ,@ComponentSetupId UNIQUEIDENTIFIER = NULL
                   ,@ComponentType NVARCHAR(200) = NULL
                   ,@ComponentPlacementType INT = NULL
                   ,@ComponentPresenceType INT = NULL

				   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @InstitutionSetupId,
                   @LayoutSetupId,
                   @ComponentSetupId,
                   @ComponentType,
                   @ComponentPlacementType,
                   @ComponentPresenceType,
       
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
                    'LayoutComponentSetup',
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