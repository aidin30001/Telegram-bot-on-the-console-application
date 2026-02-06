using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Command.Slash;
/// <summary>
/// Представляет интерфейс для реализации команды с "слэшем" для Telegram бота.
/// </summary>
public interface ISlashCommand
{
  /// <summary>
  /// Имя команды. Название команды должно быть с слэшом
  /// <para>пример: /start</para>
  /// </summary>
  string Name { get; }
  /// <summary>
  /// Описание команды.
  /// <para>Используется для отображения в списке команд.</para>
  /// </summary>
  string Description { get; }
  /// <summary>
  /// Реализации команды 
  /// </summary>
  /// <param name="message">Сообщение пользователя, вызвавшего команду.
  /// Содержит текст команды, идентификатор чата и другие данные.</param>
  /// <param name="argument">Дополнительный аргумент команды, если пользователь передал текст после команды.</param>
  /// <returns>Асинхронная задача</returns>
  Task Execute(Message message, string? argument = null);
}
