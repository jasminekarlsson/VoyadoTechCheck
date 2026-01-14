using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TechCheck.UnitTest;

[TestClass]
public class GoogleServiceTests
{
    [Fact]
    public async Task GetNumberOfResultsAsync_ReturnsSumOfResults()
    {
        // Arrange
        var json = """
                    {
                      "queries": {
                        "request": [
                          { "totalResults": "100" }
                        ]
                      }
                    }
                    """;

        var handler = new HttpMessageHandlerMock(_ =>
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json)
            });

        var httpClient = new HttpClient(handler);
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["GoogleApiKey"] = "test-key",
                ["GoogleSearchEngineId"] = "test-cx"
            })
            .Build();

        var logger = Mock.Of<ILogger<GoogleSearchSource>>();
        var sut = new GoogleSearchSource(httpClient, config, logger);

        // Act
        var result = await sut.GetNumberOfResultsAsync(
            new List<string> { "dotnet", "blazor" });

        // Assert
        result.SiteName.Should().Be("Google");
        result.NumberOfResults.Should().Be(200);
    }

    [Fact]
    public async Task GetNumberOfResultsAsync_WhenNoResults_ReturnsZero()
    {
        var json = """
                    {
                      "queries": { "request": [] }
                    }
                    """;

        var handler = new HttpMessageHandlerMock(_ =>
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json)
            });

        var httpClient = new HttpClient(handler);
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["GoogleApiKey"] = "key",
                ["GoogleSearchEngineId"] = "cx"
            })
            .Build();

        var logger = Mock.Of<ILogger<GoogleSearchSource>>();
        var sut = new GoogleSearchSource(httpClient, config, logger);

        var result = await sut.GetNumberOfResultsAsync(new List<string> { "test" });

        result.NumberOfResults.Should().Be(0);
    }
}
