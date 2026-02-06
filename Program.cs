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
  Console.WriteLine("Ошибка: Токен не найден в Secret Manager!");
  return;
}

Host bot = new Host(token);
bot.OnMessage += OnMessage;

bot.Start();


Console.WriteLine("Бот запущен. Нажмите клавишу enter для выхода...");
Console.ReadLine();

bot.End();



async void OnMessage(ITelegramBotClient client, Update update) => await UpdateRouter.RouteAsync(client, update);
