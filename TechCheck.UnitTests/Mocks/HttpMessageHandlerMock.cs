namespace TechCheck.UnitTests.Mocks
{
    public class HttpMessageHandlerMock(Func<HttpRequestMessage, HttpResponseMessage> handler) : HttpMessageHandler

    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(handler(request));
        }
    }
}
