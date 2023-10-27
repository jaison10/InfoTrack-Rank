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
                //check if the selected search engine present
                if(await searchEngineRepository.GetSearchEngineByIdAsync(searchRequest.SearchEngineId) != null)
                {
                    // obtaining ranks
                    var ranks = await searchRepository.Search(searchRequest);
                    if (ranks != null && ranks.Length > 0)
                    {
                        // stored the rank details in the DB
                        var domainRank = await searchRepository.StoreRank(searchRequest, ranks);
                        var returnVal = new DTO.SearchResult
                        {
                            Url = searchRequest.Url,
                            SearchString = searchRequest.SearchString,
                            Positions = ranks,
                            //if value returned after storing, considered as the storage was successful
                            StoredInDB = (domainRank != null) ? true : false,
                            SearchEngineId = searchRequest.SearchEngineId
                        };
                        return Ok(returnVal);
                    }
                    return NotFound();
                }
                else {
                    return NotFound(); 
                }
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
