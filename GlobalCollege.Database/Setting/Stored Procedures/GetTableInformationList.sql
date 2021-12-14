CREATE PROC [Setting].[GetTableInformationList]
AS
SELECT TableName,Parameter,ColumnName,TableDescription FROM(
SELECT TABLE_NAME TableName,
		CASE WHEN TABLE_NAME IN('CustomerRegistration','AccountRegistration','AccountRelatedEntity') 
		     THEN  TABLE_NAME+COLUMN_NAME  ELSE COLUMN_NAME END Parameter,
			 COLUMN_NAME ColumnName,
			 COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'ColumnID') AS COLUMN_ID,
			 (SELECT Name FROM Setting.ModuleSetup WHERE DatabaseTable = TABLE_NAME) TableDescription
 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA ='KYCManagement'
 ) AS TableInformation
WHERE TableInformation.Parameter IN('AccountRegistrationId','CustomerRegistrationId','IndividualCustomerRegistrationId','CorporateCustomerRegistrationId','AccountRelatedEntityId')
AND COLUMN_ID<>29
--AND NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA ='KYCManagement' AND TABLE_NAME='AccountOperationInstruction' AND COLUMN_NAME='IndividualCustomerRegistrationId')

 FOR XML AUTO, TYPE, ELEMENTS,ROOT('TableInformationList')