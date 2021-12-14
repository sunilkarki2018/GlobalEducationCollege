CREATE PROCEDURE [ContentManagement].[GlobalCollege_SP_GetResearchSetupList]
     @InstitutionSetupId UNIQUEIDENTIFIER = NULL
   ,@ResearchCategoryId UNIQUEIDENTIFIER = NULL
    ,@Title NVARCHAR(200) = NULL
    ,@Author NVARCHAR(200) = NULL
   ,@TeamSetupId UNIQUEIDENTIFIER = NULL
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


       SET @Where = +'WHERE 1=1 ' + CASE WHEN @InstitutionSetupId IS NOT NULL THEN 'AND ResearchSetup.InstitutionSetupId = @InstitutionSetupId ' ELSE '' END
     + CASE WHEN @ResearchCategoryId IS NOT NULL THEN 'AND ResearchSetup.ResearchCategoryId = @ResearchCategoryId ' ELSE '' END
     + CASE WHEN @Title IS NOT NULL THEN 'AND ResearchSetup.Title  LIKE  @Title ' ELSE '' END
     + CASE WHEN @Author IS NOT NULL THEN 'AND ResearchSetup.Author  LIKE  @Author ' ELSE '' END
     + CASE WHEN @TeamSetupId IS NOT NULL THEN 'AND ResearchSetup.TeamSetupId = @TeamSetupId ' ELSE '' END
     + CASE WHEN @RecordStatus IS NOT NULL THEN 'AND ResearchSetup.RecordStatus = @RecordStatus ' ELSE ' AND ResearchSetup.RecordStatus <> 1' END

    SELECT @TSQL = N';WITH ResearchSetup_CTE 
                     AS
                     (
                        SELECT  [Id]
                               ,[InstitutionSetupId]
                               ,[ResearchCategoryId]
                               ,[Title]
                               ,[Author]
                               ,[TeamSetupId]
                               ,[AuthorThumbnailImageLink]
                               ,[Designation]
                               ,[Duration]
                               ,[Website]
                               ,[DonwnloadLink]
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

                         FROM[ContentManagement].[ResearchSetup](NOLOCK) AS ResearchSetup '+@Where+')    , 
						 Count_CTE

                        AS
                        (
                            SELECT COUNT(*) AS TotalRows FROM ResearchSetup_CTE
                        )


                        SELECT CONVERT(INT, (TotalRows / @TotalRows)) [PageCount],                                                                                          
	                    				   CONVERT(INT, TotalRows)[TotalRecords],
	                    				   ResearchSetup.*
                        FROM ResearchSetup_CTE AS ResearchSetup
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
                   ,@ResearchCategoryId UNIQUEIDENTIFIER = NULL
                   ,@Title NVARCHAR(200) = NULL
                   ,@Author NVARCHAR(200) = NULL
                   ,@TeamSetupId UNIQUEIDENTIFIER = NULL
    			   ,@RecordStatus INT
                   ,@PageNumber INT
                   ,@PageSize INT
                   ,@TotalRows INT
                    '
                 EXECUTE sp_executesql @TSQL,
                   @ParameterList,
                   @InstitutionSetupId,
                   @ResearchCategoryId,
                   @Title,
                   @Author,
                   @TeamSetupId,
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
                    'ResearchSetup',
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