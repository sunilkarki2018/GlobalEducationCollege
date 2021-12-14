CREATE PROCEDURE [Setting].[GlobalCollege_SP_GetModuleBussinesLogicSetupList]
     @ModuleSetupId UNIQUEIDENTIFIER = NULL
    ,@Name NVARCHAR(300) = NULL
    ,@ColumnName NVARCHAR(200) = NULL
   ,@Required BIT = NULL
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


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @ModuleSetupId IS NOT NULL THEN 'AND ModuleBussinesLogicSetup.ModuleSetupId = @ModuleSetupId ' ELSE '' END
     + CASE WHEN @Name IS NOT NULL THEN 'AND ModuleBussinesLogicSetup.Name  LIKE  @Name ' ELSE '' END
     + CASE WHEN @ColumnName IS NOT NULL THEN 'AND ModuleBussinesLogicSetup.ColumnName  LIKE  @ColumnName ' ELSE '' END
     + CASE WHEN @Required IS NOT NULL THEN 'AND ModuleBussinesLogicSetup.Required = @Required ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND ModuleBussinesLogicSetup.RecordStatus = @RecordStatus ' ELSE ' AND ModuleBussinesLogicSetup.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH ModuleBussinesLogicSetup_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[Name]
                               ,[ColumnName]
                               ,[Description]
                               ,[DataType]
                               ,[StringLength]
                               ,[Required]
                               ,[Position]
                               ,[HtmlDataType]
                               ,[HtmlSize]
                               ,[DefaultValue]
                               ,[CanUpdate]
                               ,[IsParentColumn]
                               ,[SummaryHeader]
                               ,[ParameterForSummaryHeader]
                               ,[IsForeignKey]
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

                         FROM[Setting].[ModuleBussinesLogicSetup](NOLOCK) AS ModuleBussinesLogicSetup '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM ModuleBussinesLogicSetup_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   ModuleBussinesLogicSetup.*
                        FROM ModuleBussinesLogicSetup_CTE AS ModuleBussinesLogicSetup
                        CROSS JOIN Count_CTE
                        
                        ORDER BY ModifiedDate DESC
                        
                        
                        OFFSET @PageSize * (@PageNumber - 1) ROWS
                        FETCH NEXT @PageSize ROWS ONLY 
	                     
	                     ';  


            IF @Debug = 1

            BEGIN
              PRINT @TSQL

            END
                   SET @ParameterList = N'@ModuleSetupId UNIQUEIDENTIFIER = NULL
                   ,@Name NVARCHAR(300) = NULL
                   ,@ColumnName NVARCHAR(200) = NULL
                   ,@Required BIT = NULL

				   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @ModuleSetupId,
                   @Name,
                   @ColumnName,
                   @Required,
       
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
                    'ModuleBussinesLogicSetup',
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