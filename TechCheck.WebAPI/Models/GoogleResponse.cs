using System.Text.Json.Serialization;

namespace TechCheck.WebAPI.Models
{
    public class GoogleResponse
    {
        [JsonPropertyName("queries")]
        public Queries Queries { get; set; } = default!;
    }

    public class Queries
    {
        [JsonPropertyName("request")]
        public List<Request> Request { get; set; } = new();
    }

    public class Request
    {
        [JsonPropertyName("totalResults")]
        public string TotalResults { get; set; } = "0";
    }
}
