using Microsoft.AspNetCore.Components;

namespace TechCheck.Web.Client.Components
{
    public partial class SearchComponent : ComponentBase
    {
        [Parameter]
        public EventCallback<string> OnSearch { get; set; }

        public string SearchParameter { get; set; } = string.Empty;

        public async Task Search()
        {
            await OnSearch.InvokeAsync(SearchParameter);
        }
    }
}
