using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MinimalTelegramBot.Results.TypedResults;

internal sealed class MessageResult : IResult
{
    private readonly bool _edit;
    private readonly string _message;
    private readonly bool _reply;
    private readonly ParseMode _parseMode;
    private readonly ReplyMarkup? _replyMarkup;

    public MessageResult(string message, ReplyMarkup? keyboard = null, ParseMode parseMode = ParseMode.None, bool reply = false, bool edit = false)
    {
        _message = message;
        _parseMode = parseMode;
        _replyMarkup = keyboard;
        _reply = reply;
        _edit = edit;
    }

    public Task ExecuteAsync(BotRequestContext context)
    {
        if (_edit)
        {
            return Edit(context);
        }

        return _reply ? Reply(context) : Send(context);
    }

    private Task<Message> Edit(BotRequestContext context)
    {
        if (_replyMarkup is not null and not InlineKeyboardMarkup)
        {
            throw new InvalidOperationException($"Cannot edit message using not {nameof(InlineKeyboardMarkup)}");
        }

        var inline = (InlineKeyboardMarkup?)_replyMarkup;
        var messageId = context.Update.CallbackQuery!.Message!.MessageId;
        return context.Client.EditMessageText(
            chatId: context.ChatId,
            messageId: messageId,
            text: _message,
            replyMarkup: inline,
            parseMode: _parseMode);
    }

    private Task<Message> Reply(BotRequestContext context)
    {
        var messageId = context.Update.Message!.MessageId;
        var replyParameters = new ReplyParameters { ChatId = context.ChatId, MessageId = messageId, };
        return context.Client.SendMessage(
            chatId: context.ChatId,
            text: _message,
            replyParameters: replyParameters,
            replyMarkup: _replyMarkup,
            parseMode: _parseMode);
    }

    private Task<Message> Send(BotRequestContext context)
    {
        return context.Client.SendMessage(
            chatId: context.ChatId,
            text: _message,
            replyMarkup: _replyMarkup,
            parseMode: _parseMode);
    }
}
