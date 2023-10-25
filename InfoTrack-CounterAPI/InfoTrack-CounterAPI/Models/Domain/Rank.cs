namespace InfoTrack_CounterAPI.Models.Domain
{
    public class Rank
    {
        public Guid Id { get; set; }
        public string SearchString { get; set; }
        public string Url { get; set; }
        public string Positions { get; set; }
        public DateTime Date { get; set; }
    }
}
