CREATE PROCEDURE [PageManagement].[GlobalCollege_SP_GetPageSetupList]
     @InstitutionSetupId UNIQUEIDENTIFIER = NULL
    ,@PageName NVARCHAR(200) = NULL
   ,@LayoutSetupId UNIQUEIDENTIFIER = NULL
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


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @InstitutionSetupId IS NOT NULL THEN 'AND PageSetup.InstitutionSetupId = @InstitutionSetupId ' ELSE '' END
     + CASE WHEN @PageName IS NOT NULL THEN 'AND PageSetup.PageName  LIKE  @PageName ' ELSE '' END
     + CASE WHEN @LayoutSetupId IS NOT NULL THEN 'AND PageSetup.LayoutSetupId = @LayoutSetupId ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND PageSetup.RecordStatus = @RecordStatus ' ELSE ' AND PageSetup.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH PageSetup_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[InstitutionSetupId]
                               ,[PageName]
                               ,[ControllerName]
                               ,[ActionName]
                               ,[AreaName]
                               ,[LayoutSetupId]
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

                         FROM[PageManagement].[PageSetup](NOLOCK) AS PageSetup '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM PageSetup_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   PageSetup.*
                        FROM PageSetup_CTE AS PageSetup
                        CROSS JOIN Count_CTE
                        
                        ORDER BY ControllerName, ModifiedDate DESC
                        
                        
                        OFFSET @PageSize * (@PageNumber - 1) ROWS
                        FETCH NEXT @PageSize ROWS ONLY 
	                     
	                     ';  


            IF @Debug = 1

            BEGIN
              PRINT @TSQL

            END
                   SET @ParameterList = N'@InstitutionSetupId UNIQUEIDENTIFIER = NULL
                   ,@PageName NVARCHAR(200) = NULL
                   ,@LayoutSetupId UNIQUEIDENTIFIER = NULL

				   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @InstitutionSetupId,
                   @PageName,
                   @LayoutSetupId,
       
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
                    'PageSetup',
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