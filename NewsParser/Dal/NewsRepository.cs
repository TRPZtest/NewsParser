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

    public async Task<List<News>> GetLastNews()
    {
        return await _context.News.FromSqlRaw("").ToListAsync();
    }

    public async Task<List<NewsSite>> GetNewsSites()
    {
        return await _context.NewsSites.FromSqlRaw("SELECT * FROM main.NewsSites").ToListAsync();
    }

    public async Task AddNews(IEnumerable<News> news)
    {
        await _context.Database.BeginTransactionAsync();
        foreach (var n in news)
        {
            var label = new SqliteParameter("$Label", n.Label);
            var text = new SqliteParameter("$Link", n.Link);
            await _context.Database.ExecuteSqlRawAsync(
                "INSERT INTO main.NewsList (Label, Link) VALUES ($Label, $Link)",
                label,
                text
            );
        }
        await _context.Database.CommitTransactionAsync();
        await _context.SaveChangesAsync();
    }
}
