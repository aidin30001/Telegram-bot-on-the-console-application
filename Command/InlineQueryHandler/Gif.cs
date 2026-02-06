using System;
using Telegram.Bot.Types.InlineQueryResults;

namespace TelegramBot.Command.InlineQueryHandler;

public class Gif : IInlineQueryCommand
{
  public string Name => "gif";

  public Task<List<InlineQueryResult>> GetResults(string query)
  {
    var result = new List<InlineQueryResult>
    {
      new InlineQueryResultGif(
        "cat_programming_1",
        "https://media.giphy.com/media/JIX9t2j0ZTN9S/giphy.gif",
        "https://media.giphy.com/media/JIX9t2j0ZTN9S/200.gif"
      )
    };
    return Task.FromResult(result);
  }
}
