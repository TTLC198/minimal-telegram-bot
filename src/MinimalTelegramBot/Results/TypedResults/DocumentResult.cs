using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MinimalTelegramBot.Results.TypedResults;

internal sealed class DocumentResult : FileResult
{
    public DocumentResult(Stream documentStream, string? caption = null, ParseMode parseMode = ParseMode.None) : base(documentStream, caption, parseMode)
    {
    }

    public DocumentResult(string documentPath, string? caption = null, ParseMode parseMode = ParseMode.None) : base(documentPath, caption, parseMode)
    {
    }

    protected override Task<Message> Send(BotRequestContext context, InputFile inputFile)
    {
        return context.Client.SendDocument(
            chatId: context.ChatId,
            document: inputFile,
            caption: Caption,
            parseMode: ParseMode);
    }
}
