using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MinimalTelegramBot.Results.TypedResults;

internal sealed class PhotoResult : FileResult
{
    public PhotoResult(Stream photoStream, string? caption = null, ParseMode parseMode = ParseMode.None) : base(photoStream, caption, parseMode)
    {
    }

    public PhotoResult(string photoPath, string? caption = null, ParseMode parseMode = ParseMode.None) : base(photoPath, caption, parseMode)
    {
    }

    public PhotoResult(Uri uri, string? caption, ParseMode parseMode = ParseMode.None) : base(uri, caption, parseMode)
    {
    }

    protected override Task<Message> Send(BotRequestContext context, InputFile inputFile)
    {
        return context.Client.SendPhoto(
            chatId: context.ChatId,
            photo: inputFile,
            caption: Caption,
            parseMode: ParseMode);
    }
}
