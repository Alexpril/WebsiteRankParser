using Microsoft.EntityFrameworkCore;
using WebsiteRank.DAL.Models;

namespace WebsiteRank.DAL.Contexts
{
    public class WebsiteRankDbContext(DbContextOptions<WebsiteRankDbContext> options) : DbContext(options)
    {
        public DbSet<HighestWebsiteRank>? HighestWebsiteRanks { get; set; }
    }
}