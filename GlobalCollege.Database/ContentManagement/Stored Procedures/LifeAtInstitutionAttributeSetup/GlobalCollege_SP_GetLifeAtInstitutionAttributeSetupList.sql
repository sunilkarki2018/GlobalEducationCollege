CREATE PROCEDURE [ContentManagement].[GlobalCollege_SP_GetLifeAtInstitutionAttributeSetupList]
     @InstitutionSetupId UNIQUEIDENTIFIER = NULL
   ,@LifeAtInstitutionSetupId UNIQUEIDENTIFIER = NULL
    ,@Title NVARCHAR(200) = NULL
    ,@LifeAtInstitutionAttributeType NVARCHAR(200) = NULL
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


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @InstitutionSetupId IS NOT NULL THEN 'AND LifeAtInstitutionAttributeSetup.InstitutionSetupId = @InstitutionSetupId ' ELSE '' END
     + CASE WHEN @LifeAtInstitutionSetupId IS NOT NULL THEN 'AND LifeAtInstitutionAttributeSetup.LifeAtInstitutionSetupId = @LifeAtInstitutionSetupId ' ELSE '' END
     + CASE WHEN @Title IS NOT NULL THEN 'AND LifeAtInstitutionAttributeSetup.Title  LIKE  @Title ' ELSE '' END
     + CASE WHEN @LifeAtInstitutionAttributeType IS NOT NULL THEN 'AND LifeAtInstitutionAttributeSetup.LifeAtInstitutionAttributeType  LIKE  @LifeAtInstitutionAttributeType ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND LifeAtInstitutionAttributeSetup.RecordStatus = @RecordStatus ' ELSE ' AND LifeAtInstitutionAttributeSetup.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH LifeAtInstitutionAttributeSetup_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[InstitutionSetupId]
                               ,[Title]
                               ,[LifeAtInstitutionAttributeType]
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

                         FROM[ContentManagement].[LifeAtInstitutionAttributeSetup](NOLOCK) AS LifeAtInstitutionAttributeSetup '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM LifeAtInstitutionAttributeSetup_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   LifeAtInstitutionAttributeSetup.*
                        FROM LifeAtInstitutionAttributeSetup_CTE AS LifeAtInstitutionAttributeSetup
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
                   ,@LifeAtInstitutionSetupId UNIQUEIDENTIFIER = NULL
                   ,@Title NVARCHAR(200) = NULL
                   ,@LifeAtInstitutionAttributeType NVARCHAR(200) = NULL

				   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @InstitutionSetupId,
                   @LifeAtInstitutionSetupId,
                   @Title,
                   @LifeAtInstitutionAttributeType,
       
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
                    'LifeAtInstitutionAttributeSetup',
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