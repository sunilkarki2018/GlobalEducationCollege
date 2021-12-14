CREATE PROCEDURE [Administrator].[GlobalCollege_SP_GetApplicationUserList]
      @Email NVARCHAR(200) = NULL
     ,@PhoneNumber NVARCHAR(200) = NULL
     ,@UserName NVARCHAR(200) = NULL
     ,@FullName NVARCHAR(200) = NULL
     ,@UserRegistrationId UNIQUEIDENTIFIER = NULL
     ,@InstitutionSetupId UNIQUEIDENTIFIER = NULL
     ,@RecordStatus INT = NULL
     ,@PageNumber INT = 1
     ,@PageSize INT = 20
     ,@Debug INT = 1
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


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @Email IS NOT NULL THEN 'AND ApplicationUser.Email  LIKE  @Email ' ELSE '' END
     + CASE WHEN @PhoneNumber IS NOT NULL THEN 'AND ApplicationUser.PhoneNumber  LIKE  @PhoneNumber ' ELSE '' END
     + CASE WHEN @UserName IS NOT NULL THEN 'AND ApplicationUser.UserName  LIKE  @UserName ' ELSE '' END
     + CASE WHEN @FullName IS NOT NULL THEN 'AND ApplicationUser.FullName  LIKE  @FullName ' ELSE '' END
     + CASE WHEN @UserRegistrationId IS NOT NULL THEN 'AND ApplicationUser.UserRegistrationId = @UserRegistrationId ' ELSE '' END
     + CASE WHEN @InstitutionSetupId IS NOT NULL THEN 'AND ApplicationUser.InstitutionSetupId = @InstitutionSetupId ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND ApplicationUser.RecordStatus = @RecordStatus ' ELSE ' AND ApplicationUser.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH ApplicationUser_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[Email]
                               ,[PhoneNumber]
                               ,[UserName]
                               ,[FullName]
                               ,[UserRegistrationId]
                               ,[InstitutionSetupId]
							   ,(SELECT [CommericalName] FROM [ContentManagement].[InstitutionSetup] WHERE Id = InstitutionSetupId) Institution
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

                         FROM[Administrator].[ApplicationUser](NOLOCK) AS ApplicationUser '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM ApplicationUser_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   ApplicationUser.*
                        FROM ApplicationUser_CTE AS ApplicationUser
                        CROSS JOIN Count_CTE
                        
                        ORDER BY ModifiedDate DESC
                        
                        
                        OFFSET @PageSize * (@PageNumber - 1) ROWS
                        FETCH NEXT @PageSize ROWS ONLY 
	                     
	                     ';  


            IF @Debug = 1

            BEGIN
              PRINT @TSQL

            END
                   SET @ParameterList = N'@Email NVARCHAR(200) = NULL
                   ,@PhoneNumber NVARCHAR(200) = NULL
                   ,@UserName NVARCHAR(200) = NULL
                   ,@FullName NVARCHAR(200) = NULL
                   ,@UserRegistrationId UNIQUEIDENTIFIER = NULL
                   ,@InstitutionSetupId UNIQUEIDENTIFIER = NULL

				   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @Email,
                   @PhoneNumber,
                   @UserName,
                   @FullName,
                   @UserRegistrationId,
                   @InstitutionSetupId,
       
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
                    'ApplicationUser',
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