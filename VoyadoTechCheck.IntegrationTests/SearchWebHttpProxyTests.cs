using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using TechCheck.Web.Proxy;

namespace VoyadoTechCheck.IntegrationTests;

public class SearchWebHttpProxyTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public SearchWebHttpProxyTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetSearchResults_ReturnsResultsFromApi()
    {
        // Arrange
        var proxy = new WebHttpProxy(_client);
        var searchInput = "hello world";

        // Act
        var results = await proxy.GetSearchResults(searchInput);

        // Assert
        Assert.NotNull(results);
        Assert.NotEmpty(results);
    }
}
