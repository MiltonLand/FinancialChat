using System.Text;
using DataAccess.Models;
using FinancialChat.Hubs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FinancialChat.Data;

public class RabbitMQService : IRabbitMQService
{
	protected readonly ConnectionFactory _factory;
	protected readonly IConnection _connection;
	protected readonly IModel _channel;

	protected readonly IServiceProvider _serviceProvider;

	public RabbitMQService(IServiceProvider serviceProvider)
	{
		_factory = new ConnectionFactory() { Uri = new Uri("amqp://guest:guest@localhost:5672") };
		_connection = _factory.CreateConnection();
		_channel = _connection.CreateModel();

		_serviceProvider = serviceProvider;
	}

	public virtual void Connect()
	{
		_channel.QueueDeclare(queue: "financial-chat-queue", durable: true, exclusive: false, autoDelete: false);

		var consumer = new EventingBasicConsumer(_channel);

		consumer.Received += delegate (object? model, BasicDeliverEventArgs ea)
		{
			var chatHub = (IHubContext<ChatHub>?)_serviceProvider.GetService(typeof(IHubContext<ChatHub>));

			var body = ea.Body.ToArray();
			var message = Encoding.UTF8.GetString(body);

			Message messageModel = JsonConvert.DeserializeObject<Message>(message);

			chatHub?.Clients.All.SendAsync("ReceiveMessage", messageModel);
		};

		_channel.BasicConsume(queue: "financial-chat-queue", autoAck: true, consumer: consumer);
	}
}
