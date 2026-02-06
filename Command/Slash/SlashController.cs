using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Command.Slash;
/// <summary>
/// Контроллер управления слэш-командами бота.
/// Отвечает за регистрацию, поиск и выполнение команд на основе рефлексии.
/// <para>
/// Controller for managing bot slash commands.
/// Responsible for registration, lookup, and execution of commands using reflection.
/// </para>
/// </summary>
public class SlashController
{
  #region previous code
  // static List<ISlashCommand> commandList;
  // static SlashController()
  // {
  //   commandList = Assembly.GetExecutingAssembly()
  //    .GetTypes()
  //    .Where(t => typeof(ISlashCommand).IsAssignableFrom(t)
  //                && !t.IsInterface
  //                && !t.IsAbstract)
  //    .Select(t => (ISlashCommand)Activator.CreateInstance(t)!)
  //    .ToList();
  // }

  // public async static Task Activate(string messageText, ITelegramBotClient client, Message message, Update update)
  // {
  //   string[] parts = messageText.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
  //   if (parts.Length == 0) return;

  //   string commandName = parts[0];
  //   string? argument = parts.Length > 1 ? parts[1] : null;

  //   var command = commandList.FirstOrDefault(
  //     c => c.Name?.Equals(commandName, StringComparison.OrdinalIgnoreCase) ?? false);

  //   if (command is null) return;

  //   await command.Execute(message, client, update, argument);
  // }
  // public static List<BotCommand> Hint()
  // {
  //   var comand = new List<BotCommand>();

  //   commandList.ForEach(c =>
  //   {
  //     comand.AddRange(new BotCommand
  //     {
  //       Command = c.Name,
  //       Description = c.Description
  //     });
  //   });

  //   return  comand; 
  // }
  #endregion

  /// <summary>
  /// Реестр доступных команд.
  /// Ключ: Имя команды в нижнем регистре (например, "/start").
  /// Значение: Тип класса, реализующего эту команду.
  /// <para>
  /// Registry of available commands.
  /// Key: Lowercase command name (e.g., "/start").
  /// Value: Type of the class implementing the command.
  /// </para>
  /// </summary>
  private static readonly Dictionary<string, Type> _commandRegistry;
  /// <summary>
  /// Инициализирует статический реестр команд.
  /// Сканирует текущую сборку на наличие классов, наследующих <see cref="BaseSlashCommand"/>.
  /// Выполняется один раз при первом обращении к классу.
  /// <para>
  /// Initializes the static registry of commands.
  /// Scans the current assembly for classes inheriting from <see cref="BaseSlashCommand"/>.
  /// Executed once on first access to the class.
  /// </para>
  /// </summary>

  static SlashController()
  {
    _commandRegistry = Assembly.GetExecutingAssembly()
      .GetTypes()
      .Where(t => typeof(BaseSlashCommand).IsAssignableFrom(t) && !t.IsAbstract)
      .ToDictionary(
        t => ((BaseSlashCommand)Activator.CreateInstance(t, null!, null!)!).Name.ToLower(),
        t => t
      );
  }
  /// <summary>
  /// Основная точка входа для обработки входящих команд.
  /// <para>
  /// Main entry point for handling incoming slash commands.
  /// </para>
  /// </summary>
  /// <param name="messageText">
  /// Полный текст сообщения пользователя.
  /// <para>
  /// Full text of the user's message.
  /// </para>
  /// </param>
  /// <param name="client">
  /// Экземпляр клиента Telegram Bot API.
  /// <para>
  /// Telegram Bot API client instance.
  /// </para>
  /// </param>
  /// <param name="message">
  /// Объект сообщения.
  /// <para>
  /// Message object.
  /// </para>
  /// </param>
  /// <param name="update">
  /// Полный объект обновления от Telegram.
  /// <para>
  /// Full update object from Telegram.
  /// </para>
  /// </param>
  public static async Task HandleCommandAsync(string messageText, ITelegramBotClient client, Message message, Update update)
  {
    var (cmdName, argument) = ParseInput(messageText);

    if (_commandRegistry.TryGetValue(cmdName.ToLower(), out var commandType))
    {
      var command = (BaseSlashCommand)Activator.CreateInstance(commandType)!;
      command.Initialize(client, update);
      await command.Execute(message, argument);
    }
  }
  /// <summary>
  /// Разбирает входящую строку на имя команды и аргумент.
  /// <para>
  /// Parses the input string into command name and argument.
  /// </para>
  /// </summary>
  /// <example>
  /// "/set 10" -> Name: "/set", Arg: "10"
  /// </example>
  /// <param name="text">
  /// Исходный текст сообщения.
  /// <para>
  /// Original message text.
  /// </para>
  /// </param>
  /// <returns>
  /// Кортеж, содержащий имя команды и необязательный аргумент.
  /// <para>
  /// Tuple containing the command name and optional argument.
  /// </para>
  /// </returns>
  private static (string Name, string? Arg) ParseInput(string text)
  {
    string[] parts = text.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
    return (parts[0], parts.Length > 1 ? parts[1] : null);
  }
  /// <summary>
  /// Формирует список команд для отображения в интерактивном меню Telegram (кнопка "Menu").
  /// <para>
  /// Generates a list of commands for display in the Telegram interactive menu ("Menu" button).
  /// </para>
  /// </summary>
  /// <returns>
  /// Список объектов <see cref="BotCommand"/> для регистрации в API.
  /// <para>
  /// List of <see cref="BotCommand"/> objects for registration in the API.
  /// </para>
  /// </returns>
  public static List<BotCommand> GetBotCommandsForMenu()
  {
    return _commandRegistry.Values.Select(type =>
    {
      var cmd = (BaseSlashCommand)Activator.CreateInstance(type, null!, null!)!;
      return new BotCommand { Command = cmd.Name, Description = cmd.Description };
    }).ToList();
  }
}
