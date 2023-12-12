using InfoTrack_CounterAPI.Data;
using InfoTrack_CounterAPI.Models.Domain;
using InfoTrack_CounterAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace InfoTrack_CounterAPI.Repositories.Implementation
{
    public class SearchEngineRepoImplement : ISearchEngineRepository
    {
        private readonly RankDbContext rankDbContext;

        public SearchEngineRepoImplement(RankDbContext rankDbContext)
        {
            this.rankDbContext = rankDbContext;
        }
        public async Task<List<SearchEngine>> GetAllSearchEnginesAsync()
        {
            return await this.rankDbContext.SearchEngine.ToListAsync();
        }
        public async Task<SearchEngine> GetSearchEngineByIdAsync(Guid engineId)
        {
            return await this.rankDbContext.SearchEngine.FindAsync(engineId);
        }
    }
}
