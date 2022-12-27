using Microsoft.EntityFrameworkCore;
using NewsParser.Models;

namespace NewsParser.Dal;

public class NewsDbContext : DbContext
{
    public DbSet<News> News { get; set; }
    public DbSet<NewsSite> NewsSites { get; set; }

    public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options) { }
}
