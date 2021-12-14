CREATE PROCEDURE [MenuManagement].[GlobalCollege_SP_GetSubMenuSetupList]
     @InstitutionSetupId UNIQUEIDENTIFIER = NULL
   ,@MenuSetupId UNIQUEIDENTIFIER = NULL
    ,@SubMenuType NVARCHAR(200) = NULL
   ,@ParentSubMenuSetupId UNIQUEIDENTIFIER = NULL
    ,@Title NVARCHAR(200) = NULL
    ,@RedirectLink NVARCHAR(200) = NULL
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


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @InstitutionSetupId IS NOT NULL THEN 'AND SubMenuSetup.InstitutionSetupId = @InstitutionSetupId ' ELSE '' END
     + CASE WHEN @MenuSetupId IS NOT NULL THEN 'AND SubMenuSetup.MenuSetupId = @MenuSetupId ' ELSE '' END
     + CASE WHEN @SubMenuType IS NOT NULL THEN 'AND SubMenuSetup.SubMenuType  LIKE  @SubMenuType ' ELSE '' END
     + CASE WHEN @ParentSubMenuSetupId IS NOT NULL THEN 'AND SubMenuSetup.ParentSubMenuSetupId = @ParentSubMenuSetupId ' ELSE '' END
     + CASE WHEN @Title IS NOT NULL THEN 'AND SubMenuSetup.Title  LIKE  @Title ' ELSE '' END
     + CASE WHEN @RedirectLink IS NOT NULL THEN 'AND SubMenuSetup.RedirectLink  LIKE  @RedirectLink ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND SubMenuSetup.RecordStatus = @RecordStatus ' ELSE ' AND SubMenuSetup.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH SubMenuSetup_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[InstitutionSetupId]
                               ,[SubMenuType]
                               ,[Title]
                               ,[RedirectLink]
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

                         FROM[MenuManagement].[SubMenuSetup](NOLOCK) AS SubMenuSetup '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM SubMenuSetup_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   SubMenuSetup.*
                        FROM SubMenuSetup_CTE AS SubMenuSetup
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
                   ,@MenuSetupId UNIQUEIDENTIFIER = NULL
                   ,@SubMenuType NVARCHAR(200) = NULL
                   ,@ParentSubMenuSetupId UNIQUEIDENTIFIER = NULL
                   ,@Title NVARCHAR(200) = NULL
                   ,@RedirectLink NVARCHAR(200) = NULL

				   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @InstitutionSetupId,
                   @MenuSetupId,
                   @SubMenuType,
                   @ParentSubMenuSetupId,
                   @Title,
                   @RedirectLink,
       
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
                    'SubMenuSetup',
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