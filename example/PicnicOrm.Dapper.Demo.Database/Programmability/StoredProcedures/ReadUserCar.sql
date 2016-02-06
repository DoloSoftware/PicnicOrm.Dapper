CREATE PROCEDURE [dbo].[ReadUserCar]
	@userIds IdTable READONLY
AS
BEGIN
	
	DECLARE @carIds AS IdTable

	INSERT INTO @carIds
	SELECT DISTINCT uc.CarId
	FROM dbo.[UserCar] uc
		JOIN @userIds ui
			ON uc.UserId = ui.Id
			
	SELECT uc.UserId
		  ,uc.CarId
	FROM dbo.[UserCar] uc
		JOIN @userIds ui
			ON uc.UserId = ui.Id

	EXEC dbo.ReadCar @carIds

END