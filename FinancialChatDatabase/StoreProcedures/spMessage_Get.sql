CREATE PROCEDURE [dbo].[spMessage_Get]
	@Id int
AS
BEGIN
	SELECT Id, Timestamp, Username, Text 
	FROM dbo.[Message] 
	WHERE Id = @Id;
END