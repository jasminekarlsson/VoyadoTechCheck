using System.Text.Json.Serialization;

namespace TechCheck.WebAPI.Models
{
    public class BingResponse
    {
        [JsonPropertyName("search_information")]
        public SearchInformation? SearchInfo { get; set; } 
    }

    public class SearchInformation
    {
        [JsonPropertyName("total_results")]
        public long TotalResults { get; set; } = 0;
    }
}
