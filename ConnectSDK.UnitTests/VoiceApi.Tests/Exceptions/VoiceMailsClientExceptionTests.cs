namespace ConnectSDK.UnitTests.VoiceApi.Tests.Exceptions
{
    using System.Net;
    using System.Threading.Tasks;
    using ConnectSDK.UnitTests.Common;
    using ConnectSDK.Voice;
    using Xunit;
    using Xunit.Abstractions;
    using ConnectSDK.Common.Exceptions;

    public class VoiceMailsClientExceptionTests : TestBase
    {
        private readonly IVoiceClient voiceClient;

        public VoiceMailsClientExceptionTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var tokenProvider = new AuthMock();
            this.voiceClient = new VoiceClient(tokenProvider.GetToken);
        }

        [Fact]
        internal async Task ShouldBeAbleToHandleInputParamExceptionWhenGetVoiceMailsTotal()
        {
            // Arrange
            var expectedMessage = new InputParametersException().Message;
            this.MockHttpResponse(this.voiceClient.VoiceMailsClient, "", HttpStatusCode.BadRequest);

            // Act
            try
            {
                var result = await this.voiceClient.VoiceMailsClient.GetUserVoicemailsTotal();

                // Assert
                Assert.True(false);
            }
            catch (InputParametersException e)
            {
                // Assert
                Assert.Equal(expectedMessage, e.Message);
            }
        }

        [Fact]
        internal async Task ShouldBeAbleToHandleUnauthorizedExceptionWhenGetVoiceMailsTotal()
        {
            // Arrange
            var expectedMessage = new AuthorizeException().Message;
            this.MockHttpResponse(this.voiceClient.VoiceMailsClient, "", HttpStatusCode.Unauthorized);

            // Act
            try
            {
                var result = await this.voiceClient.VoiceMailsClient.GetUserVoicemailsTotal();

                // Assert
                Assert.True(false);
            }
            catch (AuthorizeException e)
            {
                // Assert
                Assert.Equal(expectedMessage, e.Message);
            }
        }

        [Fact]
        internal async Task ShouldBeAbleToHandleNotFoundExceptionWhenGetVoiceMailsTotal()
        {
            // Arrange
            var expectedMessage = new ItemNotFoundException().Message;
            var rawResponse = $"\"message\": \"Resource not found\"";
            this.MockHttpResponse(this.voiceClient.VoiceMailsClient, rawResponse, HttpStatusCode.NotFound);

            // Act
            try
            {
                var result = await this.voiceClient.VoiceMailsClient.GetUserVoicemailsTotal();

                // Assert
                Assert.True(false);
            }
            catch (ItemNotFoundException e)
            {
                // Assert
                Assert.Equal(expectedMessage, e.Message);
            }
        }

        [Fact]
        internal async Task ShouldBeAbleToHandleInternalFaultExceptionWhenGetVoiceMailsTotal()
        {
            // Arrange
            var expectedMessage = new InternalFaultException().Message;
            this.MockHttpResponse(this.voiceClient.VoiceMailsClient, "", HttpStatusCode.InternalServerError);

            // Act
            try
            {
                var result = await this.voiceClient.VoiceMailsClient.GetUserVoicemailsTotal();

                // Assert
                Assert.True(false);
            }
            catch (InternalFaultException e)
            {
                // Assert
                Assert.Equal(expectedMessage, e.Message);
            }
        }
    }
}
