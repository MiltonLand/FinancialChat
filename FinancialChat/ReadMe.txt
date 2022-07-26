/------------------------------------------------------------------------------------------------------------/
/ 1 - Build the database using the scripts in Financial Chat Database										 /
/------------------------------------------------------------------------------------------------------------/
/ 2 - In appsetting.json																					 /
/																											 /
/ "ConnectionStrings": {																					 /
/ 	"DefaultConnection": (AutomaticallyCreatedConnectionString),											 /
/ 	"Default": YourConnectionString																			 /
/ }																											 /
/ 																											 /
/ Change the "Default" connection string for the connection string of the database you created				 /
/------------------------------------------------------------------------------------------------------------/
/ 3 - In appsetting.json																					 /
/ 																											 /
/ "RabbitMQSettings": {																						 /
/ 	"Url": "amqp://guest:guest@localhost:5672",																 /
/ 	"Queue": "financial-chat-queue"																			 /
/ }																											 /
/ 																											 /
/ Replace the rabbitMQ url for your own, and the name of your queue.										 /
/------------------------------------------------------------------------------------------------------------/
/ 4 - Compile the bot and put the BotService.dll in the BotDll folder										 /
/------------------------------------------------------------------------------------------------------------/
/ 5 - Run the application																					 /
/------------------------------------------------------------------------------------------------------------/
/ 6 - Create an user and apply migrations when asked.														 /
/------------------------------------------------------------------------------------------------------------/