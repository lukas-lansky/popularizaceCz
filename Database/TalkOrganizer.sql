CREATE TABLE [dbo].[TalkOrganizer]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[TalkId] INT NOT NULL,
	[OrganizationId] INT NOT NULL, 
    CONSTRAINT [FK_TalkOrganizer_Talk] FOREIGN KEY ([TalkId]) REFERENCES [Talk]([Id]),
	CONSTRAINT [FK_TalkOrganizer_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id]),
	CONSTRAINT [AK_TalkId_OrganizationId] UNIQUE(TalkId, OrganizationId)
)
