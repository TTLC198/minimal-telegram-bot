using Microsoft.Extensions.DependencyInjection;
using MinimalTelegramBot.Settings;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using File = System.IO.File;

namespace MinimalTelegramBot.Results.TypedResults;

internal abstract class FileResult : IResult
{
    private readonly string? _filePath;
    private readonly Uri? _uri;
    private readonly Stream? _fileStream;
    protected readonly string? Caption;

    protected ParseMode ParseMode;

    protected FileResult(Stream fileStream, string? caption = null, ParseMode parseMode = ParseMode.None)
    {
        _fileStream = fileStream;
        ParseMode = parseMode;
        Caption = caption;
    }

    protected FileResult(string filePath, string? caption = null, ParseMode parseMode = ParseMode.None)
    {
        _filePath = filePath;
        ParseMode = parseMode;
        Caption = caption;
    }

    protected FileResult(Uri uri, string? caption = null, ParseMode parseMode = ParseMode.None)
    {
        _uri = uri;
        ParseMode = parseMode;
        Caption = caption;
    }

    public Task ExecuteAsync(BotRequestContext context)
    {
        if (ParseMode == ParseMode.None)
            ParseMode = context.DefaultParseMode;

        if (_uri is not null)
        {
            return SendFromUri(context);
        }

        return _fileStream is null ? SendFromName(context) : SendFromStream(context, _fileStream);
    }

    private Task<Message> SendFromName(BotRequestContext context)
    {
        var stream = File.OpenRead(_filePath!);
        return SendFromStream(context, stream);
    }

    private Task<Message> SendFromUri(BotRequestContext context)
    {
        var baseUrl = context.Services.GetRequiredService<WebApplicationConfiguration>().BaseUrl ??
                      throw new InvalidOperationException("Base URL was not configured.");
        var fullUri = new Uri(baseUrl, _uri!);
        var file = new InputFileUrl(fullUri);
        return Send(context, file);
    }

    private Task<Message> SendFromStream(BotRequestContext context, Stream stream)
    {
        context.RegisterForDispose(stream);
        var file = new InputFileStream(stream);
        return Send(context, file);
    }

    protected abstract Task<Message> Send(BotRequestContext context, InputFile inputFile);
}
