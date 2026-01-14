using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Net;
using TechCheck.UnitTests.Mocks;
using TechCheck.WebAPI.Constants;
using TechCheck.WebAPI.Services;

namespace TechCheck.UnitTests.Tests;

public class BingServiceTests
{
    public ILogger<BingService> Logger { get; set; }
    public IConfiguration Config { get; set; }

    public BingServiceTests()
    {
        Logger = Substitute.For<ILogger<BingService>>();
        Config = new ConfigurationBuilder()
        .AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["BingApiKey"] = "key"
        }).Build();
    }
    [Fact]
    public async Task GivenOneSearchWords_ThenReturnResults()
    {
        var json = """
                    {
                      "search_information":
                    {
                    "total_results": 75
                    }
                    }
                    """;

        var handler = new HttpMessageHandlerMock(_ => new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json)
        });

        var httpClient = new HttpClient(handler);
        var bingService = new BingService(httpClient, Logger, Config);
        var result = await bingService.GetNumberOfResultsAsync(new List<string> { "hello" });

        Assert.Equal(Sitename.Bing, result.SiteName);
        Assert.Equal(75, result.NumberOfResults);
    }

    [Fact]
    public async Task GivenTwoSearchWords_ThenAddResults()
    {
        var json = """
                    {
                      "search_information":
                    {
                    "total_results": 25
                    }
                    }
                    """;

        var handler = new HttpMessageHandlerMock(_ => new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json)
        });

        var httpClient = new HttpClient(handler);
        var bingService = new BingService(httpClient, Logger, Config);
        var result = await bingService.GetNumberOfResultsAsync(new List<string> { "hello", "world" });

        Assert.Equal(Sitename.Bing, result.SiteName);
        Assert.Equal(50, result.NumberOfResults);
    }

    [Fact]
    public async Task GivenResponseDoesntMap_ThenReturnZero()
    {
        var json = """
                    {
                      "queries": { "request": [] }
                    }
                    """;

        var handler = new HttpMessageHandlerMock(_ => new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json)
        });
        var httpClient = new HttpClient(handler);

        var bingService = new BingService(httpClient, Logger, Config);
        var result = await bingService.GetNumberOfResultsAsync(new List<string> { "test" });

        Assert.Equal(0, result.NumberOfResults);
    }

    [Fact]
    public async Task GivenHttpResponseNotFoundThenReturnNegativeValue()
    {
        var handler = new HttpMessageHandlerMock(_ => new HttpResponseMessage(HttpStatusCode.NotFound));
        var httpClient = new HttpClient(handler);

        var bingService = new BingService(httpClient, Logger, Config);
        var result = await bingService.GetNumberOfResultsAsync(new List<string> { "test" });

        Assert.Equal(-1, result.NumberOfResults);
    }
}
