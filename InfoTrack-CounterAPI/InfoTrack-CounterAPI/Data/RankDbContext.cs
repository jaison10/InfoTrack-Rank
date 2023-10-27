using InfoTrack_CounterAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace InfoTrack_CounterAPI.Data
{
    public class RankDbContext : DbContext
    {
        public RankDbContext(DbContextOptions<RankDbContext> options) : base(options)
        {
        }
        public DbSet<Rank> Rank { get; set; }
        public DbSet<SearchEngine> SearchEngine { get; set; }
    }
}
