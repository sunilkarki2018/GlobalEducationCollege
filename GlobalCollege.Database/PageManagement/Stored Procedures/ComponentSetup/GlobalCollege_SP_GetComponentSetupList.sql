CREATE PROCEDURE [PageManagement].[GlobalCollege_SP_GetComponentSetupList]
     @InstitutionSetupId UNIQUEIDENTIFIER = NULL
    ,@ComponentTitle NVARCHAR(200) = NULL
    ,@ComponentCategory NVARCHAR(200) = NULL
    ,@ComponentName NVARCHAR(200) = NULL
    ,@ProcedureName NVARCHAR(200) = NULL
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


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @InstitutionSetupId IS NOT NULL THEN 'AND ComponentSetup.InstitutionSetupId = @InstitutionSetupId ' ELSE '' END
     + CASE WHEN @ComponentTitle IS NOT NULL THEN 'AND ComponentSetup.ComponentTitle  LIKE  @ComponentTitle ' ELSE '' END
     + CASE WHEN @ComponentCategory IS NOT NULL THEN 'AND ComponentSetup.ComponentCategory  LIKE  @ComponentCategory ' ELSE '' END
     + CASE WHEN @ComponentName IS NOT NULL THEN 'AND ComponentSetup.ComponentName  LIKE  @ComponentName ' ELSE '' END
     + CASE WHEN @ProcedureName IS NOT NULL THEN 'AND ComponentSetup.ProcedureName  LIKE  @ProcedureName ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND ComponentSetup.RecordStatus = @RecordStatus ' ELSE ' AND ComponentSetup.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH ComponentSetup_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[InstitutionSetupId]
                               ,[ComponentTitle]
                               ,[ComponentCategory]
                               ,[ComponentName]
                               ,[ProcedureName]
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

                         FROM[PageManagement].[ComponentSetup](NOLOCK) AS ComponentSetup '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM ComponentSetup_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   ComponentSetup.*
                        FROM ComponentSetup_CTE AS ComponentSetup
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
                   ,@ComponentTitle NVARCHAR(200) = NULL
                   ,@ComponentCategory NVARCHAR(200) = NULL
                   ,@ComponentName NVARCHAR(200) = NULL
                   ,@ProcedureName NVARCHAR(200) = NULL

				   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @InstitutionSetupId,
                   @ComponentTitle,
                   @ComponentCategory,
                   @ComponentName,
                   @ProcedureName,
       
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
                    'ComponentSetup',
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