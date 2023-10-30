
namespace InfoTrack_CounterAPI.Models.DTO
{
    public class RankHistory
    {
        public Guid Id { get; set; }
        public string SearchString { get; set; }
        public string Url { get; set; }
        public string Positions { get; set; }
        public string Date { get; set; }
        public SearchEngine SearchEngine { get; set; }
    }
}
