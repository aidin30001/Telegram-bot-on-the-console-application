using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Command.Callback.AllAnswer;

public class Start_GoodDay : BaseCallbackCommand
{
  public override string Name => "Start_AnswerGoodDay";

  public override async Task Answer(CallbackQuery callback)
  {
    if (callback.Message == null) return;
    await Client.SendMessage(callback.Message.Chat.Id, "Хорошо что у тебя хороший день");
  }
}
