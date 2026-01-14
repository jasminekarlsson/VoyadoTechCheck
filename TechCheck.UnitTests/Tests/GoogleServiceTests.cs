using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Net;
using TechCheck.UnitTests.Mocks;
using TechCheck.WebAPI.Constants;
using TechCheck.WebAPI.Models.Configuration;
using TechCheck.WebAPI.Services;

namespace TechCheck.UnitTests.Tests;

public class GoogleServiceTests
{
    [Fact]
    public async Task GivenTwoSearchWords_ThenAddResults()
    {
        var json = """
                    {
                      "queries": {
                        "request": [
                          { "totalResults": "100" }
                        ]
                      }
                    }
                    """;

        var handler = new HttpMessageHandlerMock(_ => new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json)
        });

        var httpClient = new HttpClient(handler);
        var logger = Substitute.For<ILogger<GoogleService>>();
        var options = Options.Create(new GoogleApiOptions
        {
            ApiKey = "apiKey",
            GoogleSearchEngineId = "google"
        });

        var googleService = new GoogleService(httpClient, logger, options);
        var result = await googleService.GetNumberOfResultsAsync(new List<string> { "hello", "world" });

        Assert.Equal(Sitename.Google, result.SiteName);
        Assert.Equal(200, result.NumberOfResults);
    }

    [Fact]
    public async Task GivenNoResults_ThenReturnsZero()
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
        var logger = Substitute.For<ILogger<GoogleService>>();
        var options = Options.Create(new GoogleApiOptions
        {
            ApiKey = "apiKey",
            GoogleSearchEngineId = "google"
        });

        var googleService = new GoogleService(httpClient, logger, options);
        var result = await googleService.GetNumberOfResultsAsync(new List<string> { "test" });

        Assert.Equal(Sitename.Google, result.SiteName);
        Assert.Equal(0, result.NumberOfResults);
    }

    [Fact]
    public async Task GivenHttpResponseNotFound_ReturnNegativeValue()
    {
        var handler = new HttpMessageHandlerMock(_ => new HttpResponseMessage(HttpStatusCode.NotFound));

        var httpClient = new HttpClient(handler);
        var logger = Substitute.For<ILogger<GoogleService>>();
        var options = Options.Create(new GoogleApiOptions
        {
            ApiKey = "apiKey",
            GoogleSearchEngineId = "google"
        });

        var googleService = new GoogleService(httpClient, logger, options);

        var result = await googleService.GetNumberOfResultsAsync(new List<string> { "test" });

        Assert.Equal(-1, result.NumberOfResults);
    }
}
