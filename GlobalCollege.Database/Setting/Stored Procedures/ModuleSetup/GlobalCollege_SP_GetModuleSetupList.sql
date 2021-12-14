CREATE PROCEDURE [Setting].[GlobalCollege_SP_GetModuleSetupList]
     @ModuleTypeSetupId UNIQUEIDENTIFIER = NULL
    ,@Name NVARCHAR(200) = NULL
    ,@ModuleCode NVARCHAR(20) = NULL
    ,@DatabaseTable NVARCHAR(200) = NULL
    ,@ApplicationClass NVARCHAR(200) = NULL
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


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @ModuleTypeSetupId IS NOT NULL THEN 'AND ModuleSetup.ModuleTypeSetupId = @ModuleTypeSetupId ' ELSE '' END
     + CASE WHEN @Name IS NOT NULL THEN 'AND ModuleSetup.Name  LIKE  @Name ' ELSE '' END
     + CASE WHEN @ModuleCode IS NOT NULL THEN 'AND ModuleSetup.ModuleCode  LIKE  @ModuleCode ' ELSE '' END
     + CASE WHEN @DatabaseTable IS NOT NULL THEN 'AND ModuleSetup.DatabaseTable  LIKE  @DatabaseTable ' ELSE '' END
     + CASE WHEN @ApplicationClass IS NOT NULL THEN 'AND ModuleSetup.ApplicationClass  LIKE  @ApplicationClass ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND ModuleSetup.RecordStatus = @RecordStatus ' ELSE ' AND ModuleSetup.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH ModuleSetup_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[ModuleTypeSetupId]
                               ,[Name]
                               ,[ModuleCode]
                               ,[Description]
                               ,[DatabaseTable]
                               ,[ApplicationClass]
                               ,[EntryType]
                               ,[IsParent]
                               ,[ParentModule]
                               ,[ChangeLogRequired]
                               ,[MakerCheckerRequired]
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

                         FROM[Setting].[ModuleSetup](NOLOCK) AS ModuleSetup '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM ModuleSetup_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   ModuleSetup.*
                        FROM ModuleSetup_CTE AS ModuleSetup
                        CROSS JOIN Count_CTE
                        
                        ORDER BY ModifiedDate DESC
                        
                        
                        OFFSET @PageSize * (@PageNumber - 1) ROWS
                        FETCH NEXT @PageSize ROWS ONLY 
	                     
	                     ';  


            IF @Debug = 1

            BEGIN
              PRINT @TSQL

            END
                   SET @ParameterList = N'@ModuleTypeSetupId UNIQUEIDENTIFIER = NULL
                   ,@Name NVARCHAR(200) = NULL
                   ,@ModuleCode NVARCHAR(20) = NULL
                   ,@DatabaseTable NVARCHAR(200) = NULL
                   ,@ApplicationClass NVARCHAR(200) = NULL

				   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @ModuleTypeSetupId,
                   @Name,
                   @ModuleCode,
                   @DatabaseTable,
                   @ApplicationClass,
       
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
                    'ModuleSetup',
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