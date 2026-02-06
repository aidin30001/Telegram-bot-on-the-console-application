using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.UI;

public class StartKeyboard
{
  public static InlineKeyboardMarkup KeyboardMarkup = new(new[]
  {
    new[]
    {
      InlineKeyboardButton.WithCallbackData("Хорошый день", "Start_AnswerGoodDay"),
      InlineKeyboardButton.WithCallbackData("Плохой день", "Start_AnswerBadDay")
    },
    new[] { InlineKeyboardButton.WithUrl("YouTube", "https://youtube.com") },
    new[] { InlineKeyboardButton.WithWebApp("YouTube", new WebAppInfo{ Url = "https://youtube.com" })}
  });
}
