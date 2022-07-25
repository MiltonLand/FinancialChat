CREATE PROCEDURE [dbo].[spMessage_Update]
	@Id int,
	@Timestamp datetime,
	@Username nvarchar(50),
	@Text nvarchar(MAX)
AS
BEGIN
	UPDATE dbo.[Message] 
	SET Timestamp = @Timestamp, Username = @Username, Text = @Text
	WHERE Id = @Id;
END