using MinimalTelegramBot.Results.TypedResults;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MinimalTelegramBot.Results;

/// <summary>
///     Represents a collection of static methods for generating different types of <see cref="IResult"/>.
/// </summary>
public static class Results
{
    /// <summary>
    ///     Represents an empty <see cref="IResult"/> with no action.
    /// </summary>
    public static IResult Empty { get; } = new EmptyResult();

    /// <summary>
    ///     Creates a new <see cref="IResult"/> that sends a message to the chat.
    /// </summary>
    /// <param name="message">Message text.</param>
    /// <param name="keyboard">Message keyboard.</param>
    /// <param name="parseMode">Text parsing mode. See <a href="https://core.telegram.org/bots/api#formatting-options"/></param>
    /// <returns>Created <see cref="IResult"/>.</returns>
    public static IResult Message(string message, ReplyMarkup? keyboard = null, ParseMode parseMode = ParseMode.None)
    {
        ArgumentNullException.ThrowIfNull(message);

        return new MessageResult(
            message: message,
            keyboard: keyboard,
            parseMode: parseMode);
    }

    /// <summary>
    ///     Creates a new <see cref="IResult"/> that sends a message to the chat as a reply.
    /// </summary>
    /// <param name="message">Message text.</param>
    /// <param name="parseMode">Text parsing mode. See <a href="https://core.telegram.org/bots/api#formatting-options"/></param>
    /// <returns>Created <see cref="IResult"/>.</returns>
    public static IResult MessageReply(string message, ParseMode parseMode = ParseMode.None)
    {
        ArgumentNullException.ThrowIfNull(message);

        return new MessageResult(
            message: message,
            reply: true,
            parseMode: parseMode);
    }

    /// <summary>
    ///     Creates a new <see cref="IResult"/> that edits the bot message contained in the update.
    /// </summary>
    /// <param name="message">New message text.</param>
    /// <param name="keyboard">New message keyboard.</param>
    /// <param name="parseMode">Text parsing mode. See <a href="https://core.telegram.org/bots/api#formatting-options"/></param>
    /// <returns>Created <see cref="IResult"/>.</returns>
    public static IResult MessageEdit(string message, ReplyMarkup? keyboard = null, ParseMode parseMode = ParseMode.None)
    {
        ArgumentNullException.ThrowIfNull(message);

        return new MessageResult(
            message: message,
            keyboard: keyboard,
            edit: true,
            parseMode: parseMode);
    }

    /// <summary>
    ///     Creates a new <see cref="IResult"/> that answers the callback query.
    /// </summary>
    /// <returns>Created <see cref="IResult"/>.</returns>
    public static IResult CallbackAnswer()
    {
        return new CallbackAnswerResult();
    }

    /// <summary>
    ///     Creates a new <see cref="IResult"/> that sends a photo to the chat.
    /// </summary>
    /// <param name="uri">URI of the photo relative to the wwwroot directory.</param>
    /// <param name="caption">Photo caption.</param>
    /// <param name="parseMode">Text parsing mode. See <a href="https://core.telegram.org/bots/api#formatting-options"/></param>
    /// <returns>Created <see cref="IResult"/>.</returns>
    public static IResult Photo(Uri uri, string? caption = null, ParseMode parseMode = ParseMode.None)
    {
        ArgumentNullException.ThrowIfNull(uri);

        return new PhotoResult(
            uri: uri,
            caption: caption,
            parseMode: parseMode);
    }

    /// <summary>
    ///     Creates a new <see cref="IResult"/> that sends a photo to the chat.
    /// </summary>
    /// <param name="photoPath">Path of the photo file.</param>
    /// <param name="caption">Photo caption.</param>
    /// <param name="parseMode">Text parsing mode. See <a href="https://core.telegram.org/bots/api#formatting-options"/></param>
    /// <returns>Created <see cref="IResult"/>.</returns>
    public static IResult Photo(string photoPath, string? caption = null, ParseMode parseMode = ParseMode.None)
    {
        ArgumentNullException.ThrowIfNull(photoPath);

        return new PhotoResult(
            photoPath: photoPath,
            caption: caption,
            parseMode: parseMode);
    }

    /// <summary>
    ///     Creates a new <see cref="IResult"/> that sends a photo to the chat.
    /// </summary>
    /// <param name="photoStream">Stream representing the photo.</param>
    /// <param name="caption">Photo caption.</param>
    /// <param name="parseMode">Text parsing mode. See <a href="https://core.telegram.org/bots/api#formatting-options"/></param>
    /// <returns>Created <see cref="IResult"/>.</returns>
    public static IResult Photo(Stream photoStream, string? caption = null, ParseMode parseMode = ParseMode.None)
    {
        ArgumentNullException.ThrowIfNull(photoStream);

        return new PhotoResult(
            photoStream: photoStream,
            caption: caption,
            parseMode: parseMode);
    }

    /// <summary>
    ///     Creates a new <see cref="IResult"/> that sends a document to the chat.
    /// </summary>
    /// <param name="documentPath">Path of the document file.</param>
    /// <param name="caption">Document caption.</param>
    /// <param name="parseMode">Text parsing mode. See <a href="https://core.telegram.org/bots/api#formatting-options"/></param>
    /// <returns>Created <see cref="IResult"/>.</returns>
    public static IResult Document(string documentPath, string? caption = null, ParseMode parseMode = ParseMode.None)
    {
        ArgumentNullException.ThrowIfNull(documentPath);

        return new DocumentResult(
            documentPath: documentPath,
            caption: caption,
            parseMode: parseMode);
    }

    /// <summary>
    ///     Creates a new <see cref="IResult"/> that sends a document to the chat.
    /// </summary>
    /// <param name="documentStream">Stream representing the document.</param>
    /// <param name="caption">Document caption.</param>
    /// <param name="parseMode">Text parsing mode. See <a href="https://core.telegram.org/bots/api#formatting-options"/></param>
    /// <returns>Created <see cref="IResult"/>.</returns>
    public static IResult Document(Stream documentStream, string? caption = null, ParseMode parseMode = ParseMode.None)
    {
        ArgumentNullException.ThrowIfNull(documentStream);

        return new DocumentResult(
            documentStream: documentStream,
            caption: caption,
            parseMode: parseMode);
    }
}
