using Microsoft.AspNetCore.Mvc;
using TechCheck.Domain.Dto;
using TechCheck.WebAPI.Interface;

namespace TechCheck.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController(ISearchService searchService) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<SearchResult>> GetData([FromQuery]string? searchInput)
        {
            return await searchService.SearchAsync(searchInput);
        }
    }
}
