using TechCheck.Web.Contrakts.Dto;

namespace TechCheck.Web.Contrakts
{
    public interface ISearchWebService
    {
        Task<IList<SearchResult>> GetSearchResults(string searchInput);
    }
}
