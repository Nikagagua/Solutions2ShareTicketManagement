CREATE TABLE [dbo].[AccessToken]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[AccessToken] VARCHAR(MAX) NOT NULL,
	[CreatedAt] DATETIME NOT NULL
)
