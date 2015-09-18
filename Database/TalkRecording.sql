CREATE TABLE [dbo].[TalkRecording]
(
	[TalkRecordingId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[TalkId] INT NOT NULL,
	[Url] NVARCHAR(1024) NULL,
	[YouTubeVideoId] NVARCHAR(32) NULL,
	[Description] NVARCHAR(1024) NULL,
	CONSTRAINT [FK_TalkRecording_Talk] FOREIGN KEY ([TalkId]) REFERENCES [Talk]([Id])
)
