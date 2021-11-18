#nullable enable
namespace ConnectSDK.UnitTests.Common
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Moq.Protected;
    using Xunit.Abstractions;

    public abstract class TestBase : IDisposable
    {
        private const BindingFlags Private = BindingFlags.Instance | BindingFlags.NonPublic;

        private Mock<HttpMessageHandler>? httpMessageHandlerMock;

        private bool objectDisposed;

        protected TestBase(ITestOutputHelper testOutputHelper)
        {
            this.TestOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
            this.LoggerFactory = new LoggerFactory(
                new[]
                {
                    new XUnitLoggingProvider(this.TestOutputHelper)
                });
            this.Logger = this.LoggerFactory.CreateLogger(this.GetType());
        }

        protected ITestOutputHelper TestOutputHelper { get; }

        protected ILogger Logger { get; }

        protected ILoggerFactory LoggerFactory { get; }

        protected HttpRequestMessage? LatestHttpRequestFromMock
        {
            get
            {
                var invocation = this.httpMessageHandlerMock?.Invocations.LastOrDefault();
                if (invocation?.Arguments[0] is HttpRequestMessage httpRequestMessage)
                {
                    return httpRequestMessage;
                }

                return null;
            }
        }

        public void Dispose()
        {
            if (!this.objectDisposed)
            {
                this.Dispose(true);
                this.objectDisposed = true;

                GC.SuppressFinalize(this);
            }
        }

        ~TestBase()
        {
            this.Dispose(false);
        }

        // Consider responseString is raw UTF8 encoded string returned in body.
        protected void MockHttpResponse(object targetClient, string responseString, HttpStatusCode expectedCode = HttpStatusCode.OK)
        {
            var responseBytes = Encoding.UTF8.GetBytes(responseString);
            this.MockHttpResponse(targetClient, responseBytes, expectedCode);
        }

        protected void MockHttpResponse(object targetClient, byte[] responseContent, HttpStatusCode expectedCode = HttpStatusCode.OK)
        {
            var mock = this.CreateMockForOverridingAnyRequest(responseContent, expectedCode);
            this.MockHttpResponse(targetClient, mock);
        }

        protected void MockHttpResponse(object targetClient, Mock<HttpMessageHandler> handler)
        {
            this.httpMessageHandlerMock = handler ?? throw new ArgumentNullException(nameof(handler));

            var requestFactory = this.GetRequestFactory(targetClient);

            var mockedHttpClientObject = this.CreateHttpClientMockObject(requestFactory);

            this.TryReplaceHttpClientInRequestFactory(requestFactory, mockedHttpClientObject);
        }

        private void TryReplaceHttpClientInRequestFactory(object targetRequestFactory, HttpClient? mockedHttpClientObject)
        {
            try
            {
                targetRequestFactory
                    .GetType()
                    .GetField("httpClient", Private)!
                    .SetValue(targetRequestFactory, mockedHttpClientObject);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to mock HttpClient in target client's request factory", e);
            }
        }

        private HttpClient CreateHttpClientMockObject(object requestFactory)
        {
            var mockedHttpClientObject = (HttpClient?)requestFactory
                .GetType()
                .GetMethod("CreateHttpClient", Private)?
                .Invoke(requestFactory, new object[] { this.httpMessageHandlerMock!.Object });

            if (mockedHttpClientObject is null)
            {
                throw new MissingFieldException("Unable to reflect CreateHttpClient on RequestFactory");
            }

            return mockedHttpClientObject;
        }

        private Mock<HttpMessageHandler> CreateMockForOverridingAnyRequest(byte[] responseContent, HttpStatusCode expectedCode)
        {
            var mock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(
                    new HttpResponseMessage
                    {
                        StatusCode = expectedCode,
                        Content = new StreamContent(new MemoryStream(responseContent))
                    })
                .Verifiable();
            return mock;
        }

        private object GetRequestFactory(object targetClient)
        {
            var requestFactoryField = targetClient
                .GetType()
                .GetField("requestFactory", Private);

            if (requestFactoryField is null)
            {
                throw new MissingFieldException("Request factory was not found in target client class");
            }

            var requestFactory = requestFactoryField.GetValue(targetClient);
            if (requestFactory is null)
            {
                throw new ArgumentNullException(nameof(requestFactory), "Unable to get requestFactory from target client class");
            }

            return requestFactory;
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
