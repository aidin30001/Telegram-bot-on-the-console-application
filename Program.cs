using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot;
using TelegramBot.Bot;

IConfiguration config = new ConfigurationBuilder()
  .AddUserSecrets<Program>()
  .Build();

string? token = config["BotToken"];

if (string.IsNullOrEmpty(token))
{
  Console.WriteLine("Error: Token not found in Secret Manager!");
  return;
}

Host bot = new Host(token);
bot.OnMessage += OnMessage;

bot.Start();


Console.WriteLine("The bot is running. Press enter to exit...");
Console.ReadLine();

bot.End();



async void OnMessage(ITelegramBotClient client, Update update) => await UpdateRouter.RouteAsync(client, update);
