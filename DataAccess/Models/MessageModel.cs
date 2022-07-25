namespace DataAccess.Models;
public class MessageModel
{
    public int Id { get; set; }
    public int ChatroomId { get; set; }
    public DateTime TimeStamp { get; set; }
    public string? Username { get; set; }
    public string? Text { get; set; }
}
