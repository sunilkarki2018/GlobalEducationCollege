CREATE PROCEDURE [ContentManagement].[GlobalCollege_SP_GetFacultyContactList]
     @InstitutionSetupId UNIQUEIDENTIFIER = NULL
   ,@FacultySetupId UNIQUEIDENTIFIER = NULL
    ,@PhoneType NVARCHAR(200) = NULL
    ,@CommType NVARCHAR(200) = NULL
   ,@PhoneNumber INT = NULL
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


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @InstitutionSetupId IS NOT NULL THEN 'AND FacultyContact.InstitutionSetupId = @InstitutionSetupId ' ELSE '' END
     + CASE WHEN @FacultySetupId IS NOT NULL THEN 'AND FacultyContact.FacultySetupId = @FacultySetupId ' ELSE '' END
     + CASE WHEN @PhoneType IS NOT NULL THEN 'AND FacultyContact.PhoneType  LIKE  @PhoneType ' ELSE '' END
     + CASE WHEN @CommType IS NOT NULL THEN 'AND FacultyContact.CommType  LIKE  @CommType ' ELSE '' END
     + CASE WHEN @PhoneNumber IS NOT NULL THEN 'AND FacultyContact.PhoneNumber = @PhoneNumber ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND FacultyContact.RecordStatus = @RecordStatus ' ELSE ' AND FacultyContact.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH FacultyContact_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[InstitutionSetupId]
                               ,[FacultySetupId]
                               ,[PhoneType]
                               ,[CommType]
                               ,[PhoneNumber]
                               ,[Email]
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

                         FROM[ContentManagement].[FacultyContact](NOLOCK) AS FacultyContact '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM FacultyContact_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   FacultyContact.*
                        FROM FacultyContact_CTE AS FacultyContact
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
                   ,@FacultySetupId UNIQUEIDENTIFIER = NULL
                   ,@PhoneType NVARCHAR(200) = NULL
                   ,@CommType NVARCHAR(200) = NULL
                   ,@PhoneNumber INT = NULL

				   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @InstitutionSetupId,
                   @FacultySetupId,
                   @PhoneType,
                   @CommType,
                   @PhoneNumber,
       
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
                    'FacultyContact',
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