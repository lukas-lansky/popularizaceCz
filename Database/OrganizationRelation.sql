CREATE TABLE [dbo].[OrganizationRelation]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[SupOrganization] INT NOT NULL,
	[SubOrganization] INT NOT NULL,
	CONSTRAINT [FK_OrganizationRelation_Organization_Sup] FOREIGN KEY ([SupOrganization]) REFERENCES [Organization]([Id]),
	CONSTRAINT [FK_OrganizationRelation_Organization_Sub] FOREIGN KEY ([SubOrganization]) REFERENCES [Organization]([Id]),
	CONSTRAINT [AK_SupOrganization_SubOrganization] UNIQUE ([SupOrganization], [SubOrganization])
)
