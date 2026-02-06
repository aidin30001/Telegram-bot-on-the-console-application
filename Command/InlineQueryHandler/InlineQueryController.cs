using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Command.InlineQueryHandler;
/// <summary>
/// Контроллер для обработки входящих инлайн-запросов (Inline Queries).
/// Обеспечивает поиск и выполнение команд, когда пользователь вводит @имя_бота [запрос].
/// <para>
/// Controller for handling incoming Inline queries.
/// Provides command lookup and execution when a user types @botname [query].
/// </para>
/// </summary>
public class InlineQueryController
{
  /// <summary>
  /// Реестр экземпляров команд инлайн-режима.
  /// Инициализируется один раз при старте приложения через рефлексию.
  /// <para>
  /// Registry of inline command instances.
  /// Initialized once at application startup using reflection.
  /// </para>
  /// </summary>
  static List<IInlineQueryCommand> commandList;

  /// <summary>
  /// Статический конструктор для автоматического поиска всех классов,
  /// реализующих интерфейс <see cref="IInlineQueryCommand"/>.
  /// <para>
  /// Static constructor that automatically discovers all classes
  /// implementing the <see cref="IInlineQueryCommand"/> interface.
  /// </para>
  /// </summary>
  static InlineQueryController()
  {
    commandList = Assembly.GetExecutingAssembly()
      .GetTypes()
      .Where(t => typeof(IInlineQueryCommand).IsAssignableFrom(t)
        && !t.IsInterface
        && !t.IsAbstract)
      .Select(t => (IInlineQueryCommand)Activator.CreateInstance(t)!)
      .ToList();
  }
  /// <summary>
  /// Основная точка входа для обработки инлайн-запроса.
  /// <para>
  /// Main entry point for handling an Inline query.
  /// </para>
  /// </summary>
  /// <param name="client">
  /// Клиент Telegram Bot API для отправки ответа.
  /// <para>
  /// Telegram Bot API client used to send a response.
  /// </para>
  /// </param>
  /// <param name="update">
  /// Объект обновления, содержащий данные инлайн-запроса.
  /// <para>
  /// Update object containing Inline query data.
  /// </para>
  /// </param>
  /// <returns>
  /// Задача, представляющая асинхронную операцию ответа пользователю.
  /// <para>
  /// Task representing an asynchronous response operation.
  /// </para>
  /// </returns>
  public static async Task Activate(ITelegramBotClient client, Update update)
  {
    var query = update.InlineQuery;
    if (query == null) return;

    var text = query.Query.Trim().ToLower();
    var command = commandList.FirstOrDefault(
      c => c.Name?.Equals(text, StringComparison.OrdinalIgnoreCase) ?? false);
    if (command is null) return;

    var result = await command.GetResults(text);

    await client.AnswerInlineQuery(query.Id, result, 0);
  }
}
