using InfoTrack_CounterAPI.Models.Domain;

namespace InfoTrack_CounterAPI.Repositories.Interface
{
    public interface IHistoryRepository
    {
        Task<List<Rank>> GetRankHistoryAsync();
    }
}
