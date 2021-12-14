CREATE PROCEDURE [ContentManagement].[GlobalCollege_SP_GetInstitutionSetupList]
      @RegisteredName NVARCHAR(200) = NULL
    ,@InstitutionType NVARCHAR(200) = NULL
    ,@CommericalName NVARCHAR(200) = NULL
    ,@InstitutionSector NVARCHAR(200) = NULL
    ,@InstitutionSubsector NVARCHAR(200) = NULL
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


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @RegisteredName IS NOT NULL THEN 'AND InstitutionSetup.RegisteredName  LIKE  @RegisteredName ' ELSE '' END
     + CASE WHEN @InstitutionType IS NOT NULL THEN 'AND InstitutionSetup.InstitutionType  LIKE  @InstitutionType ' ELSE '' END
     + CASE WHEN @CommericalName IS NOT NULL THEN 'AND InstitutionSetup.CommericalName  LIKE  @CommericalName ' ELSE '' END
     + CASE WHEN @InstitutionSector IS NOT NULL THEN 'AND InstitutionSetup.InstitutionSector  LIKE  @InstitutionSector ' ELSE '' END
     + CASE WHEN @InstitutionSubsector IS NOT NULL THEN 'AND InstitutionSetup.InstitutionSubsector  LIKE  @InstitutionSubsector ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND InstitutionSetup.RecordStatus = @RecordStatus ' ELSE ' AND InstitutionSetup.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH InstitutionSetup_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[RegisteredName]
                               ,[InstitutionType]
                               ,[CommericalName]
                               ,[URL]
                               ,[IncorporationPerson]
                               ,[KnownSince]
                               ,[InstitutionSector]
                               ,[InstitutionSubsector]
                               ,[NatureofInstitution]
                               ,[PermitIssueDate]
                               ,[PermitValidThrough]
                               ,[IssuingAuthority]
                               ,[ForeignFollowers]
                               ,[CertifiedTeachers]
                               ,[StudentsEnrolled]
                               ,[CompleteCourses]
                               ,[TotalProgram]
                               ,[PassingtoUniversities]
                               ,[ParentsSatisfaction]
                               ,[LogoLink]
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

                         FROM[ContentManagement].[InstitutionSetup](NOLOCK) AS InstitutionSetup '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM InstitutionSetup_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   InstitutionSetup.*
                        FROM InstitutionSetup_CTE AS InstitutionSetup
                        CROSS JOIN Count_CTE
                        
                        ORDER BY ModifiedDate DESC
                        
                        
                        OFFSET @PageSize * (@PageNumber - 1) ROWS
                        FETCH NEXT @PageSize ROWS ONLY 
	                     
	                     ';  


            IF @Debug = 1

            BEGIN
              PRINT @TSQL

            END
                   SET @ParameterList = N'@RegisteredName NVARCHAR(200) = NULL
                   ,@InstitutionType NVARCHAR(200) = NULL
                   ,@CommericalName NVARCHAR(200) = NULL
                   ,@InstitutionSector NVARCHAR(200) = NULL
                   ,@InstitutionSubsector NVARCHAR(200) = NULL

				   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @RegisteredName,
                   @InstitutionType,
                   @CommericalName,
                   @InstitutionSector,
                   @InstitutionSubsector,
       
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
                    'InstitutionSetup',
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