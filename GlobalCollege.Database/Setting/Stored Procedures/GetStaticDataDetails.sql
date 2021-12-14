CREATE PROC [Setting].[GetStaticDataDetails]
AS
SELECT [ColumnName]
      ,[Title][Text]
      ,[Value]
      ,[OrderValue]
      ,[Parameter1]
      ,[Parameter2]
      ,[Parameter3]
      ,[Parameter4]
      ,[Parameter5]
FROM Setting.StaticDataDetails AS [GlobalCollegeSelectListItem]
FOR XML AUTO, TYPE, ELEMENTS,ROOT('DropdownList')