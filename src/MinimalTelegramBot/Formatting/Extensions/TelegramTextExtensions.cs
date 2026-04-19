using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Telegram.Bot.Types.Enums;

namespace MinimalTelegramBot.Formatting.Extensions;

/// <summary>
/// todo: docs
/// </summary>
public static partial class TelegramTextExtensions
{
    [GeneratedRegex(@"([_*\[\]()~`>#+\-=|{}.!\\])", RegexOptions.Compiled)]
    private static partial Regex MarkdownV2EscapeRegex();

    /// <summary>
    /// todo: docs
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string EscapeTelegramMarkdownV2(this string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text ?? string.Empty;
        }

        return MarkdownV2EscapeRegex().Replace(text, @"\$1");
    }

    /// <summary>
    /// todo: docs
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string EscapeTelegramHtml(this string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text ?? string.Empty;
        }

        return WebUtility.HtmlEncode(text);
    }

    /// <summary>
    /// todo: docs
    /// </summary>
    /// <param name="text"></param>
    /// <param name="parseMode"></param>
    /// <returns></returns>
    public static string EscapeTelegram(this string? text, ParseMode parseMode)
    {
        return parseMode switch
        {
            ParseMode.MarkdownV2 => text.EscapeTelegramMarkdownV2(),
            ParseMode.Html => text.EscapeTelegramHtml(),
            _ => text ?? string.Empty
        };
    }

    /// <summary>
    ///     Joins non-empty lines with '\n'.
    /// </summary>
    public static string JoinTelegramLines(params string?[] parts)
    {
        var sb = new StringBuilder();

        foreach (var part in parts)
        {
            if (string.IsNullOrWhiteSpace(part))
            {
                continue;
            }

            if (sb.Length > 0)
            {
                sb.AppendLine();
            }

            sb.Append(part);
        }

        return sb.ToString();
    }
}
