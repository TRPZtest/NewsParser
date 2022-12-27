using NewsParser.Models;
using HtmlAgilityPack;

namespace NewsParser.Parsers;

public class LigaParser : NewsParserBase
{
    public override List<News> GetNews(HtmlDocument doc)
    {
        var newsListNodes = doc.GetElementbyId("all-news").SelectNodes("//div[@class='news-nth-title news-time-var ' or @class='news-nth-title news-time-var']/a[not(@class)]");
        var news = new List<News>();

        foreach(var n in newsListNodes)
        {
            news.Add
            (
                new News { Label = n.InnerText, Link = n.GetAttributeValue("href", "") }
            );
        }
        return news;
    }
}
