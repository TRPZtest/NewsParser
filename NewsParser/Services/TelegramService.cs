using Telegram;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace NewsParser.Services;

public class TelegramService
{   
    private readonly ChatId _chatId;
    private readonly TelegramBotClient _client;
    public TelegramService(TelegramServiceOptions serviceOptions)
    {
        _chatId = new Telegram.Bot.Types.ChatId(serviceOptions.ChatId);
        
        _client = new TelegramBotClient(new TelegramBotClientOptions(serviceOptions.BotToken));
    }
    public async Task SendMessage(string messageText)
    {
        await _client.SendTextMessageAsync(_chatId, messageText);     
    }        
}
