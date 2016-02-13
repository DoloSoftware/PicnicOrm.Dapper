CREATE TABLE [dbo].[Employer]
(
	[Id]				INT				NOT NULL			PRIMARY KEY,
	[Name]				VARCHAR(50)		NOT NULL,
	[EmployeeCount]		INT				NOT NULL,
	[Sector]			INT				NOT NULL,
	[AddressId]			INT				NOT NULL			FOREIGN KEY (AddressId) REFERENCES [Address](Id)
)
