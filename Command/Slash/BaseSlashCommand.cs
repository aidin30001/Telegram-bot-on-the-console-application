using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Command.Slash;
/// <summary>
/// Базовый абстрактный класс для всех слэш-команд бота (например, /start, /help).
/// <para>
/// Base abstract class for all bot slash commands (e.g., /start, /help).
/// </para>
/// </summary>
public abstract class BaseSlashCommand : ISlashCommand
{
  /// <summary>
  /// Уникальное имя команды (например, "/start").
  /// Должно быть реализовано в каждом конкретном классе команды.
  /// <para>
  /// Unique command name (e.g., "/start").
  /// Must be implemented in each specific command class.
  /// </para>
  /// </summary>
  public abstract string Name { get; }
  /// <summary>
  /// Краткое описание функционала команды.
  /// Используется для формирования меню подсказок в Telegram.
  /// <para>
  /// Short description of the command functionality.
  /// Used to generate the suggestion menu in Telegram.
  /// </para>
  /// </summary>
  public abstract string Description { get; }
  /// <summary>
  /// Клиент для отправки запросов в Telegram Bot API.
  /// Доступен только внутри классов-наследников.
  /// <para>
  /// Client used to send requests to the Telegram Bot API.
  /// Accessible only inside derived classes.
  /// </para>
  /// </summary>
  protected ITelegramBotClient Client { get; private set; } = null!;
  /// <summary>
  /// Объект текущего обновления (сообщение, данные пользователя, чат и т.д.).
  /// Доступен только внутри классов-наследников.
  /// <para>
  /// Current update object (message, user data, chat, etc.).
  /// Accessible only inside derived classes.
  /// </para>
  /// </summary>
  protected Update Update { get; private set; } = null!;
  /// <summary>
  /// Метод "проброса" зависимостей. Вызывается контроллером сразу после создания экземпляра.
  /// Позволяет избежать написания 'base(client, update)' в каждом наследнике.
  /// <para>
  /// Dependency injection method. Called by the controller immediately after instance creation.
  /// Allows avoiding 'base(client, update)' calls in each derived class.
  /// </para>
  /// </summary>
  /// <param name="client">
  /// Активный клиент бота.
  /// <para>
  /// Active bot client.
  /// </para>
  /// </param>
  /// <param name="update">
  /// Данные текущего события.
  /// <para>
  /// Current event data.
  /// </para>
  /// </param>
  public void Initialize(ITelegramBotClient client, Update update)
  {
    Client = client;
    Update = update;
  }
  /// <summary>
  /// Основная логика выполнения команды.
  /// <para>
  /// Main logic for executing the command.
  /// </para>
  /// </summary>
  /// <param name="message">
  /// Объект сообщения, содержащий текст и данные отправителя.
  /// <para>
  /// Message object containing text and sender data.
  /// </para>
  /// </param>
  /// <param name="argument">
  /// Необязательный параметр, переданный после имени команды (например, "123" в "/set 123").
  /// <para>
  /// Optional parameter passed after the command name (e.g., "123" in "/set 123").
  /// </para>
  /// </param>
  /// <returns>
  /// Задача, представляющая асинхронную операцию.
  /// <para>
  /// Task representing an asynchronous operation.
  /// </para>
  /// </returns>
  public abstract Task Execute(Message message, string? argument = null);
}
