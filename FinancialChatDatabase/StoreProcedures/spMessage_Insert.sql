CREATE PROCEDURE [dbo].[spMessage_Insert]
	@Timestamp datetime,
	@Username nvarchar(50),
	@Text nvarchar(MAX)
AS
BEGIN
	INSERT INTO dbo.[Message] (Timestamp, Username, Text)
	VALUES (@Timestamp, @Username, @Text)
END