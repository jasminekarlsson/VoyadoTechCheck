using System.Net.Http.Json;
using TechCheck.Web.Contrakts;
using TechCheck.Web.Contrakts.Dto;

namespace TechCheck.Web.Proxy
{
    public class WebHttpProxy(HttpClient httpclient) : ISearchWebService
    {
        public async Task<IList<SearchResult>> GetSearchResults(string searchInput)
        {
            var url = $"api/search?searchInput={Uri.EscapeDataString(searchInput)}";

            var results = await httpclient.GetFromJsonAsync<List<SearchResult>>($"api/data?searchInput={searchInput}");

            return results ?? [];
        }
    }
}
