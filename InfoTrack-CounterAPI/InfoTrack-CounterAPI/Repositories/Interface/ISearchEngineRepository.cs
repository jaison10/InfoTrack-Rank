using InfoTrack_CounterAPI.Models.Domain;

namespace InfoTrack_CounterAPI.Repositories.Interface
{
    public interface ISearchEngineRepository
    {
        Task<List<SearchEngine>> GetAllSearchEnginesAsync();
        Task<SearchEngine> GetSearchEngineByIdAsync(Guid engineId);
    }
}
