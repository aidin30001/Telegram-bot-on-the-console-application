using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.Command.Callback;
/// <summary>
/// Контроллер управления Callback-запросами (нажатия на Inline-кнопки).
/// Отвечает за регистрацию, поиск и выполнение логики ответов на основе рефлексии.
/// </summary>
public class CallbackQueryController
{
  #region previous code
  // static List<ICallbackQueryCommand> commandList = Assembly.GetExecutingAssembly()
  //   .GetTypes()
  //   .Where(t => typeof(ICallbackQueryCommand).IsAssignableFrom(t)
  //     && !t.IsInterface
  //     && !t.IsAbstract)
  //   .Select(t => (ICallbackQueryCommand)Activator.CreateInstance(t)!)
  //   .ToList();

  // public async static Task Activate(ITelegramBotClient client, Update update)
  // {
  //   if (update.Type != UpdateType.CallbackQuery) return;

  //   var callbackQuery = update.CallbackQuery;
  //   if (callbackQuery == null) return;

  //   var data = callbackQuery.Data;

  //   var command = commandList.FirstOrDefault(
  //     c => c.Name?.Equals(data, StringComparison.OrdinalIgnoreCase) ?? false);

  //   if (command is null) return;

  //   await command.Answer(callbackQuery, client, update);
  // }
  #endregion
  // <summary>
  /// Реестр доступных Callback-команд. 
  /// Ключ: Значение callback_data кнопки в нижнем регистре.
  /// Значение: Тип класса, реализующего обработку нажатия.
  /// </summary>
  private static readonly Dictionary<string, Type> _commandRegistry;
  /// <summary>
  /// Инициализирует статический реестр Callback-команд.
  /// Сканирует сборку на наличие классов, наследующих <see cref="BaseCallbackCommand"/>.
  /// Выполняется один раз при первом обращении к классу.
  /// </summary>
  static CallbackQueryController()
  {
    _commandRegistry = Assembly.GetExecutingAssembly()
      .GetTypes()
      .Where(t => typeof(BaseCallbackCommand).IsAssignableFrom(t) && !t.IsAbstract)
      .ToDictionary(
        t => ((BaseCallbackCommand)Activator.CreateInstance(t, null!, null!)!).Name.ToLower(),
        t => t
      );
  }
  /// <summary>
  /// Основная точка входа для обработки входящих Callback-запросов.
  /// </summary>
  /// <param name="client">Экземпляр клиента Telegram Bot API.</param>
  /// <param name="update">Полный объект обновления от Telegram.</param>
  public static async Task HandleCommandAsync(ITelegramBotClient client, Update update)
  {
    if (update.Type != UpdateType.CallbackQuery) return;

    var callbackQuery = update.CallbackQuery;
    if (callbackQuery == null || callbackQuery.Data == null) return;

    var data = callbackQuery.Data;

    if (_commandRegistry.TryGetValue(data.ToLower(), out var commandType))
    {
      var comand = (BaseCallbackCommand)Activator.CreateInstance(commandType)!;
      comand.Initialize(client, update);
      await comand.Answer(callbackQuery);
    }
  }
}
