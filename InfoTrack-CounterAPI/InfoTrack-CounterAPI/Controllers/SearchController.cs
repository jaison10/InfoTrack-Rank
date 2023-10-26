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

        public SearchController(ISearchRepository searchRepository)
        {
            this.searchRepository = searchRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Search([FromBody] DTO.SearchRequest searchRequest)
        {
            try
            {
                var positions = await searchRepository.Search(searchRequest);
                if (positions != null)
                {
                    var domainRank = await searchRepository.StoreRank(searchRequest, positions);
                    var returnVal = new DTO.SearchResult
                    {
                        Url = searchRequest.Url,
                        SearchString = searchRequest.SearchString,
                        Positions = positions,
                        StoredInDB = (domainRank != null) ? true : false
                    };
                    return Ok(returnVal);
                }
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
