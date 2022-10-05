using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using omdb_search.Extensions;
using omdb_search.Models.Omdb;
using System.Net;
using System.Text;

namespace omdb_search.Services.Omdb
{
    public class OmdbService : IOmdbService
    {
        private readonly ILogger<OmdbService> logger;
        private string? apiKey => Environment.GetEnvironmentVariable("API_KEY");
        private string baseUri => $"http://www.omdbapi.com/?apikey={apiKey}";

        private HttpClient client = new HttpClient();

        public OmdbService(ILogger<OmdbService> logger)
        {
            this.logger = logger;
        }

        public async Task<Movie> SearchById(string id)
        {
            var sb = new StringBuilder(baseUri);
            var postUri = sb.Append($"&i={id}").ToString();
            var responseStr = await sendToOmdbApi(postUri);
            return ReadResponseData(responseStr);
        }

        public async Task<string> SearchByName(string name)
        {
            var sb = new StringBuilder(baseUri);
            var postUri = sb.Append($"&s={name}").ToString();
            return await sendToOmdbApi(postUri);
        }
        private async Task<string> sendToOmdbApi(string postUri)
        {
            logger.LogInformation($"Posting to: {postUri}");
            var response = await client.GetAsync(postUri);
            var responseStr = response.Content.ReadAsStringAsync().Result;
            logger.LogInformation($"Response: {responseStr}");
            return responseStr;
        }

        public Movie ReadResponseData(string responseStr)
        {
            var movieDict = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(responseStr);
            if (movieDict == null)
                throw new ArgumentNullException(nameof(movieDict));

            var result = new Movie
            {
                Title = movieDict["Title"],
                Poster = movieDict["Poster"],
            };

            movieDict.RemoveAll((key, value) => key == "Title" || key == "Poster" || key == "Ratings");
            result.Details = movieDict;
            return result;
        }
    }
}
