using NewsParser;
using NewsParser.Dal;
using Microsoft.EntityFrameworkCore;
using NewsParser.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(
        (hostContext, services) =>
        {
            IConfiguration configuration = hostContext.Configuration;
            var telegramOptions = configuration
                .GetSection("TelegramConfig")
                .Get<TelegramServiceOptions>();
            services.AddSingleton(telegramOptions);
            services.AddSingleton<TelegramService>();
            services.AddDbContext<NewsDbContext>(
                options =>
                    options.UseSqlite(configuration.GetConnectionString("NewsBdConnectionString")),
                ServiceLifetime.Singleton
            );
            services.AddSingleton<NewsRepository>();
            services.AddHostedService<Worker>();
        }
    )
    .Build();

await host.RunAsync();
