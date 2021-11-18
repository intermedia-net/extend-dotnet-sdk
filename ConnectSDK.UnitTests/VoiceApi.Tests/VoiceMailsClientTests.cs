namespace ConnectSDK.UnitTests.VoiceApi.Tests
{
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using ConnectSDK.UnitTests.Common;
    using Xunit;
    using Xunit.Abstractions;
    using ConnectSDK.Voice;
    using ConnectSDK.Voice.Models.V2.VoiceMails;
    using ConnectSDK.Voice.Models.V2.VoiceMailsSettings;

    public class VoiceMailsClientTests : TestBase
    {
        private readonly IVoiceClient voiceClient;

        public VoiceMailsClientTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var tokenProvider = new AuthMock();
            this.voiceClient = new VoiceClient(tokenProvider.GetToken);
        }

        [Theory]
        [InlineData(13, "+11111111111", "Mr.prodUser@test.net", "", 300, "", true)]
        internal async Task ShouldBeAbleToGetVoiceMails(
            int id,
            string phoneNumber,
            string displayName,
            string status,
            int duration,
            string whenCreated,
            bool hasText)
        {
            // Arrange
            var voiceMails = new VoiceMails();
            VoiceMail[] voiceMail =
            {
                new VoiceMail
                {
                    Id = id,
                    Sender = new Sender
                        { PhoneNumber = phoneNumber, DisplayName = displayName },
                    Status = status,
                    Duration = duration,
                    WhenCreated = whenCreated,
                    HasText = hasText
                }
            };

            voiceMails.Records = voiceMail;

            var rawResponse = JsonSerializer.Serialize(voiceMails);
            this.MockHttpResponse(this.voiceClient.VoiceMailsClient, rawResponse);

            // Act
            var result = await this.voiceClient.VoiceMailsClient.GetUserVoiceMails();

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.Equal(voiceMails.Records.Length, result.Records.Length);
            Assert.Equal(voiceMails.Records[0].Id, result.Records[0].Id);
        }

        [Theory]
        [InlineData(14)]
        internal async Task ShouldBeAbleToGetVoiceMailsTotal(int total)
        {
            // Arrange
            var rawResponse = $"{{\"total\":\"{total}\"}}";
            this.MockHttpResponse(this.voiceClient.VoiceMailsClient, rawResponse);

            // Act
            var result = await this.voiceClient.VoiceMailsClient.GetUserVoicemailsTotal();

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.Equal(total, result.Total);
        }

        [Theory, AutoData]
        internal async Task ShouldBeAbleToUpdateVoicemailRecordsStatus(SelectedVoiceMailsBody body)
        {
            // Arrange
            var rawResponse = "{}";

            this.MockHttpResponse(this.voiceClient.VoiceMailsClient, rawResponse);

            // Act
            await this.voiceClient.VoiceMailsClient.UpdateVoicemailRecordsStatus(body);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
        }

        [Theory]
        [InlineData("read", new[] { 580222, 222113 })]
        internal async Task ShouldBeAbleToUpdateSelectedVoicemailRecordsStatus(string status, int[] ids)
        {
            // Arrange
            var body = new SelectedVoiceMailsBody
                { Status = status, Ids = ids };
            var rawResponse = "{}";

            this.MockHttpResponse(this.voiceClient.VoiceMailsClient, rawResponse);

            // Act
            await this.voiceClient.VoiceMailsClient.UpdateSelectedVoicemailRecordsStatus(body);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
        }

        [Theory]
        [InlineData(13, "+11111111111", "Mr.prodUser@test.net", "", 300, "", true)]
        internal async Task ShouldBeAbleToGetVoiceMailsRecord(
            int id,
            string phoneNumber,
            string displayName,
            string status,
            int duration,
            string whenCreated,
            bool hasText)
        {
            // Arrange
            var voiceMail = new VoiceMail
            {
                Id = id,
                Sender = new Sender
                    { PhoneNumber = phoneNumber, DisplayName = displayName },
                Status = status,
                Duration = duration,
                WhenCreated = whenCreated,
                HasText = hasText
            };

            var rawResponse = JsonSerializer.Serialize(voiceMail);
            this.MockHttpResponse(this.voiceClient.VoiceMailsClient, rawResponse);

            // Act
            var result = await this.voiceClient.VoiceMailsClient.GetVoicemailRecord(id.ToString());

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.Equal(voiceMail.Id, result.Id);
        }

        [Theory]
        [InlineData("13111", Format.Mp3)]
        internal async Task ShouldBeAbleToGetVoiceMailsContent(string id, Format format)
        {
            // Arrange
            var rawResponse = new byte[] { };

            this.MockHttpResponse(this.voiceClient.VoiceMailsClient, rawResponse);

            // Act
            var result = await this.voiceClient.VoiceMailsClient.GetVoicemailContent(id, format);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.Equal(rawResponse.Length, result.Length);
        }

        [Fact]
        internal async Task ShouldBeAbleToGetVoiceMailsTranscript()
        {
            // Arrange
            var id = Guid.NewGuid().ToString("N");
            var response = new Transcript
                { Text = "text Message" };
            var rawResponse = JsonSerializer.Serialize(response);

            this.MockHttpResponse(this.voiceClient.VoiceMailsClient, rawResponse);

            // Act
            var result = await this.voiceClient.VoiceMailsClient.GetVoicemailTranscript(id);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.Equal(response.Text, result.Text);
        }

        [Theory]
        [InlineData(Status.Read)]
        internal async Task ShouldBeAbleToDeleteVoicemailRecords(Status status)
        {
            // Arrange
            var rawResponse = JsonSerializer.Serialize(Guid.NewGuid().ToString("N"));

            this.MockHttpResponse(this.voiceClient.VoiceMailsClient, rawResponse);

            // Act
            await this.voiceClient.VoiceMailsClient.DeleteVoicemailRecords(status);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
        }

        [Theory]
        [InlineData(new[] { 580222, 222113 })]
        internal async Task ShouldBeAbleToDeleteSelectedVoicemailRecords(int[] ids)
        {
            // Arrange
            var body = new SelectedIdsBody
                { Ids = ids };
            var rawResponse = JsonSerializer.Serialize(body);
            this.MockHttpResponse(this.voiceClient.VoiceMailsClient, rawResponse);

            // Act
            await this.voiceClient.VoiceMailsClient.DeleteSelectedVoicemailRecords(body);
            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
        }
    }
}
