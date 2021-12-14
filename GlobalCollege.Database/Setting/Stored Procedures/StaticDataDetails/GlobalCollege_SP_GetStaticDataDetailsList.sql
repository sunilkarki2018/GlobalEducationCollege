CREATE PROCEDURE [Setting].[GlobalCollege_SP_GetStaticDataDetailsList]
     @StaticDataMasterId UNIQUEIDENTIFIER = NULL
    ,@ColumnName NVARCHAR(300) = NULL
    ,@Title NVARCHAR(300) = NULL
    ,@Value NVARCHAR(300) = NULL
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


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @StaticDataMasterId IS NOT NULL THEN 'AND StaticDataDetails.StaticDataMasterId = @StaticDataMasterId ' ELSE '' END
     + CASE WHEN @ColumnName IS NOT NULL THEN 'AND StaticDataDetails.ColumnName  LIKE  @ColumnName ' ELSE '' END
     + CASE WHEN @Title IS NOT NULL THEN 'AND StaticDataDetails.Title  LIKE  @Title ' ELSE '' END
     + CASE WHEN @Value IS NOT NULL THEN 'AND StaticDataDetails.Value  LIKE  @Value ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND StaticDataDetails.RecordStatus = @RecordStatus ' ELSE ' AND StaticDataDetails.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH StaticDataDetails_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[StaticDataMasterId]
                               ,[ColumnName]
                               ,[Title]
                               ,[Value]
                               ,[OrderValue]
                               ,[Parameter1]
                               ,[Parameter2]
                               ,[Parameter3]
                               ,[Parameter4]
                               ,[Parameter5]
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

                         FROM[Setting].[StaticDataDetails](NOLOCK) AS StaticDataDetails '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM StaticDataDetails_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   StaticDataDetails.*
                        FROM StaticDataDetails_CTE AS StaticDataDetails
                        CROSS JOIN Count_CTE
                        
                        ORDER BY ModifiedDate DESC
                        
                        
                        OFFSET @PageSize * (@PageNumber - 1) ROWS
                        FETCH NEXT @PageSize ROWS ONLY 
	                     
	                     ';  


            IF @Debug = 1

            BEGIN
              PRINT @TSQL

            END
                   SET @ParameterList = N'@StaticDataMasterId UNIQUEIDENTIFIER = NULL
                   ,@ColumnName NVARCHAR(300) = NULL
                   ,@Title NVARCHAR(300) = NULL
                   ,@Value NVARCHAR(300) = NULL

				   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @StaticDataMasterId,
                   @ColumnName,
                   @Title,
                   @Value,
       
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
                    'StaticDataDetails',
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