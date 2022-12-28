using NewsParser.Models;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace NewsParser.Parsers;

public class NewsLinkTitlesParser : NewsParserBase
{
    private readonly int _siteId;
    private readonly string _xpath;

    public NewsLinkTitlesParser(string xpath, int siteId)
    {
        _siteId = siteId;
        _xpath = xpath;
    }

    public override List<News> GetNews(HtmlDocument doc)
    {
        var newsListNodes = doc.DocumentNode.SelectNodes(_xpath);

        if (newsListNodes == null)
            throw new Exception("Wrong xpath");

        var news = new List<News>();

        foreach (var n in newsListNodes)
        {
            news.Add(
                new News
                {
                    Label = n.InnerText.Trim().Replace("&quot;", ""),
                    Link = n.GetAttributeValue("href", ""),
                    NewsSiteId = _siteId
                }
            );
        }
        return news;
    }
}
