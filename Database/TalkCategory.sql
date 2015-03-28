CREATE TABLE [dbo].[TalkCategory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[TalkId] INT NOT NULL,
	[CategoryId] INT NOT NULL,
	CONSTRAINT [FK_TalkCategory_Talk] FOREIGN KEY ([TalkId]) REFERENCES [Talk]([Id]),
	CONSTRAINT [FK_TalkCategory_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Category]([Id]),
	CONSTRAINT [AK_TalkId_CategoryId] UNIQUE ([TalkId], [CategoryId])
)
