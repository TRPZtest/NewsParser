using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HtmlAgilityPack;

namespace NewsParser.Models;

public class NewsSite
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Link { get; set; }
    public string XpathString { get; set; } = string.Empty;
}
