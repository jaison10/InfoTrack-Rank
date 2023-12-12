namespace InfoTrack_CounterAPI.Models.DTO
{
    public class SearchRequest
    {
        public string SearchString { get; set; }
        public string Url { get; set; }
        public Guid SearchEngineId { get; set; }
    }
}
