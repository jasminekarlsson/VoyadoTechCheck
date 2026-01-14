using TechCheck.Web.Contrakts.Dto;
using TechCheck.WebAPI.Interface;
using TechCheck.WebAPI.Utils;

namespace TechCheck.WebAPI.Services
{
    public class SearchService(IEnumerable<ISearchcollector> sources) : ISearchService
    {
        public async Task<IEnumerable<SearchResult>> SearchAsync(string? searchInput)
        {
            if (string.IsNullOrEmpty(searchInput))
            {
                return [];
            }

            var words = StringHelper.CutUpString(searchInput);

            var tasks = sources.Select(s => s.GetNumberOfResultsAsync(words));
            var results = await Task.WhenAll(tasks);

            return [.. results];
        }
    }
}
