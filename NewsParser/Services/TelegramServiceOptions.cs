using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace NewsParser.Services;

public class TelegramServiceOptions
{
    [NotNull]
    public string? BotToken { get; set; } 
    
    [NotNull]
    public string? ChatId { get; set; }
}
