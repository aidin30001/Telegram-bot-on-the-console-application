using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Command.InlineQueryHandler;
/// <summary>
/// Контроллер для обработки входящих инлайн-запросов (Inline Queries).
/// Обеспечивает поиск и выполнение команд, когда пользователь вводит @имя_бота [запрос].
/// </summary>
public class InlineQueryController
{
  /// <summary>
  /// Реестр экземпляров команд инлайн-режима.
  /// Инициализируется один раз при старте приложения через рефлексию.
  /// </summary>
  static List<IInlineQueryCommand> commandList;
  /// <summary>
  /// Статический конструктор для автоматического поиска всех классов, 
  /// реализующих интерфейс <see cref="IInlineQueryCommand"/>.
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
  /// </summary>
  /// <param name="client">Клиент Telegram Bot API для отправки ответа.</param>
  /// <param name="update">Объект обновления, содержащий данные инлайн-запроса.</param>
  /// <returns>Задача, представляющая асинхронную операцию ответа пользователю.</returns>
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
