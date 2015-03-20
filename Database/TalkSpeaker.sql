CREATE TABLE [dbo].[TalkSpeaker]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[TalkId] INT NOT NULL,
	[PersonId] INT NOT NULL,
	CONSTRAINT [FK_TalkSpeaker_Talk] FOREIGN KEY ([TalkId]) REFERENCES [Talk]([Id]),
	CONSTRAINT [FK_TalkSpeaker_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person]([Id]),
	CONSTRAINT [AK_TalkId_PersonId] UNIQUE ([TalkId], [PersonId])
)
