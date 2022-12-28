using HtmlAgilityPack;
using NewsParser.Dal;
using NewsParser.Models;
using NewsParser.Parsers;
using NewsParser.Services;

namespace NewsParser;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly NewsRepository _repository;
    private readonly TelegramService _tgService;

    public Worker(ILogger<Worker> logger, NewsRepository repository, TelegramService tgService)
    {
        _logger = logger;
        _repository = repository;
        _tgService = tgService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var web = new HtmlWeb();
                var sites = await _repository.GetNewsSitesAsync();

                var htmlDocs = new List<HtmlDocument>();

                foreach (var site in sites)
                {
                    var doc = await web.LoadFromWebAsync(site.Link);
                    NewsParserBase parser = new NewsLinkTitlesParser(site.XpathString, site.Id);

                    var parsedNews = parser.GetNews(doc);

                    var lastNews = await _repository.GetLastNewsAsync(site.Id, parsedNews.Count());

                    var freshNews = parsedNews.Where(
                        x => !lastNews.Select(y => y.Link).Contains(x.Link)
                    );

                    await _repository.AddNewsAsync(freshNews);

                    await SendNewsToTelegram(freshNews, stoppingToken);
                }
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "\n" + ex.StackTrace);
                await Task.Delay(20_000, stoppingToken);
            }
        }
    }

    private async Task SendNewsToTelegram(IEnumerable<News> news, CancellationToken stoppingToken)
    {
        foreach (var n in news)
        {
            await Task.Delay(2_000, stoppingToken);
            await _tgService.SendMessage($"{n.Label}\n{n.Link}");
        }
    }
}
