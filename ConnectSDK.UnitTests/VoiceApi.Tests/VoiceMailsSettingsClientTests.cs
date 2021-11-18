namespace ConnectSDK.UnitTests.VoiceApi.Tests
{
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;
    using ConnectSDK.UnitTests.Common;
    using Xunit;
    using Xunit.Abstractions;
    using ConnectSDK.Voice;
    using ConnectSDK.Voice.Models.V2.VoiceMailsSettings;

    public class VoiceMailsSettingsClientTests : TestBase
    {
        private readonly IVoiceClient voiceClient;

        public VoiceMailsSettingsClientTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var tokenProvider = new AuthMock();
            this.voiceClient = new VoiceClient(tokenProvider.GetToken);
        }

        [Theory]
        [InlineData(Format.Mp3, Custom.True)]
        internal async Task ShouldBeAbleToGetGreetingContent(Format format, Custom custom)
        {
            // Arrange
            var rawResponse = new byte[] { };

            this.MockHttpResponse(this.voiceClient.VoiceMailsSettingsClient, rawResponse);

            // Act
            var result = await this.voiceClient.VoiceMailsSettingsClient.GetGreetingContent(format, custom);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.Equal(rawResponse.Length, result.Length);
        }

        [Theory]
        [InlineData(new byte[] { })]
        internal async Task ShouldBeAbleToUploadGreetingContent(byte[] byteArray)
        {
            var rawResponse = "";
            this.MockHttpResponse(this.voiceClient.VoiceMailsSettingsClient, rawResponse);

            // Act
            await this.voiceClient.VoiceMailsSettingsClient.UploadGreetingContent(byteArray);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
        }

        [Fact]
        internal async Task ShouldBeAbleToResetGreetingContent()
        {
            // Arrange
            var rawResponse = "";
            this.MockHttpResponse(this.voiceClient.VoiceMailsSettingsClient, rawResponse);

            // Act
            await this.voiceClient.VoiceMailsSettingsClient.ResetGreetingContent();

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
        }

        [Theory]
        [InlineData("5400")]
        internal async Task ShouldBeAbleToGetUserSettings(string pin)
        {
            // Arrange
            var rawResponse = new UserSettings
                { Pin = pin };
            this.MockHttpResponse(this.voiceClient.VoiceMailsSettingsClient, JsonSerializer.Serialize(rawResponse));

            // Act
            var result = await this.voiceClient.VoiceMailsSettingsClient.GetUserSettings();

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);

            Assert.Equal(rawResponse.Pin, result.Pin);
        }

        [Theory]
        [InlineData("5401")]
        internal async Task ShouldBeAbleToUpdateUserSettings(string pin)
        {
            // Arrange
            var rawResponse = JsonSerializer.Serialize(Guid.NewGuid().ToString("N"));
            this.MockHttpResponse(this.voiceClient.VoiceMailsSettingsClient, JsonSerializer.Serialize(rawResponse));

            var userSettings = new UserSettings
                { Pin = pin };
            
            // Act
            await this.voiceClient.VoiceMailsSettingsClient.UpdateUserSettings(userSettings);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
        }

        [Fact]
        internal async Task ShouldBeAbleToGetVoiceMailUsage()
        {
            // Arrange
            var rawResponse = new VoiceMailUsage
                { SpaceUsedPercentage = 0 };

            this.MockHttpResponse(this.voiceClient.VoiceMailsSettingsClient, JsonSerializer.Serialize(rawResponse));

            // Act
            var result = await this.voiceClient.VoiceMailsSettingsClient.GetVoicemailUsage();

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.Equal(rawResponse.SpaceUsedPercentage, result.SpaceUsedPercentage);
        }
    }
}
