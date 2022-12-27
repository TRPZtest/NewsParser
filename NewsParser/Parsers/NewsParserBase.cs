using HtmlAgilityPack;
using NewsParser.Models;

namespace NewsParser.Parsers;

public abstract class NewsParserBase
{
    public abstract List<News> GetNews(HtmlDocument doc);
}
