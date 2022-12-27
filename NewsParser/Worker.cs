using NewsParser.Dal;

namespace NewsParser;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly NewsRepository _repository;

    public Worker(ILogger<Worker> logger, NewsRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {            
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            var sites = await _repository.GetNewsSites();
            Console.WriteLine(sites.FirstOrDefault()?.Name + sites.FirstOrDefault()?.Link);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
