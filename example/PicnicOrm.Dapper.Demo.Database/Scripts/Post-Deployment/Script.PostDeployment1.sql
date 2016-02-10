--/*
--Post-Deployment Script Template							
----------------------------------------------------------------------------------------
-- This file contains SQL statements that will be appended to the build script.		
-- Use SQLCMD syntax to include a file in the post-deployment script.			
-- Example:      :r .\myfile.sql								
-- Use SQLCMD syntax to reference a variable in the post-deployment script.		
-- Example:      :setvar TableName MyTable							
--               SELECT * FROM [$(TableName)]					
----------------------------------------------------------------------------------------
--*/

----Populate Address Table

--DECLARE @address AS TABLE
--(
--	[Id]			INT,
--	[Street]		NVARCHAR(50),
--	[City]			NVARCHAR(50),
--	[State]			NVARCHAR(50),
--	[PostalCode]	NVARCHAR(50)
--)

--INSERT INTO @address
--VALUES (1,			'10800 108th Place North',			'Maple Grove',					'MN',				'55369'),
--	   (2,			'2329 131st Avenue NorthWest',		'Coon Rapids',					'MN',				'55448'),
--	   (3,			'33 7th Street NorthEast #304',		'Osseo',						'MN',				'55369'),
--	   (4,			'11015 Illacs Court NE',			'Blaine',						'MN',				'55449'),
--	   (5,			'14701 Charlson Road',				'Eden Prairie',					'MN',				'55447'),
--	   (6,			'9380 Excelsior Blvd',				'Hopkins',						'MN',				'55343'),
--	   (7,			'2905 Northwest Blvd',				'Plymouth',						'MN',				'55443')

--MERGE dbo.[Address] AS tgt
--USING @address AS src
--ON tgt.Id = src.Id
--WHEN MATCHED THEN
--	UPDATE SET tgt.Street = src.Street,
--			   tgt.City = src.City,
--			   tgt.[State] = src.[State],
--			   tgt.PostalCode = src.[PostalCode]
--WHEN NOT MATCHED BY SOURCE THEN
--	DELETE
--WHEN NOT MATCHED BY TARGET THEN
--	INSERT VALUES (src.Id, src.Street, src.City, src.[State], src.PostalCode);


---------------------------------------------------------------------------------------------------------------------------------

----Populate Employer Table

--DECLARE @employer AS TABLE
--(
--	[Id]				INT,
--	[Name]				VARCHAR(50),
--	[EmployeeCount]		INT,
--	[Sector]			INT,
--	[AddressId]			INT
--)

--INSERT INTO @employer
--VALUES (1,				'C H Robinson Companies Inc',					15000,					1,				5),
--	   (2,				'Cargill',										130000,					2,				6),
--	   (3,				'American Retrieval',							50,						3,				7),
--	   (4,				'Allianz',										100000,					4,				1)

--MERGE dbo.[Employer] AS tgt
--USING @employer AS src
--ON tgt.Id = src.Id
--WHEN MATCHED THEN
--	UPDATE SET tgt.Name = src.Name,
--			   tgt.EmployeeCount = src.EmployeeCount,
--			   tgt.Sector = src.Sector,
--			   tgt.AddressId = src.AddressId
--WHEN NOT MATCHED BY SOURCE THEN
--	DELETE
--WHEN NOT MATCHED BY TARGET THEN
--	INSERT VALUES (src.Id,
--				   src.Name,
--				   src.EmployeeCount,
--				   src.Sector,
--				   src.AddressId);

----------------------------------------------------------------------------------------------------------------------------------

----Populate Car Table

--DECLARE @car AS TABLE
--(
--	[Id]				INT,
--	[Make]				NVARCHAR(20),
--	[Model]				NVARCHAR(20),
--	[Year]				INT
--)

--INSERT INTO @car
--VALUES (1,			'Chevrolet',			'Cavalier',				1987),
--	   (2,			'Ford',					'Ranger',				1994),
--	   (3,			'Chrysler',				'300',					2006),
--	   (4,			'Saturn',				'Ion',					2006),
--	   (5,			'Chevrolet',			'HHR',					2006),
--	   (6,			'Chevrolet',			'S10',					1990),
--	   (7,			'GMC',					'Terrain',				2014)

--MERGE dbo.[Car] AS tgt
--USING @car AS src
--ON tgt.Id = src.Id
--WHEN MATCHED THEN
--	UPDATE SET tgt.Make = src.Make,
--			   tgt.Model = src.Model,
--			   tgt.[Year] = src.[Year]
--WHEN NOT MATCHED BY SOURCE THEN
--	DELETE
--WHEN NOT MATCHED BY TARGET THEN
--	INSERT VALUES (src.Id, 
--				   src.Make,
--				   src.Model,
--				   src.[Year]);

--------------------------------------------------------------------------------------------------------------------------

----Populate User Table

--DECLARE @user AS TABLE
--(
--	[Id]					INT,
--	[Name]					NVARCHAR(50),
--	[BirthDate]				DATE,
--	[AddressId]				INT,
--	[EmployerId]			INT
--)

--INSERT INTO @user
--VALUES (1,				'Lee Olsen',				'1975-03-15',				2,				1),
--	   (2,				'Dinah Olsen',				'1981-07-29',				2,				3),
--	   (3,				'Mark Kuglin',				'1975-02-24',				3,				2),
--	   (4,				'Sue Olsen',				'1957-01-27',				4,				4),
--	   (5,				'Mary Olsen',				'1977-07-27',				1,				4)

--MERGE dbo.[User] AS tgt
--USING @user AS src
--ON tgt.Id = src.Id
--WHEN MATCHED THEN
--	UPDATE SET tgt.Name = src.Name,
--			   tgt.BirthDate = src.BirthDate,
--			   tgt.AddressId = src.AddressId,
--			   tgt.EmployerId = src.EmployerId
--WHEN NOT MATCHED BY SOURCE THEN
--	DELETE
--WHEN NOT MATCHED BY TARGET THEN
--	INSERT VALUES (src.Id, 
--				   src.Name,
--				   src.BirthDate,
--				   src.AddressId,
--				   src.EmployerId);


--------------------------------------------------------------------------------------------------------------------

----Populate UserCar Table

--DECLARE @usercar AS TABLE
--(
--	[UserId]	INT,
--	[CarId]		INT
--)

--INSERT INTO @usercar
--VALUES (1,		3),
--	   (1,		2),
--	   (1,		7),
--	   (2,		7),
--	   (2,		3),
--	   (3,		6),
--	   (3,		1),
--	   (4,		5),
--	   (4,		4),
--	   (5,		4)

--MERGE dbo.[UserCar] AS tgt
--USING @usercar AS src
--ON tgt.UserId = src.UserId AND
--   tgt.CarId = src.CarId
--WHEN NOT MATCHED BY SOURCE THEN
--	DELETE
--WHEN NOT MATCHED BY TARGET THEN
--	INSERT VALUES (src.UserId,
--				   src.CarId);