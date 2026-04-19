using System.Net;

namespace MinimalTelegramBot.Formatting.Extensions;

/// <summary>
/// todo: docs
/// </summary>
public static partial class TelegramFormattingExtensions
{
    extension(string? text)
    {
        /// <summary>
        ///     Wraps text into Telegram HTML bold tag with safe encoding.
        /// </summary>
        public string ToTelegramHtmlBold()
        {
            return $"<b>{text.EscapeTelegramHtml()}</b>";
        }

        /// <summary>
        ///     Wraps text into Telegram HTML italic tag with safe encoding.
        /// </summary>
        public string ToTelegramHtmlItalic()
        {
            return $"<i>{text.EscapeTelegramHtml()}</i>";
        }

        /// <summary>
        ///     Wraps text into Telegram HTML code tag with safe encoding.
        /// </summary>
        public string ToTelegramHtmlCode()
        {
            return $"<code>{text.EscapeTelegramHtml()}</code>";
        }

        /// <summary>
        ///     Wraps text into Telegram HTML pre tag with safe encoding.
        /// </summary>
        public string ToTelegramHtmlPre()
        {
            return $"<pre>{text.EscapeTelegramHtml()}</pre>";
        }

        /// <summary>
        ///     Creates a safe Telegram HTML link.
        /// </summary>
        public string ToTelegramHtmlLink(string url)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(url);
            return $"<a href=\"{WebUtility.HtmlEncode(url)}\">{text.EscapeTelegramHtml()}</a>";
        }

        /// <summary>
        ///     Wraps text into Telegram MarkdownV2 bold markup with escaping.
        /// </summary>
        public string ToTelegramMarkdownV2Bold()
        {
            return $"*{text.EscapeTelegramMarkdownV2()}*";
        }

        /// <summary>
        ///     Wraps text into Telegram MarkdownV2 italic markup with escaping.
        /// </summary>
        public string ToTelegramMarkdownV2Italic()
        {
            return $"_{text.EscapeTelegramMarkdownV2()}_";
        }

        /// <summary>
        ///     Wraps text into Telegram MarkdownV2 inline code markup with escaping.
        /// </summary>
        public string ToTelegramMarkdownV2Code()
        {
            return $"`{text.EscapeTelegramMarkdownV2()}`";
        }

        /// <summary>
        ///     Wraps text into Telegram MarkdownV2 code block markup with escaping.
        /// </summary>
        public string ToTelegramMarkdownV2Pre(string? language = null)
        {
            var escaped = text.EscapeTelegramMarkdownV2();

            if (string.IsNullOrWhiteSpace(language))
            {
                return $"```{Environment.NewLine}{escaped}{Environment.NewLine}```";
            }

            return $"```{language.EscapeTelegramMarkdownV2()}{Environment.NewLine}{escaped}{Environment.NewLine}```";
        }

        /// <summary>
        ///     Creates a safe Telegram MarkdownV2 link.
        /// </summary>
        public string ToTelegramMarkdownV2Link(string url)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(url);

            // For MarkdownV2, both link text and URL should be escaped.
            // Parentheses must be escaped in URL too.
            var escapedText = text.EscapeTelegramMarkdownV2();
            var escapedUrl = url.EscapeTelegramMarkdownV2();

            return $"[{escapedText}]({escapedUrl})";
        }
    }
}
