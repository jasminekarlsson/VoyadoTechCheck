using System.Text.Json;
using TechCheck.Web.Contrakts.Dto;
using TechCheck.WebAPI.Constants;
using TechCheck.WebAPI.Interface;
using TechCheck.WebAPI.Models;

namespace TechCheck.WebAPI.Services
{
    public class BingService(HttpClient httpClient, ILogger<BingService> logger, IConfiguration configuration) : ISearchcollector
    {
        public async Task<SearchResult> GetNumberOfResultsAsync(List<string> searchWords)
        {
            try
            {
                var apiKey = configuration["BingApiKey"];

                if (string.IsNullOrEmpty(apiKey))
                {
                    logger.LogError("Bing API key is not configured.");
                    return new SearchResult
                    {
                        SiteName = Sitename.Bing,
                        NumberOfResults = -1
                    };
                }

                var tasks = searchWords.Select(word => SearchBingResults(word, httpClient, apiKey));
                var results = await Task.WhenAll(tasks);

                return new SearchResult
                {
                    SiteName = Sitename.Bing,
                    NumberOfResults = results.Sum()
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to recieve data from Bing");

                return new SearchResult
                {
                    SiteName = Sitename.Bing,
                    NumberOfResults = -1
                };
            }
        }

        private async Task<long> SearchBingResults(string searchWord, HttpClient httpClient, string apiKey)
        {
            var response = await httpClient.GetAsync(string.Format($"https://serpapi.com/search?engine=bing&q={searchWord}&api_key={apiKey}"));
            response.EnsureSuccessStatusCode();

            var bingResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<BingResponse>(bingResponse);

            if (result != null && result.SearchInfo != null)
            {
                logger.LogInformation("Total BING Results for {SearchWord}: {TotalResults}", searchWord, result.SearchInfo.TotalResults);
                return result.SearchInfo.TotalResults;
            }

            logger.LogWarning("No {SiteName} Results found for {SearchWord}", Sitename.Bing, searchWord);
            return 0;
        }
    }
}
