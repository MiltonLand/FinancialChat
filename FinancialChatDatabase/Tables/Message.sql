CREATE TABLE [dbo].[Message]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ChatroomId] INT NULL, 
    [Timestamp] DATETIME NULL, 
    [Username] NVARCHAR(50) NULL, 
    [Text] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_Message_Chatroom] FOREIGN KEY ([ChatroomId]) REFERENCES [Chatroom]([Id])
)
