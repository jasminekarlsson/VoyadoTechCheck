using Microsoft.AspNetCore.Components;
using TechCheck.Web.Contrakts.Dto;

namespace TechCheck.Web.Client.Components
{
    public partial class ResultComponent : ComponentBase
    {
        [Parameter]
        public IList<SearchResult> Result { get; set; } = [];

        [Parameter]
        public string SearchWord { get; set; } = string.Empty;
    }
}
