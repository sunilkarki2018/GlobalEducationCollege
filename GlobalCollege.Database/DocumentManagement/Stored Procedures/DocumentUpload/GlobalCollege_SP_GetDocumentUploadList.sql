CREATE PROCEDURE [DocumentManagement].[GlobalCollege_SP_GetDocumentUploadList]
     @InstitutionSetupId UNIQUEIDENTIFIER = NULL
    ,@DocumentCategoryId UNIQUEIDENTIFIER = NULL
    ,@DocumentSetupId UNIQUEIDENTIFIER = NULL
    ,@FileName NVARCHAR(300) = NULL
    ,@Tags NVARCHAR(200) = NULL
    ,@Extension NVARCHAR(20) = NULL
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


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @DocumentCategoryId IS NOT NULL THEN 'AND DocumentUpload.DocumentCategoryId = @DocumentCategoryId ' ELSE '' END
	 + CASE WHEN @InstitutionSetupId IS NOT NULL THEN 'AND DocumentUpload.InstitutionSetupId = @InstitutionSetupId ' ELSE '' END
     + CASE WHEN @DocumentSetupId IS NOT NULL THEN 'AND DocumentUpload.DocumentSetupId = @DocumentSetupId ' ELSE '' END
     + CASE WHEN @FileName IS NOT NULL THEN 'AND DocumentUpload.FileName  LIKE  @FileName ' ELSE '' END
     + CASE WHEN @Tags IS NOT NULL THEN 'AND DocumentUpload.Tags  LIKE  @Tags ' ELSE '' END
     + CASE WHEN @Extension IS NOT NULL THEN 'AND DocumentUpload.Extension  LIKE  @Extension ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND DocumentUpload.RecordStatus = @RecordStatus ' ELSE ' AND DocumentUpload.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH DocumentUpload_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[DocumentCategoryId]
							   ,(SELECT Name FROM [DocumentManagement].[DocumentCategory] WHERE Id = DocumentCategoryId) [Document Category]
                               ,[DocumentSetupId]
							   ,(SELECT DocumentName FROM [DocumentManagement].[DocumentSetup] WHERE Id = DocumentSetupId) [Document]
                               ,[FileName]
                               ,[Tags]
                               ,[Extension]
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

                         FROM[DocumentManagement].[DocumentUpload](NOLOCK) AS DocumentUpload '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM DocumentUpload_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   DocumentUpload.*
                        FROM DocumentUpload_CTE AS DocumentUpload
                        CROSS JOIN Count_CTE
                        
                        ORDER BY ModifiedDate DESC
                        
                        
                        OFFSET @PageSize * (@PageNumber - 1) ROWS
                        FETCH NEXT @PageSize ROWS ONLY 
	                     
	                     ';  


            IF @Debug = 1

            BEGIN
              PRINT @TSQL

            END
                   SET @ParameterList = N'@DocumentCategoryId UNIQUEIDENTIFIER = NULL
				   ,@InstitutionSetupId UNIQUEIDENTIFIER = NULL
                   ,@DocumentSetupId UNIQUEIDENTIFIER = NULL
                   ,@FileName NVARCHAR(300) = NULL
                   ,@Tags NVARCHAR(200) = NULL
                   ,@Extension NVARCHAR(20) = NULL

				   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @DocumentCategoryId,
				   @InstitutionSetupId,
                   @DocumentSetupId,
                   @FileName,
                   @Tags,
                   @Extension,
       
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
                    'DocumentUpload',
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