using InfoTrack_CounterAPI.Data;
using InfoTrack_CounterAPI.Models.Domain;
using DTO = InfoTrack_CounterAPI.Models.DTO;
using InfoTrack_CounterAPI.Repositories.Interface;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace InfoTrack_CounterAPI.Repositories.Implementation
{
    public class SearchRepoImplement : ISearchRepository
    {
        private readonly HttpClient client;
        private readonly RankDbContext rankDbContext;

        public SearchRepoImplement(HttpClient client, RankDbContext rankDbContext)
        {
            this.client = client;
            this.rankDbContext = rankDbContext;
        }
        public async Task<string> Search(DTO.SearchRequest searchRequest)
        {
            if (searchRequest.Url.Length == 0 || searchRequest.SearchString.Length == 0)
                throw new ArgumentException("Invalid Input!");

            return "1, 2, 3";

            string requestUrl = $"https://www.google.co.uk/search?num=100&q={searchRequest.SearchString}";
            HttpResponseMessage response = await client.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                string htmlContent = await response.Content.ReadAsStringAsync();
                
            }

            var client2 = new HttpClient
            {
                BaseAddress = new Uri("https://www.google.co.uk/")
            };
            client2.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",
                "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36");

            var response2 = await client2.GetAsync(searchRequest.SearchString);

            response2.EnsureSuccessStatusCode();

            client2.Dispose();

            var RESULT = await response2.Content.ReadAsStringAsync();

            
        }

        public async Task<Rank> StoreRank(DTO.SearchRequest searchRequest, string positions)
        {
            var domainVal = new Rank
            {
                Url = searchRequest.Url,
                SearchString = searchRequest.SearchString,
                Date = new DateTime(),
                Positions = positions
            };
            var rank = await rankDbContext.Rank.AddAsync(domainVal);
            await this.rankDbContext.SaveChangesAsync();
            if (rank == null) { return null; }
            return rank.Entity;
        }

    }
}
