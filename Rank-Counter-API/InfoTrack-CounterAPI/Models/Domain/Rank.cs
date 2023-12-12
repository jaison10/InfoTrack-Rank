using System.ComponentModel.DataAnnotations;

namespace InfoTrack_CounterAPI.Models.Domain
{
    public class Rank
    {
        [Key]
        public Guid Id { get; set; }
        public string SearchString { get; set; }
        public string Url { get; set; }
        public string Positions { get; set; }
        public DateTime Date { get; set; }
        public Guid SearchEngineId { get; set; }
        // navigation property
        public SearchEngine SearchEngine { get; set; }
    }
}
