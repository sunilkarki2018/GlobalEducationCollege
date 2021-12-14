CREATE PROCEDURE [Administrator].[GlobalCollege_SP_GetApplicationUserClaimList]
     @Id UNIQUEIDENTIFIER = NULL
   ,@UserId UNIQUEIDENTIFIER = NULL
    ,@ClaimType NVARCHAR(200) = NULL
    ,@ClaimValue NVARCHAR(200) = NULL
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


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @Id IS NOT NULL THEN 'AND ApplicationUserClaim.Id = @Id ' ELSE '' END
     + CASE WHEN @UserId IS NOT NULL THEN 'AND ApplicationUserClaim.UserId = @UserId ' ELSE '' END
     + CASE WHEN @ClaimType IS NOT NULL THEN 'AND ApplicationUserClaim.ClaimType  LIKE  @ClaimType ' ELSE '' END
     + CASE WHEN @ClaimValue IS NOT NULL THEN 'AND ApplicationUserClaim.ClaimValue  LIKE  @ClaimValue ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND ApplicationUserClaim.RecordStatus = @RecordStatus ' ELSE ' AND ApplicationUserClaim.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH ApplicationUserClaim_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[Id]
                               ,[UserId]
                               ,[ClaimType]
                               ,[ClaimValue]
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

                         FROM[Administrator].[ApplicationUserClaim](NOLOCK) AS ApplicationUserClaim '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM ApplicationUserClaim_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   ApplicationUserClaim.*
                        FROM ApplicationUserClaim_CTE AS ApplicationUserClaim
                        CROSS JOIN Count_CTE
                        
                        ORDER BY ModifiedDate DESC
                        
                        
                        OFFSET @PageSize * (@PageNumber - 1) ROWS
                        FETCH NEXT @PageSize ROWS ONLY 
	                     
	                     ';  


            IF @Debug = 1

            BEGIN
              PRINT @TSQL

            END
                   SET @ParameterList = N'@Id UNIQUEIDENTIFIER = NULL
                   ,@UserId UNIQUEIDENTIFIER = NULL
                   ,@ClaimType NVARCHAR(200) = NULL
                   ,@ClaimValue NVARCHAR(200) = NULL

				   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @Id,
                   @UserId,
                   @ClaimType,
                   @ClaimValue,
       
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
                    'ApplicationUserClaim',
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