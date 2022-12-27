using NewsParser.Dal;
using NewsParser.Models;
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
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            // var sites = await _repository.GetNewsSites();
            // Console.WriteLine(sites.FirstOrDefault()?.Name + sites.FirstOrDefault()?.Link);
            // await Task.Delay(1000, stoppingToken);

            // await _tgService.SendMessage("test");
            await _repository.AddNews(
                new List<News>
                {
                    new News { Link = "Test worker", Label = "Test Link" }
                }
            );
            return;
        }
    }
}
