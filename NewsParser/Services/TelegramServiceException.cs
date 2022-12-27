namespace NewsParser.Services;

public class TelegramServiceException : Exception
{
    public string TelegramMessage { get; }
    public string ChatId { get; }
    public string BotToken { get; }
}
