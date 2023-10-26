using InfoTrack_CounterAPI.Data;
using InfoTrack_CounterAPI.Models.Domain;
using DTO = InfoTrack_CounterAPI.Models.DTO;
using InfoTrack_CounterAPI.Repositories.Interface;
using System.Text.RegularExpressions;

namespace InfoTrack_CounterAPI.Repositories.Implementation
{
    public class SearchRepoImplement : ISearchRepository
    {
        private readonly HttpClient _client;
        private readonly RankDbContext rankDbContext;

        public SearchRepoImplement(HttpClient client, RankDbContext rankDbContext)
        {
            this._client = client;
            this.rankDbContext = rankDbContext;
        }
        public async Task<string> Search(DTO.SearchRequest searchRequest)
        {
            if (searchRequest.Url.Length == 0 || searchRequest.SearchString.Length == 0)
                throw new ArgumentException("Invalid Input!");

            string requestUrl = $"https://www.google.co.uk/search?num=100&q={searchRequest.SearchString.Replace(" ", "+")}";

            using (HttpClient client = new HttpClient())
            {
                // trying to overcome cookie request
                //client.DefaultRequestHeaders.Add("Cookie", "CONSENT=YES+cb");

                HttpResponseMessage response = await client.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();
                var dom_content = await response.Content.ReadAsStringAsync();

                //set the pattern that needs to be searched!
                var pattern = "href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))";

                var ranks = "";
                //finding all the matches
                MatchCollection matches = Regex.Matches(dom_content, pattern);

                int position = 0;
                foreach (Match match in matches)
                {
                    //obtaining the url from the match found
                    string url = match.Groups[1].Value;
                    position++;
                    //checking if the URL is the URL client looking for
                    if (url.Contains(searchRequest.Url))
                    {
                        ranks += $"{position.ToString()} , ";
                    }
                }
                client.Dispose();
                if(ranks.Length > 0) { ranks = ranks.Substring(0, ranks.Length - 1); }
                return ranks;
            }


            //var client2 = new HttpClient
            //{
            //    BaseAddress = new Uri("https://www.google.co.uk/")
            //};
            //client2.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",
            //    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36");

            ////var response2 = await client2.GetAsync(searchRequest.SearchString);
            //requestUrl = $"search?num=100&q={string.Join('+', searchRequest.SearchString.Split(' '))}";
            //var response2 = await client2.GetAsync(requestUrl);

            //response2.EnsureSuccessStatusCode();

            //client2.Dispose();

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
