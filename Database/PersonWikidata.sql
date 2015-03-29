CREATE TABLE [dbo].[PersonWikidata]
(
	[PersonId] INT NOT NULL PRIMARY KEY,
	[WikidataId] NVARCHAR(50) NOT NULL,
	[ImageUrl] NVARCHAR(200) NULL,
	[BirthDate] DATETIME NULL,
	CONSTRAINT [FK_PersonWikidata_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person]([Id]),
	CONSTRAINT [AK_WikidataId] UNIQUE ([WikidataId])
)
