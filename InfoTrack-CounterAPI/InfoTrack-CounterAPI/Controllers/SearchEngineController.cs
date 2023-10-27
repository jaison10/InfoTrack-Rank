using InfoTrack_CounterAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrack_CounterAPI.Controllers
{
    [ApiController]
    [Route("controller")]
    public class SearchEngineController : Controller
    {
        private readonly ISearchEngineRepository searchEngineRepository;

        public SearchEngineController(ISearchEngineRepository searchEngineRepository)
        {
            this.searchEngineRepository = searchEngineRepository;
        }
        public async Task<IActionResult> GetSearchEngines()
        {
            var engines = await this.searchEngineRepository.GetAllSearchEnginesAsync();
            if (engines == null) return NotFound();
            return Ok(engines);
        }
    }
}
