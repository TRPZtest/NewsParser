using System.ComponentModel.DataAnnotations;

namespace NewsParser.Services;

public class TelegramServiceOptions
{
    [Required]
    public string BotToken { get; set; }
    [Required]
    public string ChatId { get; set; }
}
