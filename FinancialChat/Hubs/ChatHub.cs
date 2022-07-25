using FinancialChat.Data;
using Microsoft.AspNetCore.SignalR;

namespace FinancialChat.Hubs;
public class ChatHub : Hub
{
	public Task SendMessage(Message message)
	{
		return Clients.All.SendAsync("ReceiveMessage", message);
	}
}
