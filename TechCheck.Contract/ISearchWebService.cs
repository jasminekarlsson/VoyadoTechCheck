using TechCheck.Domain.Dto;

namespace TechCheck.Domain
{
    public interface ISearchWebService
    {
        Task<IList<SearchResult>> GetSearchResults(string searchInput);
    }
}
