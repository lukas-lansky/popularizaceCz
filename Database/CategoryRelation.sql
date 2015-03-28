CREATE TABLE [dbo].[CategoryRelation]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[SupCategory] INT NOT NULL,
	[SubCategory] INT NOT NULL,
	CONSTRAINT [FK_Category_Category_Sup] FOREIGN KEY ([SupCategory]) REFERENCES [Category]([Id]),
	CONSTRAINT [FK_Category_Category_Sub] FOREIGN KEY ([SubCategory]) REFERENCES [Category]([Id]),
	CONSTRAINT [AK_SupCategory_SubCategory] UNIQUE ([SupCategory], [SubCategory])
)
