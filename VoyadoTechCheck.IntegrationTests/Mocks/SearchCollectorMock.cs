using TechCheck.Domain.Dto;
using TechCheck.WebAPI.Interface;

namespace VoyadoTechCheck.IntegrationTests.Mocks
{
    public class SearchCollectorMock : ISearchcollector
    {
        public Task<SearchResult> GetNumberOfResultsAsync(List<string> searchWords)
        {
            return Task.FromResult(new SearchResult
            {
                SiteName = "SearchTest",
                NumberOfResults = 123
            });
        }
    }
}
