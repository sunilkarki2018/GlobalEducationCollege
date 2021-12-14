CREATE PROCEDURE [ContentManagement].[GlobalCollege_SP_GetContactForScholarshipList]
     @InstitutionSetupId UNIQUEIDENTIFIER = NULL
   ,@ScholarSetupId UNIQUEIDENTIFIER = NULL
    ,@PhoneType NVARCHAR(200) = NULL
    ,@PhoneNumber NVARCHAR(200) = NULL
    ,@Email NVARCHAR(200) = NULL
    ,@Remarks NVARCHAR(200) = NULL
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


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @InstitutionSetupId IS NOT NULL THEN 'AND ContactForScholarship.InstitutionSetupId = @InstitutionSetupId ' ELSE '' END
     + CASE WHEN @ScholarSetupId IS NOT NULL THEN 'AND ContactForScholarship.ScholarSetupId = @ScholarSetupId ' ELSE '' END
     + CASE WHEN @PhoneType IS NOT NULL THEN 'AND ContactForScholarship.PhoneType  LIKE  @PhoneType ' ELSE '' END
     + CASE WHEN @PhoneNumber IS NOT NULL THEN 'AND ContactForScholarship.PhoneNumber  LIKE  @PhoneNumber ' ELSE '' END
     + CASE WHEN @Email IS NOT NULL THEN 'AND ContactForScholarship.Email  LIKE  @Email ' ELSE '' END
     + CASE WHEN @Remarks IS NOT NULL THEN 'AND ContactForScholarship.Remarks  LIKE  @Remarks ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND ContactForScholarship.RecordStatus = @RecordStatus ' ELSE ' AND ContactForScholarship.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH ContactForScholarship_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[InstitutionSetupId]
                               ,[PhoneType]
                               ,[DefaultPhone]
                               ,[PhoneNumber]
                               ,[Extension]
                               ,[Email]
                               ,[Remarks]
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

                         FROM[ContentManagement].[ContactForScholarship](NOLOCK) AS ContactForScholarship '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM ContactForScholarship_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   ContactForScholarship.*
                        FROM ContactForScholarship_CTE AS ContactForScholarship
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
                   ,@ScholarSetupId UNIQUEIDENTIFIER = NULL
                   ,@PhoneType NVARCHAR(200) = NULL
                   ,@PhoneNumber NVARCHAR(200) = NULL
                   ,@Email NVARCHAR(200) = NULL
                   ,@Remarks NVARCHAR(200) = NULL

				   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @InstitutionSetupId,
                   @ScholarSetupId,
                   @PhoneType,
                   @PhoneNumber,
                   @Email,
                   @Remarks,
       
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
                    'ContactForScholarship',
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