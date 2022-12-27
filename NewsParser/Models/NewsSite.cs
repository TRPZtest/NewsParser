using System.ComponentModel.DataAnnotations;

namespace NewsParser.Models;

public class NewsSite
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
}
