CREATE PROCEDURE [dbo].[ReadEmployer]
	@employerIds IdTable READONLY
AS
BEGIN
	DECLARE @addressIds AS IdTable

	INSERT INTO @addressIds
	SELECT DISTINCT e.AddressId
	FROM dbo.[Employer] e
		JOIN @employerIds i
			ON e.Id = i.Id	

	SELECT e.Id
		  ,Name
		  ,EmployeeCount
		  ,Sector
		  ,AddressId
		  ,NULL AS [Employer]
	FROM dbo.[Employer] e
		JOIN @employerIds i
			ON e.Id = i.Id

	EXEC dbo.ReadAddress @addressIds

END
