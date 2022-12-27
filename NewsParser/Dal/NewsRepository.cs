using Microsoft.EntityFrameworkCore;
using NewsParser.Models;
using System.Linq;

namespace NewsParser.Dal;

public class NewsRepository
{
    private readonly NewsDbContext _context;
    public NewsRepository(NewsDbContext context)
    {
        _context = context;
    }
    public async Task<List<News>>GetLastNews()
    {       
        return  await _context.News.FromSqlRaw("").ToListAsync();
    }
    public async Task<List<NewsSite>>GetNewsSites()
    {
        return await _context.NewsSites.FromSqlRaw("SELECT * FROM main.NewsSites").ToListAsync();
    }
}
