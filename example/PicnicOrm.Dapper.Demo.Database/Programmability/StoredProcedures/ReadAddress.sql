CREATE PROCEDURE [dbo].[ReadAddress]
	@addressIds IdTable READONLY
AS
BEGIN

	SELECT a.Id
		  ,Street
		  ,City
		  ,[State]
		  ,PostalCode
	FROM dbo.[Address] a
		JOIN @addressIds i
			ON a.Id = i.Id

END
