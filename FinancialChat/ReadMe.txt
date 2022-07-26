1 - Build the database using the scripts in Financial Chat Database
2 - Change the "Default" connection string for the connection string of the database you created
3 - Compile the bot and put the dll in the BotDll folder
4 - In the Program.cs 

builder.Services.AddSingleton(x => new Bot("amqp://guest:guest@localhost:5672", "financial-chat-queue"));

replace the rabbitMQ url for your own, and the name of your queue.

5 - create an user and apply migrations when asked.