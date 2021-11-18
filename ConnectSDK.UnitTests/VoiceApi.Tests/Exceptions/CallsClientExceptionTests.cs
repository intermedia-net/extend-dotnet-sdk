namespace ConnectSDK.UnitTests.VoiceApi.Tests.Exceptions
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using ConnectSDK.UnitTests.Common;
    using Xunit;
    using Xunit.Abstractions;
    using ConnectSDK.Common.Exceptions;
    using ConnectSDK.Voice;
    using ConnectSDK.Voice.Models.V2.Calls;
    
    public class CallsClientExceptionTests : TestBase
    {
        // Subject of testing.
        private readonly IVoiceClient voiceClient;

        public CallsClientExceptionTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var tokenProvider = new AuthMock();
            this.voiceClient = new VoiceClient(tokenProvider.GetToken);
        }

        [Theory]
        [InlineData("99257135", "105")]
        internal async Task ShouldBeAbleToMakeCall(string deviceId, string phoneNumber)
        {
            // Arrange
            var callId = Guid.NewGuid().ToString("N");
            var commandId = Guid.NewGuid().ToString("N");
            var body = new MakeCallBody(deviceId, phoneNumber);
            var rawResponse = $"{{\"callId\":\"{callId}\",\"commandId\":\"{commandId}\"}}";
            this.MockHttpResponse(this.voiceClient.CallsClient, rawResponse);

            // Act
            var result = await this.voiceClient.CallsClient.MakeCall(body);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.NotEmpty(result.CallId);
            Assert.NotEmpty(result.CommandId);
        }

        [Theory]
        [InlineData("99257135", "105")]
        internal async Task ShouldBeAbleToHandleInputParamExceptionWhenMakeCall(string deviceId, string phoneNumber)
        {
            // Arrange
            var expectedMessage = new InputParametersException().Message;
            var body = new MakeCallBody(deviceId, phoneNumber);
            this.MockHttpResponse(this.voiceClient.CallsClient, "", HttpStatusCode.BadRequest);

            // Act
            try
            {
                var result = await this.voiceClient.CallsClient.MakeCall(body);

                //Assert
                Assert.True(false);
            }
            catch (InputParametersException e)
            {
                //Assert
                Assert.Equal(expectedMessage, e.Message);
            }
        }

        [Theory]
        [InlineData("99257135", "105")]
        internal async Task ShouldBeAbleToHandleUnauthorizedExceptionWhenMakeCall(string deviceId, string phoneNumber)
        {
            // Arrange
            var expectedMessage = new AuthorizeException().Message;
            var body = new MakeCallBody(deviceId, phoneNumber);
            this.MockHttpResponse(this.voiceClient.CallsClient, "", HttpStatusCode.Unauthorized);

            // Act
            try
            {
                var result = await this.voiceClient.CallsClient.MakeCall(body);

                //Assert
                Assert.True(false);
            }
            catch (AuthorizeException e)
            {
                //Assert
                Assert.Equal(expectedMessage, e.Message);
            }
        }

        [Theory]
        [InlineData("99257135", "105")]
        internal async Task ShouldBeAbleToHandleNotFoundExceptionWhenMakeCall(string deviceId, string phoneNumber)
        {
            // Arrange
            var expectedMessage = new ItemNotFoundException().Message;
            var body = new MakeCallBody(deviceId, phoneNumber);
            var rawResponse = $"\"message\": \"Resource not found\"";
            this.MockHttpResponse(this.voiceClient.CallsClient, rawResponse, HttpStatusCode.NotFound);

            // Act
            try
            {
                var result = await this.voiceClient.CallsClient.MakeCall(body);

                //Assert
                Assert.True(false);
            }
            catch (ItemNotFoundException e)
            {
                //Assert
                Assert.Equal(expectedMessage, e.Message);
            }
        }

        [Theory]
        [InlineData("99257135", "105")]
        internal async Task ShouldBeAbleToHandleInternalFaultExceptionWhenMakeCall(string deviceId, string phoneNumber)
        {
            // Arrange
            var expectedMessage = new InternalFaultException().Message;
            var body = new MakeCallBody(deviceId, phoneNumber);
            this.MockHttpResponse(this.voiceClient.CallsClient, "", HttpStatusCode.InternalServerError);

            // Act
            try
            {
                var result = await this.voiceClient.CallsClient.MakeCall(body);

                //Assert
                Assert.True(false);
            }
            catch (InternalFaultException e)
            {
                //Assert
                Assert.Equal(expectedMessage, e.Message);
            }
        }
    }
}
