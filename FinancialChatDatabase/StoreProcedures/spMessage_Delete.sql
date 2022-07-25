CREATE PROCEDURE [dbo].[spMessage_Delete]
	@Id int
AS
BEGIN
	DELETE 
	FROM dbo.[Message] 
	WHERE Id = @Id;
END