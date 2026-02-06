using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Command.Callback.AllAnswer;

public class Start_BadDay : BaseCallbackCommand
{
  public override string Name => "Start_AnswerBadDay";

  public override async Task Answer(CallbackQuery callback)
  {
    if (callback.Message == null) return;
    await Client.SendMessage(callback.Message.Chat.Id, "Сочувствую что у тебя плохой день");
  }
}
