using Microsoft.AspNetCore.Components;
using TechCheck.Domain;
using TechCheck.Domain.Dto;

namespace TechCheck.Web.Client.Pages
{
    public partial class SearchPage(ILogger<SearchPage> logger) : ComponentBase
    {

        [Inject]
        private ISearchWebService DatacollectorService { get; set; } = default!;

        public IList<SearchResult> Result { get; set; } = [];

        protected string SearchParameter { get; set; } = string.Empty;

        protected bool isLoading { get; set; }

        public async Task OnSearch(string parameter)
        {
            isLoading = true;

            logger.LogInformation("Search initiated with parameter: {Parameter}", parameter);
            SearchParameter = parameter;
            try
            {
                Result = await DatacollectorService.GetSearchResults(parameter);
            }
            finally
            {

                isLoading = false;
            }
        }
    }
}
