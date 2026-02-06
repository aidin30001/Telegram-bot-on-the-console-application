using Telegram.Bot.Types.InlineQueryResults;

namespace TelegramBot.Command.InlineQueryHandler;
/// <summary>
/// Интерфейс для реализации команд, обрабатывающих Inline-запросы Telegram бота.
/// <para>
/// Interface for implementing commands that handle Telegram bot Inline queries.
/// </para>
/// </summary>
public interface IInlineQueryCommand
{
  /// <summary>
  /// Имя команды или идентификатор.
  /// <para>
  /// Command name or identifier.
  /// </para>
  /// </summary>
  string Name { get; }
  /// <summary>
  /// Получает результаты для Inline-запроса.
  /// <para>
  /// Retrieves results for an Inline query.
  /// </para>
  /// <para>
  /// Метод вызывается, когда пользователь вводит текст в поле поиска бота прямо в чате.
  /// </para>
  /// <para>
  /// This method is called when the user types text into the bot search field directly in the chat.
  /// </para>
  /// </summary>
  /// <param name="query">
  /// Строка запроса, введённая пользователем.
  /// <para>
  /// Query string entered by the user.
  /// </para>
  /// <para>
  /// Например, если пользователь набрал "@yourbot котики", то query будет "котики".
  /// </para>
  /// <para>
  /// For example, if the user typed "@yourbot cats", the query value will be "cats".
  /// </para>
  /// </param>
  /// <returns>
  /// Асинхронная задача, возвращающая список объектов <see cref="InlineQueryResult"/>,
  /// которые будут отображены пользователю в виде Inline-контента.
  /// <para>
  /// Asynchronous task returning a list of <see cref="InlineQueryResult"/> objects
  /// that will be displayed to the user as Inline content.
  /// </para>
  /// </returns>
  Task<List<InlineQueryResult>> GetResults(string query);
}
