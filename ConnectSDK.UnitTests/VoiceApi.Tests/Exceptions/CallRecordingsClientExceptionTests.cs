namespace ConnectSDK.UnitTests.VoiceApi.Tests.Exceptions
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using ConnectSDK.Common.Exceptions;
    using ConnectSDK.UnitTests.Common;
    using ConnectSDK.Voice;
    using Xunit;
    using Xunit.Abstractions;
    
    public class CallRecordingsClientExceptionTests : TestBase
    {
        // Subject of testing.
        private readonly IVoiceClient voiceClient;

        public CallRecordingsClientExceptionTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var tokenProvider = new AuthMock();
            this.voiceClient = new VoiceClient(tokenProvider.GetToken);
        }

        [Fact]
        internal async Task ShouldBeAbleToHandleInputParamExceptionWhenGetCallRecording()
        {
            // Arrange
            var expectedMessage = new InputParametersException().Message;
            var uuid = Guid.NewGuid().ToString("N");
            this.MockHttpResponse(this.voiceClient.CallRecordingsClient, "", HttpStatusCode.BadRequest);

            // Act
            try
            {
                var result = await this.voiceClient.CallRecordingsClient.GetCallRecordings(uuid);

                //Assert
                Assert.True(false);
            }
            catch (InputParametersException e)
            {
                //Assert
                Assert.Equal(expectedMessage, e.Message);
            }
        }

        [Fact]
        internal async Task ShouldBeAbleToHandleUnauthorizedExceptionWhenGetCallRecording()
        {
            // Arrange
            var expectedMessage = new AuthorizeException().Message;
            var uuid = Guid.NewGuid().ToString("N");
            this.MockHttpResponse(this.voiceClient.CallRecordingsClient, "", HttpStatusCode.Unauthorized);

            // Act
            try
            {
                var result = await this.voiceClient.CallRecordingsClient.GetCallRecordings(uuid);

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
        internal async Task ShouldBeAbleToHandleNotFoundExceptionWhenGetCallRecording()
        {
            // Arrange
            var expectedMessage = new ItemNotFoundException().Message;
            var uuid = Guid.NewGuid().ToString("N");
            var rawResponse = $"\"message\": \"Resource not found\"";
            this.MockHttpResponse(this.voiceClient.CallRecordingsClient, rawResponse, HttpStatusCode.NotFound);

            // Act
            try
            {
                var result = await this.voiceClient.CallRecordingsClient.GetCallRecordings(uuid);

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
        internal async Task ShouldBeAbleToHandleInternalFaultExceptionWhenGetCallRecording()
        {
            // Arrange
            var expectedMessage = new InternalFaultException().Message;
            var uuid = Guid.NewGuid().ToString("N");
            this.MockHttpResponse(this.voiceClient.CallRecordingsClient, "", HttpStatusCode.InternalServerError);

            // Act
            try
            {
                var result = await this.voiceClient.CallRecordingsClient.GetCallRecordings(uuid);

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
