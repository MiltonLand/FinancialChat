using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public class MessageData : IMessageData
{
    private readonly ISqlDataAccess _db;
    public MessageData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<MessageModel>> GetMessages()
    {
        return _db.LoadData<MessageModel, dynamic>("dbo.spMessage_GetAll", new { });
    }

    public async Task<MessageModel?> GetMessage(int id)
    {
        return (await _db.LoadData<MessageModel, dynamic>("dbo.spMessage_Get", new { Id = id })).FirstOrDefault();
    }
    public Task InsertMessage(MessageModel message)
    {
        return _db.SaveData("dbo.spMessage_Insert", new { message.TimeStamp, message.Username, message.Text });
    }

    public Task UpdateMessage(MessageModel message)
    {
        return _db.SaveData("dbo.spMessage_Update", message);
    }

    public Task DeleteMessage(int id)
    {
        return _db.SaveData("dbo.spMessage_Delete", new { Id = id });
    }
}
