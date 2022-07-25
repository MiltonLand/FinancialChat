using DataAccess.Models;

namespace DataAccess.Data;
public interface IMessageData
{
    Task DeleteMessage(int id);
    Task<MessageModel?> GetMessage(int id);
    Task<IEnumerable<MessageModel>> GetMessages();
    Task InsertMessage(MessageModel message);
    Task UpdateMessage(MessageModel message);
}