namespace InfoTrack_CounterAPI.Models.DTO
{
    public class SearchResult
    {
        public string SearchString { get; set; }
        public string Url { get; set; }
        public string Positions { get; set; }
        public bool StoredInDB { get; set; }
    }
}
