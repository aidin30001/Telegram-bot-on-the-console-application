using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.UI;

namespace TelegramBot.Command.Slash.AllCommands;

public class Start : BaseSlashCommand
{
  public override string Name => "/start";

  public override string Description => "Запустить бота";

  public override async Task Execute(Message message, string? argument = null) =>
    await Client.SendMessage(message.Chat.Id, "Привет! Это тестовый бот.\nКак прошел твой день?", replyMarkup: StartKeyboard.KeyboardMarkup);
}
