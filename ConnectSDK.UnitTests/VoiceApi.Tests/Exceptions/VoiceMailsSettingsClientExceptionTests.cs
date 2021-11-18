namespace ConnectSDK.UnitTests.VoiceApi.Tests.Exceptions
{
    using System.Net;
    using System.Threading.Tasks;
    using ConnectSDK.UnitTests.Common;
    using Xunit;
    using Xunit.Abstractions;
    using ConnectSDK.Common.Exceptions;
    using ConnectSDK.Voice;
    using ConnectSDK.Voice.Models.V2.VoiceMailsSettings;

    public class VoiceMailsSettingsClientExceptionTests : TestBase
    {
        private readonly IVoiceClient voiceClient;

        public VoiceMailsSettingsClientExceptionTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var tokenProvider = new AuthMock();
            this.voiceClient = new VoiceClient(tokenProvider.GetToken);
        }

        [Theory]
        [InlineData(Format.Mp3, Custom.True)]
        internal async Task ShouldBeAbleToHandleInputParamExceptionWhenGetGreetingContent(Format format, Custom custom)
        {
            // Arrange
            var expectedMessage = new InputParametersException().Message;
            this.MockHttpResponse(this.voiceClient.VoiceMailsSettingsClient, "", HttpStatusCode.BadRequest);

            // Act
            try
            {
                var result = await this.voiceClient.VoiceMailsSettingsClient.GetGreetingContent(format, custom);

                // Assert
                Assert.True(false);
            }
            catch (InputParametersException e)
            {
                // Assert
                Assert.Equal(expectedMessage, e.Message);
            }
        }

        [Theory]
        [InlineData(Format.Mp3, Custom.True)]
        internal async Task ShouldBeAbleToHandleUnauthorizedExceptionWhenGetGreetingContent(Format format, Custom custom)
        {
            // Arrange
            var expectedMessage = new AuthorizeException().Message;
            this.MockHttpResponse(this.voiceClient.VoiceMailsSettingsClient, "", HttpStatusCode.Unauthorized);

            // Act
            try
            {
                var result = await this.voiceClient.VoiceMailsSettingsClient.GetGreetingContent(format, custom);

                // Assert
                Assert.True(false);
            }
            catch (AuthorizeException e)
            {
                // Assert
                Assert.Equal(expectedMessage, e.Message);
            }
        }

        [Theory]
        [InlineData(Format.Mp3, Custom.True)]
        internal async Task ShouldBeAbleToHandleNotFoundExceptionWhenGetGreetingContent(Format format, Custom custom)
        {
            // Arrange
            var expectedMessage = new ItemNotFoundException().Message;
            var rawResponse = $"\"message\": \"Resource not found\"";
            this.MockHttpResponse(this.voiceClient.VoiceMailsSettingsClient, rawResponse, HttpStatusCode.NotFound);

            // Act
            try
            {
                var result = await this.voiceClient.VoiceMailsSettingsClient.GetGreetingContent(format, custom);

                // Assert
                Assert.True(false);
            }
            catch (ItemNotFoundException e)
            {
                // Assert
                Assert.Equal(expectedMessage, e.Message);
            }
        }

        [Theory]
        [InlineData(Format.Mp3, Custom.True)]
        internal async Task ShouldBeAbleToHandleInternalFaultExceptionWhenGetGreetingContent(Format format, Custom custom)
        {
            // Arrange
            var expectedMessage = new InternalFaultException().Message;
            this.MockHttpResponse(this.voiceClient.VoiceMailsSettingsClient, "", HttpStatusCode.InternalServerError);

            // Act
            try
            {
                var result = await this.voiceClient.VoiceMailsSettingsClient.GetGreetingContent(format, custom);

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
