CREATE TABLE [dbo].[UserCar]
(
	[UserId]			INT			FOREIGN KEY (UserId) REFERENCES [User](Id),
	[CarId]				INT			FOREIGN	KEY	(CarId)	REFERENCES [Car](Id),
	CONSTRAINT PK_UserId_CarId PRIMARY KEY (UserId, CarId)
)
