namespace ConnectSDK.UnitTests.VoiceApi.Tests
{
    using System;
    using System.Threading.Tasks;
    using ConnectSDK.UnitTests.Common;
    using ConnectSDK.Voice;
    using ConnectSDK.Voice.Models.V2.Calls;
    using Xunit;
    using Xunit.Abstractions;
    
    public class CallsClientTests : TestBase
    {
        // Subject of testing.
        private readonly IVoiceClient voiceClient;

        public CallsClientTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
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
        [InlineData("8fcad04f-88e0-4255-9225-b6ca98256b8f")]
        internal async Task ShouldBeAbleToTerminateCall(Guid callId)
        {
            // Arrange
            var commandId = Guid.NewGuid().ToString("N");
            var rawResponse = $"{{\"commandId\":\"{commandId}\"}}";
            this.MockHttpResponse(this.voiceClient.CallsClient, rawResponse);

            // Act
            var result = await this.voiceClient.CallsClient.TerminateCall(callId.ToString("N"));

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.NotEmpty(result.CommandId);
        }

        [Theory]
        [InlineData("8fcad04f-88e0-4255-9225-b6ca98256b8f")]
        internal async Task ShouldBeAbleToCancelCall(Guid callId)
        {
            // Arrange
            var commandId = Guid.NewGuid().ToString("N");
            var rawResponse = $"{{\"commandId\":\"{commandId}\"}}";
            this.MockHttpResponse(this.voiceClient.CallsClient, rawResponse);

            // Act
            var result = await this.voiceClient.CallsClient.CancelCall(callId.ToString("N"));

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.NotEmpty(result.CommandId);
        }

        [Theory]
        [InlineData("8fcad04f-88e0-4255-9225-b6ca98256b8f", "105")]
        internal async Task ShouldBeAbleToTransferCall(Guid callId, string phoneNumber)
        {
            // Arrange
            var commandId = Guid.NewGuid().ToString("N");
            var body = new TransferCallBody(phoneNumber);
            var rawResponse = $"{{\"commandId\":\"{commandId}\"}}";
            this.MockHttpResponse(this.voiceClient.CallsClient, rawResponse);

            // Act
            var result = await this.voiceClient.CallsClient.TransferCall(callId.ToString("N"), body);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.NotEmpty(result.CommandId);
        }

        [Theory]
        [InlineData("8fcad04f-88e0-4255-9225-b6ca98256b8f", "cab7e1dd-5744-481b-8df8-4a1cd71edf71")]
        internal async Task ShouldBeAbleToMergeCall(Guid callId, Guid mergeCallId)
        {
            // Arrange
            var commandId = Guid.NewGuid().ToString("N");
            var body = new MergeCallBody(mergeCallId.ToString("N"));
            var rawResponse = $"{{\"commandId\":\"{commandId}\"}}";
            this.MockHttpResponse(this.voiceClient.CallsClient, rawResponse);

            // Act
            var result = await this.voiceClient.CallsClient.MergeCall(callId.ToString("N"), body);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.NotEmpty(result.CommandId);
        }

        [Theory]
        [InlineData("54501816", "Polycom VVX301")]
        internal async Task ShouldBeAbleToGetDevices(string deviceId, string deviceName)
        {
            // Arrange
            var rawResponse = $"{{\"clickToCallDevices\":[{{\"id\":\"{deviceId}\",\"name\":\"{deviceName}\"}}]}}";
            this.MockHttpResponse(this.voiceClient.CallsClient, rawResponse);

            // Act
            var result = await this.voiceClient.CallsClient.GetDevices();

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            var device = Assert.Single(result.ClickToCallDevices);
            Assert.Equal(deviceId, device.Id);
            Assert.Equal(deviceName, device.Name);
        }
    }
}
