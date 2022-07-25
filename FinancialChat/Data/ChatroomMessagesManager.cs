using DataAccess.Data;
using DataAccess.Models;

namespace FinancialChat.Data;

public class ChatroomMessagesManager
{
	private readonly IMessageData _messages;
	private const int MAX_MESSAGES = 50;
	public Queue<Message> Messages { get; private set; } = new Queue<Message>();

	public ChatroomMessagesManager(IMessageData messageData)
	{
		_messages = messageData;

		GetMessages();
	}

	public async void GetMessages()
	{
		List<MessageModel>? messages = (await _messages.GetMessages()).Reverse().Take(MAX_MESSAGES).Reverse().ToList();
		var messagesList = new Queue<Message>();

		messages.ForEach(m => messagesList.Enqueue(new Message
		{
			TimeStamp = m.TimeStamp,
			Username = m.Username,
			Text = m.Text
		}));

		Messages = messagesList;
	}

	internal void AddMessage(Message m)
	{
		Messages.Enqueue(m);
		
		if (Messages.Count > MAX_MESSAGES)
		{
			Messages.Dequeue();
		}
	}

	internal async Task SaveMessage(Message message)
	{
		var messageModel = new MessageModel
		{
			TimeStamp = message.TimeStamp,
			Username = message.Username,
			Text = message.Text
		};

		await _messages.InsertMessage(messageModel);
	}
}
