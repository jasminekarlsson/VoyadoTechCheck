using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using TechCheck.WebAPI.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using VoyadoTechCheck.IntegrationTests.Mocks;

namespace VoyadoTechCheck.IntegrationTests.Factories
{
    public class TestWebApplicationFactory
    : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<ISearchcollector>();

                services.AddScoped<ISearchcollector, SearchCollectorMock>();
            });
        }
    }
}
