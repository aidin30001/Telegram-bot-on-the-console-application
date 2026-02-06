using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Command.Slash;
/// <summary>
/// Базовый абстрактный класс для всех слэш-команд бота (напр. /start, /help).
/// </summary>
public abstract class BaseSlashCommand : ISlashCommand
{
  /// <summary>
  /// Уникальное имя команды (например, "/start"). 
  /// Должно быть реализовано в каждом конкретном классе команды.
  /// </summary>
  public abstract string Name { get; }
  /// <summary>
  /// Краткое описание функционала команды. 
  /// Используется для формирования меню подсказок в Telegram.
  /// </summary>
  public abstract string Description { get; }
  /// <summary>
  /// Клиент для отправки запросов в Telegram Bot API. 
  /// Доступен только внутри классов-наследников.
  /// </summary>
  protected ITelegramBotClient Client { get; private set; } = null!;
  /// <summary>
  /// Объект текущего обновления (сообщение, данные пользователя, чат и т.д.). 
  /// Доступен только внутри классов-наследников.
  /// </summary>
  protected Update Update { get; private set; } = null!;
  /// <summary>
  /// Метод "проброса" зависимостей. Вызывается контроллером сразу после создания экземпляра.
  /// Позволяет избежать написания 'base(client, update)' в каждом наследнике.
  /// </summary>
  /// <param name="client">Активный клиент бота.</param>
  /// <param name="update">Данные текущего события.</param>
  public void Initialize(ITelegramBotClient client, Update update)
  {
    Client = client;
    Update = update;
  }
  /// <summary>
  /// Основная логика выполнения команды.
  /// </summary>
  /// <param name="message">Объект сообщения, содержащий текст и данные отправителя.</param>
  /// <param name="argument">Необязательный параметр, переданный после имени команды (например, "123" в "/set 123").</param>
  /// <returns>Задача, представляющая асинхронную операцию.</returns>
  public abstract Task Execute(Message message, string? argument = null);
}
