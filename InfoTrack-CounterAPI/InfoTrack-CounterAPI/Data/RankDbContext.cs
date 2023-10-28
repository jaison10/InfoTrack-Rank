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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add the configuration for your entities here
            modelBuilder.Entity<SearchEngine>().HasData(
                new SearchEngine { 
                    Id = Guid.NewGuid(),
                    EngineName = "Bing",
                    Url = "https://www.bing.com/search?count=", 
                    UrlExtractionSyntax = "<cite>(.*?)</cite>"
                },
                new SearchEngine { 
                    Id = Guid.NewGuid(), 
                    EngineName = "Google",
                    Url = "https://www.google.co.uk/search?num=",
                    UrlExtractionSyntax = "href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))"
                }
            );
        }
    }
}