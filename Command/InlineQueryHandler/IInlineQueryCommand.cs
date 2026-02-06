using Telegram.Bot.Types.InlineQueryResults;

namespace TelegramBot.Command.InlineQueryHandler;
/// <summary>
/// Интерфейс для реализации команд, обрабатывающих Inline-запросы Telegram бота.
/// </summary>
public interface IInlineQueryCommand
{
  /// <summary>
  /// Имя команды или идентификатор.
  /// </summary>
  string Name { get; }
  /// <summary>
  /// Получает результаты для Inline-запроса.
  /// <para>Метод вызывается, когда пользователь вводит текст в поле поиска бота прямо в чате.</para>
  /// </summary>
  /// <param name="query">Строка запроса, введённая пользователем.
  /// <para>Например, если пользователь набрал "@yourbot котики", то query будет "котики".</para>
  /// </param>
  /// <returns>Асинхронная задача, возвращающая список объектов <see cref="InlineQueryResult"/>, 
  /// которые будут отображены пользователю в виде Inline-контента.</returns>
  Task<List<InlineQueryResult>> GetResults(string query);
}
