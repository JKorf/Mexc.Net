using System.Net;
using System.Text;
using CryptoExchange.Net.Interfaces;
using Mexc.Net.Clients;
using Mexc.Net.Interfaces.Clients;
using Mexc.Net.Objects.Options;
using Moq;

namespace Mexc.Net.UnitTests.Helpers
{
    public class TestHelpers
    {
        public static IMexcRestClient CreateClient(Action<MexcRestOptions> options = null)
        {
            IMexcRestClient client;
            client = options != null ? new MexcRestClient(options) : new MexcRestClient();
            client.SpotApi.RequestFactory = Mock.Of<IRequestFactory>();
            return client;
        }

        public static void SetResponse(MexcRestClient client, string responseData, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var expectedBytes = Encoding.UTF8.GetBytes(responseData);
            var responseStream = new MemoryStream();
            responseStream.Write(expectedBytes, 0, expectedBytes.Length);
            responseStream.Seek(0, SeekOrigin.Begin);

            var response = new Mock<IResponse>();
            response.Setup(c => c.StatusCode).Returns(statusCode);
            response.Setup(c => c.IsSuccessStatusCode).Returns(statusCode == HttpStatusCode.OK);
            response.Setup(c => c.GetResponseStreamAsync()).Returns(Task.FromResult((Stream)responseStream));

            var request = new Mock<IRequest>();
            request.Setup(c => c.Uri).Returns(new Uri("http://www.test.com"));
            request.Setup(c => c.GetHeaders()).Returns([]);
            request.Setup(c => c.GetResponseAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(response.Object));

            var factory = Mock.Get(client.SpotApi.RequestFactory);
            factory.Setup(c => c.Create(It.IsAny<Version>(), It.IsAny<HttpMethod>(), It.IsAny<Uri>(), It.IsAny<int>()))
                .Returns(request.Object);
        }
    }
}
