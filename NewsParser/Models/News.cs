using System.ComponentModel.DataAnnotations;

namespace NewsParser.Models;

public class News
{
    [Key]
    public int Id { get; set; }
    public string Label { get; set; }
    public string Link { get; set; }
}
