using TechCheck.Web.Contrakts.Dto;

namespace TechCheck.WebAPI.Interface
{
    public interface ISearchcollector
    {
        Task<SearchResult> GetNumberOfResultsAsync(List<string> searchWords);
    }
}
