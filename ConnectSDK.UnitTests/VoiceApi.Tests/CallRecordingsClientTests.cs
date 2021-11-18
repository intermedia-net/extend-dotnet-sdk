namespace ConnectSDK.UnitTests.VoiceApi.Tests
{
    using System;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using ConnectSDK.UnitTests.Common;
    using ConnectSDK.Voice;
    using ConnectSDK.Voice.Models.V2.CallRecordings;
    using ConnectSDK.Voice.Models.V2.Calls;
    using Xunit;
    using Xunit.Abstractions;

    public class CallRecordingsClientTests : TestBase
    {
        // Subject of testing.
        private readonly IVoiceClient voiceClient;

        public CallRecordingsClientTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var tokenProvider = new AuthMock();
            this.voiceClient = new VoiceClient(tokenProvider.GetToken);
        }

        [Theory]
        [InlineData(1526628, "MrProdTest2", "1612/38912/cr_2021_Sep_16_12_12_58_21891.mp3", 200, "2021-09-16T19:13:14+00:00", "a7501bd0-22bf-4ebc-b974-01dadf4309de", "outgoing",false)]
        internal async Task ShouldBeAbleToGetCallRecording(int callRecordId, string callerName, string fileName, int duration, string whenCreated, string callId, string direction, bool wasPaused)
        {
            // Arrange
            var uuid = Guid.NewGuid().ToString("N");

            var response = new CallRecordings
            {
                Records = new[]
                {
                    new CallRecording
                    {
                        Id = callRecordId,
                        Caller = new Caller { PhoneNumber = "105", DisplayName = "MrProdTest2" },
                        Direction = direction,
                        Duration = duration,
                        CallId = callId,
                        FileName = fileName,
                        WasPaused = wasPaused,
                        WhenCreated = whenCreated
                    }
                }
            };
                
            var rawResponse = JsonSerializer.Serialize(response);
            
            this.MockHttpResponse(this.voiceClient.CallRecordingsClient, rawResponse);

            // Act
            var result = await this.voiceClient.CallRecordingsClient.GetCallRecordings(uuid);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            var callRecord = Assert.Single(result.Records);
            Assert.Equal(callRecordId, callRecord.Id);
            Assert.Equal(callerName, callRecord.Caller.DisplayName);
            Assert.Equal(fileName, callRecord.FileName);
            Assert.Equal(duration, callRecord.Duration);
            Assert.Equal(callId, callRecord.CallId);
            Assert.Equal(whenCreated, callRecord.WhenCreated);
            Assert.Equal(direction, callRecord.Direction);
            Assert.Equal(wasPaused, callRecord.WasPaused);
        }

        [Theory]
        [InlineData(1526628, new byte[] { 0b110101, 0b100101, 0b1100111 })]
        internal async Task ShouldBeAbleToGetCallRecordingContent(int callRecordId, byte[] file)
        {
            // Arrange
            var uuid = Guid.NewGuid().ToString("N");
            var rawResponse = Encoding.Default.GetString(file);
            this.MockHttpResponse(this.voiceClient.CallRecordingsClient, rawResponse);

            // Act
            var result = await this.voiceClient.CallRecordingsClient.GetCallRecordingContent(uuid, callRecordId);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.Equal(file, result);
        }

        [Theory]
        [InlineData(new[] { 1526628, 1526618 }, new byte[] { 0b110101, 0b100101, 0b1100111, 0b101101, 0b110001, 0b1001111 })]
        internal async Task ShouldBeAbleToGetCallRecordingArchive(int[] callRecordIds, byte[] archive)
        {
            // Arrange
            var uuid = Guid.NewGuid().ToString("N");
            var rawResponse = Encoding.Default.GetString(archive);
            this.MockHttpResponse(this.voiceClient.CallRecordingsClient, rawResponse);

            // Act
            var result = await this.voiceClient.CallRecordingsClient.GetCallRecordingArchive(uuid, callRecordIds);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.Equal(archive, result);
        }
    }
}
