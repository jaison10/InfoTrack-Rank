using InfoTrack_CounterAPI.Models.Domain;
using DTO = InfoTrack_CounterAPI.Models.DTO;

namespace InfoTrack_CounterAPI.Repositories.Interface
{
    public interface ISearchRepository
    {
        Task<string> Search(DTO.SearchRequest searchRequest);

        Task<Rank> StoreRank(DTO.SearchRequest searchRequest, string positions);
    }
}
