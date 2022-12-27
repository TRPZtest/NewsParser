using NewsParser;
using NewsParser.Dal;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        services.AddDbContext<NewsDbContext>(options => options.UseSqlite(configuration.GetConnectionString("NewsBdConnectionString")), ServiceLifetime.Singleton);
        services.AddSingleton<NewsRepository>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
