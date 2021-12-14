CREATE PROCEDURE [Administrator].[GlobalCollege_SP_GetApplicationRoleDetailsList]
     @Role UNIQUEIDENTIFIER = NULL
    ,@ModuleName NVARCHAR(200) = NULL
    ,@SubModuleName NVARCHAR(200) = NULL
   ,@CanView BIT = NULL
   ,@CanCreate BIT = NULL
   ,@CanEdit BIT = NULL
   ,@CanDelete BIT = NULL
   ,@CanAuthorize BIT = NULL
   ,@CanDiscard BIT = NULL
   ,@CanDownload BIT = NULL
   ,@CanAutoAuthorise BIT = NULL
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


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @Role IS NOT NULL THEN 'AND ApplicationRoleDetails.Role = @Role ' ELSE '' END
     + CASE WHEN @ModuleName IS NOT NULL THEN 'AND ApplicationRoleDetails.ModuleName  LIKE  @ModuleName ' ELSE '' END
     + CASE WHEN @SubModuleName IS NOT NULL THEN 'AND ApplicationRoleDetails.SubModuleName  LIKE  @SubModuleName ' ELSE '' END
     + CASE WHEN @CanView IS NOT NULL THEN 'AND ApplicationRoleDetails.CanView = @CanView ' ELSE '' END
     + CASE WHEN @CanCreate IS NOT NULL THEN 'AND ApplicationRoleDetails.CanCreate = @CanCreate ' ELSE '' END
     + CASE WHEN @CanEdit IS NOT NULL THEN 'AND ApplicationRoleDetails.CanEdit = @CanEdit ' ELSE '' END
     + CASE WHEN @CanDelete IS NOT NULL THEN 'AND ApplicationRoleDetails.CanDelete = @CanDelete ' ELSE '' END
     + CASE WHEN @CanAuthorize IS NOT NULL THEN 'AND ApplicationRoleDetails.CanAuthorize = @CanAuthorize ' ELSE '' END
     + CASE WHEN @CanDiscard IS NOT NULL THEN 'AND ApplicationRoleDetails.CanDiscard = @CanDiscard ' ELSE '' END
     + CASE WHEN @CanDownload IS NOT NULL THEN 'AND ApplicationRoleDetails.CanDownload = @CanDownload ' ELSE '' END
     + CASE WHEN @CanAutoAuthorise IS NOT NULL THEN 'AND ApplicationRoleDetails.CanAutoAuthorise = @CanAutoAuthorise ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND ApplicationRoleDetails.RecordStatus = @RecordStatus ' ELSE ' AND ApplicationRoleDetails.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH ApplicationRoleDetails_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[Role]
                               ,[ModuleName]
                               ,[SubModuleName]
                               ,[CanView]
                               ,[CanCreate]
                               ,[CanEdit]
                               ,[CanDelete]
                               ,[CanAuthorize]
                               ,[CanDiscard]
                               ,[CanDownload]
                               ,[CanAutoAuthorise]
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

                         FROM[Administrator].[ApplicationRoleDetails](NOLOCK) AS ApplicationRoleDetails '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM ApplicationRoleDetails_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   ApplicationRoleDetails.*
                        FROM ApplicationRoleDetails_CTE AS ApplicationRoleDetails
                        CROSS JOIN Count_CTE
                        
                        ORDER BY ModifiedDate DESC
                        
                        
                        OFFSET @PageSize * (@PageNumber - 1) ROWS
                        FETCH NEXT @PageSize ROWS ONLY 
	                     
	                     ';  


            IF @Debug = 1

            BEGIN
              PRINT @TSQL

            END
                   SET @ParameterList = N'@Role UNIQUEIDENTIFIER = NULL
                   ,@ModuleName NVARCHAR(200) = NULL
                   ,@SubModuleName NVARCHAR(200) = NULL
                   ,@CanView BIT = NULL
                   ,@CanCreate BIT = NULL
                   ,@CanEdit BIT = NULL
                   ,@CanDelete BIT = NULL
                   ,@CanAuthorize BIT = NULL
                   ,@CanDiscard BIT = NULL
                   ,@CanDownload BIT = NULL
                   ,@CanAutoAuthorise BIT = NULL

				   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @Role,
                   @ModuleName,
                   @SubModuleName,
                   @CanView,
                   @CanCreate,
                   @CanEdit,
                   @CanDelete,
                   @CanAuthorize,
                   @CanDiscard,
                   @CanDownload,
                   @CanAutoAuthorise,
       
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
                    'ApplicationRoleDetails',
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