using Microsoft.Extensions.Options;
using System.Text.Json;
using TechCheck.Web.Contrakts.Dto;
using TechCheck.WebAPI.Constants;
using TechCheck.WebAPI.Interface;
using TechCheck.WebAPI.Models;
using TechCheck.WebAPI.Models.Configuration;

namespace TechCheck.WebAPI.Services
{
    public class GoogleService(HttpClient httpClient, ILogger<GoogleService> logger, IOptions<GoogleApiOptions> options) : ISearchcollector
    {
        public async Task<SearchResult> GetNumberOfResultsAsync(List<string> searchWords)
        {
            try
            {
                var tasks = searchWords.Select(word => SearchGoogleResults(word, httpClient, options.Value.ApiKey, options.Value.GoogleSearchEngineId));
                var results = await Task.WhenAll(tasks);
                
                return new SearchResult
                {
                    SiteName = Sitename.Google,
                    NumberOfResults = results.Sum()
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to recieve data from Google");
                return new SearchResult
                {
                    SiteName = Sitename.Google,
                    NumberOfResults = -1 
                };
            }
        }

        private async Task<long> SearchGoogleResults(string searchWord, HttpClient httpClient, string apiKey, string searchEngineId)
        {
            var url = string.Format($"https://www.googleapis.com/customsearch/v1?key={apiKey}&cx={searchEngineId}&q={searchWord}");
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var googleResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GoogleResponse>(googleResponse);

            if (result?.Queries.Request != null && result.Queries.Request.Count != 0 && long.TryParse(result!.Queries.Request[0].TotalResults, out var totalResults))
            {

                logger.LogInformation(message: "Total HTTP Google Results for {SearchWord}: {TotalResults}", searchWord, totalResults);
                return totalResults;
            }

            logger.LogWarning("No {SiteName} Results found for {SearchWord}", Sitename.Google, searchWord);
            return 0;
        }
    }
}
