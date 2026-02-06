using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Command.Callback;
using TelegramBot.Command.InlineQueryHandler;
using TelegramBot.Command.Slash;

namespace TelegramBot.Bot;

public static class UpdateRouter
{
  public static async Task RouteAsync(ITelegramBotClient client, Update update)
  {
    if (update.InlineQuery != null)
    {
      await InlineQueryController.Activate(client, update);
      return;
    }

    if (update.Message is not { Text: { } messageText } message)
    {
      await CallbackQueryController.HandleCommandAsync(client, update);
      return;
    }

    if (messageText.StartsWith("/"))
    {

      await SlashController.HandleCommandAsync(messageText, client, message, update);
      return;
    }

    //on message
    // await client.SendMessage(message.Chat.Id, messageText);
  }
}
