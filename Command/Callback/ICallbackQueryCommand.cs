using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Command.Callback;
/// <summary>
/// Интерфейс для реализации команд, обрабатывающих <see cref="CallbackQuery"/> Telegram бота.
/// <para>CallbackQuery возникает, когда пользователь нажимает кнопку Inline-клавиатуры в сообщении бота.</para>
/// </summary>
public interface ICallbackQueryCommand
{
  /// <summary>
  /// Данные, которые передаются в <see cref="CallbackQuery.Data"/>.
  /// <para>Используется для различения разных Callback-команд.</para>
  /// </summary>
  string Name { get; }
  /// <summary>
  /// Обрабатывает CallbackQuery от пользователя.
  /// <para>Этот метод вызывается, когда пользователь нажимает кнопку в Inline-клавиатуре.</para>
  /// </summary>
  /// <param name="callback">
  /// Объект <see cref="CallbackQuery"/>, содержащий информацию о нажатой кнопке,
  /// идентификатор пользователя, данные кнопки и сообщение, к которому привязана кнопка.
  /// </param>
  /// <returns>Асинхронная задача <see cref="Task"/>, представляющая выполнение обработки CallbackQuery.</returns>
  Task Answer(CallbackQuery callback);
}
