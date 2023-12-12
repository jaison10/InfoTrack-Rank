using InfoTrack_CounterAPI.Models.DTO;
using InfoTrack_CounterAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrack_CounterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchEngineController : Controller
    {
        private readonly ISearchEngineRepository searchEngineRepository;

        public SearchEngineController(ISearchEngineRepository searchEngineRepository)
        {
            this.searchEngineRepository = searchEngineRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetSearchEngines()
        {
            var engines = await this.searchEngineRepository.GetAllSearchEnginesAsync();
            if (engines == null) return NotFound();

            var enginesDto = new List<SearchEngine>();
            foreach (var engine in engines)
            {
                enginesDto.Add(new SearchEngine
                {
                    Id = engine.Id,
                    Url = engine.Url,
                    EngineName = engine.EngineName
                });
            }
            return Ok(enginesDto);
        }
    }
}
