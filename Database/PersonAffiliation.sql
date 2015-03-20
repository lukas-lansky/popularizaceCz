CREATE TABLE [dbo].[PersonAffiliation]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[PersonId] INT NOT NULL,
	[OrganizationId] INT NOT NULL,
	[MainAffiliation] BIT NOT NULL DEFAULT (0),
	CONSTRAINT [FK_PersonAffiliation_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person]([Id]),
	CONSTRAINT [FK_PersonAffiliation_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id]),
	CONSTRAINT [AK_PersonId_OrganizationId] UNIQUE ([PersonId], [OrganizationId])
)
