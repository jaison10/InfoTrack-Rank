using InfoTrack_CounterAPI.Data;
using InfoTrack_CounterAPI.Models.Domain;
using InfoTrack_CounterAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace InfoTrack_CounterAPI.Repositories.Implementation
{
    public class HistoryRepoImplement : IHistoryRepository
    {
        private readonly RankDbContext rankDbContext;

        public HistoryRepoImplement(RankDbContext rankDbContext)
        {
            this.rankDbContext = rankDbContext;
        }
        public async Task<List<Rank>> GetRankHistoryAsync()
        {
            return await this.rankDbContext.Rank.OrderByDescending(x=> x.Date).Include(nameof(SearchEngine)).ToListAsync(); 
        }
    }
}
