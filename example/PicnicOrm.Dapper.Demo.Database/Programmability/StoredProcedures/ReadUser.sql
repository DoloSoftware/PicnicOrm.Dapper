CREATE PROCEDURE [dbo].[ReadUser]
	@BirthDate DATETIME = NULL
AS
BEGIN

	DECLARE @ids AS IdTable
	DECLARE @addressIds AS IdTable
	DECLARE @employerIds AS IdTable

	INSERT INTO @ids
	SELECT Id
	FROM dbo.[User]
	WHERE BirthDate >= COALESCE(@BirthDate, BirthDate)

	INSERT INTO @addressIds
	SELECT DISTINCT AddressId
	FROM dbo.[User]	
	WHERE BirthDate >= COALESCE(@BirthDate, BirthDate)

	INSERT INTO @employerIds
	SELECT DISTINCT EmployerId
	FROM dbo.[User]
	WHERE BirthDate >= COALESCE(@BirthDate, BirthDate)

	SELECT Id
		  ,Name
		  ,BirthDate
		  ,AddressId
		  ,EmployerId
	FROM dbo.[User]
	WHERE BirthDate >= COALESCE(@BirthDate, BirthDate)

	EXEC dbo.ReadAddress @addressIds
	EXEC dbo.ReadEmployer @employerIds
	EXEC dbo.ReadUserCar @ids

END