using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Command.Slash;
/// <summary>
/// Представляет интерфейс для реализации команды с "слэшем" для Telegram бота.
/// <para>
/// Represents an interface for implementing a slash command for a Telegram bot.
/// </para>
/// </summary>
public interface ISlashCommand
{
  /// <summary>
  /// Имя команды. Название команды должно быть с слэшом.
  /// <para>пример: /start</para>
  /// <para>
  /// Command name. The command name should start with a slash.
  /// </para>
  /// <para>
  /// Example: /start
  /// </para>
  /// </summary>
  string Name { get; }
  /// <summary>
  /// Описание команды.
  /// <para>Используется для отображения в списке команд.</para>
  /// <para>
  /// Command description.
  /// </para>
  /// <para>
  /// Used for displaying in the command list.
  /// </para>
  /// </summary>

  string Description { get; }
  /// <summary>
  /// Реализация команды.
  /// <para>
  /// Command execution logic.
  /// </para>
  /// </summary>
  /// <param name="message">
  /// Сообщение пользователя, вызвавшего команду.
  /// Содержит текст команды, идентификатор чата и другие данные.
  /// <para>
  /// Message from the user who invoked the command.
  /// Contains the command text, chat ID, and other relevant data.
  /// </para>
  /// </param>
  /// <param name="argument">
  /// Дополнительный аргумент команды, если пользователь передал текст после команды.
  /// <para>
  /// Optional command argument if the user provided text after the command.
  /// </para>
  /// </param>
  /// <returns>
  /// Асинхронная задача.
  /// <para>
  /// Asynchronous task.
  /// </para>
  /// </returns>
  Task Execute(Message message, string? argument = null);
}
