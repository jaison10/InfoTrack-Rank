using InfoTrack_CounterAPI.Models.DTO;
using InfoTrack_CounterAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrack_CounterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HistoryController : Controller
    {
        private readonly IHistoryRepository historyRepository;

        public HistoryController(IHistoryRepository historyRepository)
        {
            this.historyRepository = historyRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetRankHistory()
        {
            var rankHistory = await historyRepository.GetRankHistoryAsync();
            if(rankHistory == null)
            {
                return NotFound();
            }
            // converting to DTO Model
            var histories = new List<RankHistory>();
            foreach(var history in rankHistory)
            {
                histories.Add(new RankHistory
                {
                    Id = history.Id,
                    Url = history.Url,
                    SearchString = history.SearchString,
                    Date = history.ToString("yyyy-MM-dd HH:mm:ss"), 
                    Positions = history.Positions,
                    SearchEngine = new SearchEngine
                    {
                        Id = history.SearchEngine.Id,
                        Url = history.SearchEngine.Url,
                        EngineName = history.SearchEngine.EngineName
                    }
                });
            }
            return Ok(histories);
        }
    }
}
