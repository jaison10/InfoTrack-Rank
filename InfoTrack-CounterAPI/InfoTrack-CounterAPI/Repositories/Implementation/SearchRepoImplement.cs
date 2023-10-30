using InfoTrack_CounterAPI.Data;
using InfoTrack_CounterAPI.Models.Domain;
using DTO = InfoTrack_CounterAPI.Models.DTO;
using InfoTrack_CounterAPI.Repositories.Interface;
using System.Text.RegularExpressions;
using System.Web;

namespace InfoTrack_CounterAPI.Repositories.Implementation
{
    public class SearchRepoImplement : ISearchRepository
    {
        private readonly RankDbContext rankDbContext;
        private readonly ISearchEngineRepository searchEngineRepository;
        private readonly int NoOfItems = 100;
        private readonly string Agent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36";
        public SearchRepoImplement(
            RankDbContext rankDbContext, 
            ISearchEngineRepository searchEngineRepository)
        {
            this.rankDbContext = rankDbContext;
            this.searchEngineRepository = searchEngineRepository;
        }
        public async Task<string> Search(DTO.SearchRequest searchRequest)
        {
            if (searchRequest.Url.Length == 0 || searchRequest.SearchString.Length == 0)
                throw new ArgumentException("Invalid Input!");

            var selectedEngine = await this.searchEngineRepository.GetSearchEngineByIdAsync(searchRequest.SearchEngineId);
            if (selectedEngine == null)  throw new ArgumentException("Invalid Search Engine Input!");

            var ranks = await this.GetRank(searchRequest, selectedEngine);

            return ranks.ToString();
        }

        private async Task<string> GetRank(DTO.SearchRequest searchRequest, SearchEngine selectedEngine)
        {
            string requestUrl = $"{selectedEngine.Url}{NoOfItems}&q={Uri.EscapeDataString(searchRequest.SearchString)}";

            using (HttpClient client = new HttpClient())
            {
                // trying to overcome cookie request
                //client.DefaultRequestHeaders.Add("Cookie", "CONSENT=YES+cb");

                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", Agent);

                HttpResponseMessage response = await client.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();
                //decoding to remove &gt, &lt, etc
                var dom_content = HttpUtility.HtmlDecode(await response.Content.ReadAsStringAsync());
                //var dom_content = HttpUtility.HtmlDecode(await client.GetStringAsync(requestUrl));

                //set the pattern that needs to be searched!
                var pattern = selectedEngine.UrlExtractionSyntax;
                //finding all the matches
                MatchCollection matches = Regex.Matches(dom_content, pattern);

                var ranks = "";
                int rank = 0;
                foreach (Match match in matches)
                {
                    //obtaining the url from the match found
                    string url = match.Groups[1].Value;
                    rank++;
                    //checking if the obtained URL is the URL client looking for
                    if (url.Contains(searchRequest.Url))
                    {
                        ranks += $"{rank.ToString()}, ";
                    }
                }
                client.Dispose();
                if (ranks.Length > 0) { ranks = ranks.Substring(0, ranks.Length - 2); }
                return ranks;
            }
        }
        public async Task<Rank> StoreRank(DTO.SearchRequest searchRequest, string positions)
        {
            var domainVal = new Rank
            {
                Url = searchRequest.Url,
                SearchString = searchRequest.SearchString,
                Date = DateTime.Now,
                Positions = positions,
                SearchEngineId = searchRequest.SearchEngineId
            };
            var rank = await rankDbContext.Rank.AddAsync(domainVal);
            if (rank == null) { return null; }
            await this.rankDbContext.SaveChangesAsync();
            return rank.Entity;
        }

    }
}
