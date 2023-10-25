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
            var histories = new List<RankHistory>();
            foreach(var history in rankHistory)
            {
                histories.Add(new RankHistory
                {
                    Url = history.Url,
                    SearchString = history.SearchString,
                    Date = history.Date, 
                    Positions = history.Positions,
                });
            }
            return Ok(histories);
        }
    }
}
