using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Command.Callback;
/// <summary>
/// Интерфейс для реализации команд, обрабатывающих <see cref="CallbackQuery"/> Telegram бота.
/// <para>
/// Interface for implementing commands that handle Telegram bot <see cref="CallbackQuery"/>.
/// </para>
/// <para>
/// CallbackQuery возникает, когда пользователь нажимает кнопку Inline-клавиатуры в сообщении бота.
/// </para>
/// <para>
/// CallbackQuery occurs when a user presses an inline keyboard button in a bot message.
/// </para>
/// </summary>
public interface ICallbackQueryCommand
{
  /// <summary>
  /// Данные, которые передаются в <see cref="CallbackQuery.Data"/>.
  /// Используется для различения разных Callback-команд.
  /// <para>
  /// Data passed to <see cref="CallbackQuery.Data"/>.
  /// Used to distinguish different callback commands.
  /// </para>
  /// </summary>
  string Name { get; }
  /// <summary>
  /// Обрабатывает CallbackQuery от пользователя.
  /// <para>
  /// Handles CallbackQuery from a user.
  /// </para>
  /// <para>
  /// Этот метод вызывается, когда пользователь нажимает кнопку в Inline-клавиатуре.
  /// </para>
  /// <para>
  /// This method is triggered when a user presses a button in an inline keyboard.
  /// </para>
  /// </summary>
  /// <param name="callback">
  /// Объект <see cref="CallbackQuery"/>, содержащий информацию о нажатой кнопке,
  /// идентификатор пользователя, данные кнопки и сообщение, к которому привязана кнопка.
  /// <para>
  /// <see cref="CallbackQuery"/> object containing information about the pressed button,
  /// user identifier, button data, and the message to which the button is attached.
  /// </para>
  /// </param>
  /// <returns>
  /// Асинхронная задача <see cref="Task"/>, представляющая выполнение обработки CallbackQuery.
  /// <para>
  /// Asynchronous <see cref="Task"/> representing CallbackQuery processing execution.
  /// </para>
  /// </returns>
  Task Answer(CallbackQuery callback);
}
