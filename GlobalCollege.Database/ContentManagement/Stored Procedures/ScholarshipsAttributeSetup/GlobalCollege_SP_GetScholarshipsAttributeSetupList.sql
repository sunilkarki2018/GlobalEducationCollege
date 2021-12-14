CREATE PROCEDURE [ContentManagement].[GlobalCollege_SP_GetScholarshipsAttributeSetupList]
     @InstitutionSetupId UNIQUEIDENTIFIER = NULL
   ,@ScholarSetupId UNIQUEIDENTIFIER = NULL
    ,@Title NVARCHAR(200) = NULL
    ,@ScholarAttributeType NVARCHAR(200) = NULL
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


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @InstitutionSetupId IS NOT NULL THEN 'AND ScholarshipsAttributeSetup.InstitutionSetupId = @InstitutionSetupId ' ELSE '' END
     + CASE WHEN @ScholarSetupId IS NOT NULL THEN 'AND ScholarshipsAttributeSetup.ScholarSetupId = @ScholarSetupId ' ELSE '' END
     + CASE WHEN @Title IS NOT NULL THEN 'AND ScholarshipsAttributeSetup.Title  LIKE  @Title ' ELSE '' END
     + CASE WHEN @ScholarAttributeType IS NOT NULL THEN 'AND ScholarshipsAttributeSetup.ScholarAttributeType  LIKE  @ScholarAttributeType ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND ScholarshipsAttributeSetup.RecordStatus = @RecordStatus ' ELSE ' AND ScholarshipsAttributeSetup.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH ScholarshipsAttributeSetup_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[InstitutionSetupId]
                               ,[Title]
                               ,[ScholarAttributeType]
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

                         FROM[ContentManagement].[ScholarshipsAttributeSetup](NOLOCK) AS ScholarshipsAttributeSetup '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM ScholarshipsAttributeSetup_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   ScholarshipsAttributeSetup.*
                        FROM ScholarshipsAttributeSetup_CTE AS ScholarshipsAttributeSetup
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
                   ,@Title NVARCHAR(200) = NULL
                   ,@ScholarAttributeType NVARCHAR(200) = NULL

				   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @InstitutionSetupId,
                   @ScholarSetupId,
                   @Title,
                   @ScholarAttributeType,
       
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
                    'ScholarshipsAttributeSetup',
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