using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Command.Callback;
/// <summary>
/// Базовый абстрактный класс для всех команд, обрабатывающих нажатия кнопок (Callback Queries).
/// Реализует паттерн инициализации без принудительного написания конструкторов в наследниках.
/// </summary>
public abstract class BaseCallbackCommand : ICallbackQueryCommand
{
  /// <summary>
  /// Уникальный идентификатор данных кнопки (callback_data). 
  /// Должно быть реализовано в каждом конкретном классе команды.
  /// </summary>
  public abstract string Name { get; }
  /// <summary>
  /// Клиент для отправки запросов в Telegram Bot API. 
  /// Доступен только внутри классов-наследников.
  /// </summary>
  protected ITelegramBotClient Client { get; private set; } = null!;
  /// <summary>
  /// Объект текущего обновления (нажатие кнопки, данные пользователя и т.д.). 
  /// Доступен только внутри классов-наследников.
  /// </summary>
  protected Update Update { get; private set; } = null!;
  /// <summary>
  /// Метод "проброса" зависимостей. Вызывается контроллером сразу после создания экземпляра.
  /// Позволяет избежать написания конструкторов в каждом наследнике.
  /// </summary>
  /// <param name="client">Активный клиент бота.</param>
  /// <param name="update">Данные текущего события.</param>
  public void Initialize(ITelegramBotClient client, Update update)
  {
    Client = client;
    Update = update;
  }
  /// <summary>
  /// Основная логика обработки нажатия кнопки.
  /// </summary>
  /// <param name="callback">Объект запроса, содержащий данные о нажатой кнопке и сообщении.</param>
  /// <returns>Задача, представляющая асинхронную операцию.</returns>
  public abstract Task Answer(CallbackQuery callback);
}
