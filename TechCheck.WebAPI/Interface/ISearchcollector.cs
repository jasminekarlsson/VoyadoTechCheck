using TechCheck.Domain.Dto;

namespace TechCheck.WebAPI.Interface
{
    public interface ISearchcollector
    {
        Task<SearchResult> GetNumberOfResultsAsync(List<string> searchWords);
    }
}
