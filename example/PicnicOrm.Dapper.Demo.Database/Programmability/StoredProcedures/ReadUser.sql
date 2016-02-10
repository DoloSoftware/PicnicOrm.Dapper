CREATE PROCEDURE [dbo].[ReadUser]

AS
	DECLARE @ids AS IdTable
	DECLARE @addressIds AS IdTable
	DECLARE @employerIds AS IdTable

	INSERT INTO @ids
	SELECT Id
	FROM dbo.[User]

	INSERT INTO @addressIds
	SELECT DISTINCT AddressId
	FROM dbo.[User]

	INSERT INTO @employerIds
	SELECT DISTINCT EmployerId
	FROM dbo.[User]

	SELECT Id
		  ,Name
		  ,BirthDate
		  ,AddressId
		  ,EmployerId
	FROM dbo.[User]

	EXEC dbo.ReadAddress @addressIds
	EXEC dbo.ReadEmployer @employerIds
	EXEC dbo.ReadUserCar @ids

RETURN 0
