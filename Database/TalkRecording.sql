CREATE TABLE [dbo].[TalkRecording]
(
	[TalkId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Url] NVARCHAR(1024) NULL,
	[YouTubeVideoId] NVARCHAR(32) NULL,
	CONSTRAINT [FK_TalkRecording_Talk] FOREIGN KEY ([TalkId]) REFERENCES [Talk]([Id])
)
