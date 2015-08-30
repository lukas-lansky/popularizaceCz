CREATE TABLE [dbo].[Venue]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Name] NVARCHAR(200) NULL, 
    [Latitude] DECIMAL(9, 6) NULL,
	[Longitude] DECIMAL(9, 6) NULL
)
