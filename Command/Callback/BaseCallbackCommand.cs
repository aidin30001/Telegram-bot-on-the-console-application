using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Command.Callback;
/// <summary>
/// Базовый абстрактный класс для всех команд, обрабатывающих нажатия кнопок (Callback Queries).
/// Реализует паттерн инициализации без принудительного написания конструкторов в наследниках.
/// <para>
/// Base abstract class for all commands that handle button clicks (Callback Queries).
/// Implements an initialization pattern without forcing constructors in derived classes.
/// </para>
/// </summary>
public abstract class BaseCallbackCommand : ICallbackQueryCommand
{
  /// <summary>
  /// Уникальный идентификатор данных кнопки (callback_data).
  /// Должно быть реализовано в каждом конкретном классе команды.
  /// <para>
  /// Unique identifier of the button data (callback_data).
  /// Must be implemented in each specific command class.
  /// </para>
  /// </summary>
  public abstract string Name { get; }
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
  /// Объект текущего обновления (нажатие кнопки, данные пользователя и т.д.).
  /// Доступен только внутри классов-наследников.
  /// <para>
  /// Object of the current update (button click, user data, etc.).
  /// Accessible only inside derived classes.
  /// </para>
  /// </summary>
  protected Update Update { get; private set; } = null!;
  /// <summary>
  /// Метод "проброса" зависимостей. Вызывается контроллером сразу после создания экземпляра.
  /// Позволяет избежать написания конструкторов в каждом наследнике.
  /// <para>
  /// Dependency injection method. Called by the controller immediately after instance creation.
  /// Allows avoiding writing constructors in each derived class.
  /// </para>
  /// </summary>
  /// <param name="client">Активный клиент бота / Active bot client.</param>
  /// <param name="update">Данные текущего события / Current event data.</param>
  public void Initialize(ITelegramBotClient client, Update update)
  {
    Client = client;
    Update = update;
  }
  /// <summary>
  /// Основная логика обработки нажатия кнопки.
  /// <para>
  /// Main logic for handling button click.
  /// </para>
  /// </summary>
  /// <param name="callback">Объект запроса, содержащий данные о нажатой кнопке и сообщении / Request object containing button and message data.</param>
  /// <returns>Задача, представляющая асинхронную операцию / Task representing an asynchronous operation.</returns>
  public abstract Task Answer(CallbackQuery callback);
}
