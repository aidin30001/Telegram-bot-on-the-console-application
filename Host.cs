using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Command.Slash;

namespace TelegramBot;

public class Host
{
  public Action<ITelegramBotClient, Update>? OnMessage;
  TelegramBotClient bot;
  CancellationTokenSource cts;
  ReceiverOptions receiver;

  public Host(string token)
  {
    bot = new TelegramBotClient(token);
    cts = new CancellationTokenSource();
    receiver = new ReceiverOptions()
    {
      AllowedUpdates = Array.Empty<UpdateType>()
    };
  }

  public void Start()
  {
    bot.StartReceiving(
      UpdateHandler,
      ErrorHandler,
      receiver,
      cts.Token);
    
    bot.SetMyCommands(SlashController.GetBotCommandsForMenu());
  }

  public void End() => cts.Cancel();

  private async Task ErrorHandler(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
  {
    Console.WriteLine($"Error: {exception.Message}");
    await Task.CompletedTask;
  }

  private async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken token)
  {
    if (OnMessage is null) return;
    
    OnMessage.Invoke(client, update);
    await Task.CompletedTask;
  }
}
