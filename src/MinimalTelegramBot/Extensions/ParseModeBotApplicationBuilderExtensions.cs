using Telegram.Bot.Types.Enums;

namespace MinimalTelegramBot.Extensions;

/// <summary>
/// todo: docs
/// </summary>
public static class ParseModeBotApplicationBuilderExtensions
{
    private const string ParseModeKey = "__ParseMode";

    /// <summary>
    /// todo: docs
    /// </summary>
    /// <param name="app"></param>
    /// <param name="parseMode"></param>
    /// <returns></returns>
    public static IBotApplicationBuilder UseParseMode(
        this IBotApplicationBuilder app,
        ParseMode parseMode)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.Properties[ParseModeKey] = parseMode;
        return app;
    }
}
