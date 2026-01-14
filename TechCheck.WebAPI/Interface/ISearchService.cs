using TechCheck.Domain.Dto;

namespace TechCheck.WebAPI.Interface
{
    public interface ISearchService
    {
        Task<IEnumerable<SearchResult>> SearchAsync(string? searchInput);
    }
}
