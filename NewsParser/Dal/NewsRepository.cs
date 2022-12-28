using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NewsParser.Models;
using System.Data.SqlClient;
using System.Linq;

namespace NewsParser.Dal;

public class NewsRepository
{
    private readonly NewsDbContext _context;

    public NewsRepository(NewsDbContext context)
    {
        _context = context;
    }

    public async Task<List<News>> GetLastNewsAsync(int siteId, int limit)
    {
        var siteIdParam = new SqliteParameter("$SiteId", siteId);
        var limitParam = new SqliteParameter("$Limit", limit);

        return await _context.News
            .FromSqlRaw(
                "SELECT * FROM main.NewsList n WHERE n.NewsSiteId = $SiteId ORDER BY n.Id desc LIMIT $Limit",
                siteIdParam,
                limitParam
            )
            .ToListAsync();
    }

    public async Task<List<NewsSite>> GetNewsSitesAsync()
    {
        return await _context.NewsSites.FromSqlRaw("SELECT * FROM main.NewsSites").ToListAsync();
    }

    public async Task AddNewsAsync(IEnumerable<News> news)
    {
        await _context.Database.BeginTransactionAsync();
        foreach (var n in news)
        {
            var label = new SqliteParameter("$Label", n.Label);
            var text = new SqliteParameter("$Link", n.Link);
            var siteId = new SqliteParameter("$SiteId", n.NewsSiteId);
            await _context.Database.ExecuteSqlRawAsync(
                "INSERT INTO main.NewsList (Label, Link, NewsSiteId) VALUES ($Label, $Link, $SiteId)",
                label,
                text,
                siteId
            );
        }
        await _context.Database.CommitTransactionAsync();      
    }
}
