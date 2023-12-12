namespace InfoTrack_CounterAPI.Models.Domain
{
    public class SearchEngine
    {
        public Guid Id { get; set; }
        public string EngineName { get; set; }
        public string Url { get; set; }
        public string UrlExtractionSyntax { get; set; } 
    }
}
