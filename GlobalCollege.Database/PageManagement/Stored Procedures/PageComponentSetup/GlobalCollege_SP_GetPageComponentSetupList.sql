CREATE PROCEDURE [PageManagement].[GlobalCollege_SP_GetPageComponentSetupList]
     @InstitutionSetupId UNIQUEIDENTIFIER = NULL
   ,@PageSetupId UNIQUEIDENTIFIER = NULL
   ,@ComponentSetupId UNIQUEIDENTIFIER = NULL
    ,@ComponentType NVARCHAR(200) = NULL
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


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @InstitutionSetupId IS NOT NULL THEN 'AND PageComponentSetup.InstitutionSetupId = @InstitutionSetupId ' ELSE '' END
     + CASE WHEN @PageSetupId IS NOT NULL THEN 'AND PageComponentSetup.PageSetupId = @PageSetupId ' ELSE '' END
     + CASE WHEN @ComponentSetupId IS NOT NULL THEN 'AND PageComponentSetup.ComponentSetupId = @ComponentSetupId ' ELSE '' END
     + CASE WHEN @ComponentType IS NOT NULL THEN 'AND PageComponentSetup.ComponentType  LIKE  @ComponentType ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND PageComponentSetup.RecordStatus = @RecordStatus ' ELSE ' AND PageComponentSetup.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH PageComponentSetup_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[InstitutionSetupId]
                               ,[PageSetupId]
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

                         FROM[PageManagement].[PageComponentSetup](NOLOCK) AS PageComponentSetup '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM PageComponentSetup_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   PageComponentSetup.*
                        FROM PageComponentSetup_CTE AS PageComponentSetup
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
                   ,@PageSetupId UNIQUEIDENTIFIER = NULL
                   ,@ComponentSetupId UNIQUEIDENTIFIER = NULL
                   ,@ComponentType NVARCHAR(200) = NULL

				   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @InstitutionSetupId,
                   @PageSetupId,
                   @ComponentSetupId,
                   @ComponentType,
       
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
                    'PageComponentSetup',
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