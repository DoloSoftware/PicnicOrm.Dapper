CREATE PROCEDURE [dbo].[ReadCar]
	@carIds IdTable READONLY
AS
BEGIN

	SELECT c.Id
		  ,Make
		  ,Model
		  ,[Year]
	FROM dbo.[Car] c
		JOIN @carIds i
			ON c.Id = i.Id

END