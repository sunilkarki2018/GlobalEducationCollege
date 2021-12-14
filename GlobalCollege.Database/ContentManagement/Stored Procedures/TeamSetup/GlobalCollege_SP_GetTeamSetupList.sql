CREATE PROCEDURE [ContentManagement].[GlobalCollege_SP_GetTeamSetupList]
     @InstitutionSetupId UNIQUEIDENTIFIER = NULL
    ,@Name NVARCHAR(200) = NULL
    ,@Designation NVARCHAR(200) = NULL
    ,@EducationLevel NVARCHAR(200) = NULL
    ,@Address NVARCHAR(200) = NULL
    ,@Specialization NVARCHAR(200) = NULL
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


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @InstitutionSetupId IS NOT NULL THEN 'AND TeamSetup.InstitutionSetupId = @InstitutionSetupId ' ELSE '' END
     + CASE WHEN @Name IS NOT NULL THEN 'AND TeamSetup.Name  LIKE  @Name ' ELSE '' END
     + CASE WHEN @Designation IS NOT NULL THEN 'AND TeamSetup.Designation  LIKE  @Designation ' ELSE '' END
     + CASE WHEN @EducationLevel IS NOT NULL THEN 'AND TeamSetup.EducationLevel  LIKE  @EducationLevel ' ELSE '' END
     + CASE WHEN @Address IS NOT NULL THEN 'AND TeamSetup.Address  LIKE  @Address ' ELSE '' END
     + CASE WHEN @Specialization IS NOT NULL THEN 'AND TeamSetup.Specialization  LIKE  @Specialization ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND TeamSetup.RecordStatus = @RecordStatus ' ELSE ' AND TeamSetup.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH TeamSetup_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[InstitutionSetupId]
                               ,[Name]
                               ,[Designation]
                               ,[EducationLevel]
                               ,[Address]
                               ,[Specialization]
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

                         FROM[ContentManagement].[TeamSetup](NOLOCK) AS TeamSetup '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM TeamSetup_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   TeamSetup.*
                        FROM TeamSetup_CTE AS TeamSetup
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
                   ,@Name NVARCHAR(200) = NULL
                   ,@Designation NVARCHAR(200) = NULL
                   ,@EducationLevel NVARCHAR(200) = NULL
                   ,@Address NVARCHAR(200) = NULL
                   ,@Specialization NVARCHAR(200) = NULL

				   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @InstitutionSetupId,
                   @Name,
                   @Designation,
                   @EducationLevel,
                   @Address,
                   @Specialization,
       
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
                    'TeamSetup',
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