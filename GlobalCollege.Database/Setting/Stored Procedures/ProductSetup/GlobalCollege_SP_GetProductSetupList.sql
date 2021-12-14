CREATE PROCEDURE [Setting].[GlobalCollege_SP_GetProductSetupList]
      @AccountType NVARCHAR(100) = NULL
    ,@CustomerType NVARCHAR(100) = NULL
    ,@Name NVARCHAR(300) = NULL
   ,@AccountOpeningFlowSetupId UNIQUEIDENTIFIER = NULL
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


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @AccountType IS NOT NULL THEN 'AND ProductSetup.AccountType  LIKE  @AccountType ' ELSE '' END
     + CASE WHEN @CustomerType IS NOT NULL THEN 'AND ProductSetup.CustomerType  LIKE  @CustomerType ' ELSE '' END
     + CASE WHEN @Name IS NOT NULL THEN 'AND ProductSetup.Name  LIKE  @Name ' ELSE '' END
     + CASE WHEN @AccountOpeningFlowSetupId IS NOT NULL THEN 'AND ProductSetup.AccountOpeningFlowSetupId = @AccountOpeningFlowSetupId ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND ProductSetup.RecordStatus = @RecordStatus ' ELSE ' AND ProductSetup.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH ProductSetup_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[AccountType]
                               ,[CustomerType]
                               ,[Name]
                               ,[ShortDescription]
                               ,[AccountOpeningFlowSetupId]
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

                         FROM[Setting].[ProductSetup](NOLOCK) AS ProductSetup '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM ProductSetup_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   ProductSetup.*
                        FROM ProductSetup_CTE AS ProductSetup
                        CROSS JOIN Count_CTE
                        
                        ORDER BY ModifiedDate DESC
                        
                        
                        OFFSET @PageSize * (@PageNumber - 1) ROWS
                        FETCH NEXT @PageSize ROWS ONLY 
	                     
	                     ';  


            IF @Debug = 1

            BEGIN
              PRINT @TSQL

            END
                   SET @ParameterList = N'@AccountType NVARCHAR(100) = NULL
                   ,@CustomerType NVARCHAR(100) = NULL
                   ,@Name NVARCHAR(300) = NULL
                   ,@AccountOpeningFlowSetupId UNIQUEIDENTIFIER = NULL

				   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @AccountType,
                   @CustomerType,
                   @Name,
                   @AccountOpeningFlowSetupId,
       
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
                    'ProductSetup',
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