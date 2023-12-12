using DTO = InfoTrack_CounterAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using InfoTrack_CounterAPI.Repositories.Interface;

namespace InfoTrack_CounterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : Controller
    {
        private readonly ISearchRepository searchRepository;
        private readonly ISearchEngineRepository searchEngineRepository;

        public SearchController(ISearchRepository searchRepository, ISearchEngineRepository searchEngineRepository)
        {
            this.searchRepository = searchRepository;
            this.searchEngineRepository = searchEngineRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Search([FromBody] DTO.SearchRequest searchRequest)
        {
            try
            {
                // obtaining ranks
                var ranks = await searchRepository.Search(searchRequest);
                // creating dto for return value
                var returnVal = new DTO.SearchResult
                {
                    Url = searchRequest.Url,
                    SearchString = searchRequest.SearchString,
                    Positions = ranks,
                    SearchEngineId = searchRequest.SearchEngineId
                };
                if (ranks != null && ranks.Length > 0)
                {
                    // stored the rank details in the DB
                    var domainRank = await searchRepository.StoreRank(searchRequest, ranks);
                    //if value returned after storing, considered as the storage was successful
                    returnVal.StoredInDB = (domainRank != null) ? true : false;
                }
                return Ok(returnVal);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
