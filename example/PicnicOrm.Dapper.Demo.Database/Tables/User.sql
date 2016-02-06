CREATE TABLE [dbo].[User]
(
	[Id]					INT					NOT NULL				PRIMARY KEY,
	[Name]					NVARCHAR(50)		NOT NULL,
	[BirthDate]				DATE				NOT NULL,
	[AddressId]				INT					NOT NULL				FOREIGN KEY (AddressId) REFERENCES [Address](Id),
	[EmployerId]			INT					NOT NULL				FOREIGN KEY (EmployerId) REFERENCES [Employer](Id)
)