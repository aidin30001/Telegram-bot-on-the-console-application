using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Command.Callback;

namespace TelegramBot.UI;

public static class Test
{
  public static InlineKeyboardButton Button = new("Тестовая кнопка")
  {
    CallbackData = "Testing_Button"
  };

  public static List<InlineKeyboardButton> Buttons = new()
  {
    new ("Тестовая кнопка №1")
    {
      CallbackData = "Testing_Button"
    },
    new ("Тестовая кнопка №2")
    {
      CallbackData = "Testing_Button"
    }
  };

  public static InlineKeyboardButton UrlButton = new("YouTube")
  {
    Url = "https://youtube.com"
  };

  public static InlineKeyboardButton WebAppButton = new("YouTube web")
  {
    WebApp = new WebAppInfo { Url = "https://youtube.com" }
  };

  public static InlineKeyboardMarkup KeyboardMarkup = new(new[]
  {
    new[]
    {
      InlineKeyboardButton.WithCallbackData("Тестовая кнопка 1", "Testing_Button"),
      InlineKeyboardButton.WithCallbackData("Тестовая кнопка 2", "Testing_Button")
    },
    new[] { InlineKeyboardButton.WithUrl("YouTube", "https://youtube.com") },
    new[] { InlineKeyboardButton.WithWebApp("YouTube", new WebAppInfo{ Url = "https://youtube.com" })}
  });
}
