namespace ConnectSDK.UnitTests.MeetingsApi.Tests
{
    using System.Text.Json;
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using ConnectSDK.UnitTests.Common;
    using Xunit;
    using Xunit.Abstractions;
    using ConnectSDK.Meetings;
    using ConnectSDK.Meetings.Models.V1;

    public class MeetingsClientTests : TestBase
    {
        // Subject of testing.
        private readonly IMeetingsClient meetingsClient;

        public MeetingsClientTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var tokenProvider = new AuthMock();
            this.meetingsClient = new MeetingsClient(tokenProvider.GetToken);
        }

        [Theory, AutoData]
        internal async Task ShouldBeAbleToStartMeeting(MeetingInfo response)
        {

            this.MockHttpResponse(this.meetingsClient, JsonSerializer.Serialize(response));

            // Act
            var result = await this.meetingsClient.StartNewMeetingWithDetails();

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.Equal(response.Code, result.Code);
           // Assert.Equal(response.Data, result.Data);
            Assert.Equal(response.Message, result.Message);
        }

        [Theory, AutoData]
        internal async Task ShouldBeAbleToGetMeetingDetailes(DetailedMeetingInfo response)
        {
            // Arrange
            this.MockHttpResponse(this.meetingsClient, JsonSerializer.Serialize(response));

            // Act
            var result = await this.meetingsClient.GetMeetingDetails(response.Data.MeetingCode);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.Equal(response.Data.MeetingCode, result.Data.MeetingCode);
        }


        [Theory, AutoData]
        internal async Task ShouldBeAbleToGetUserDetails(UserDetails response)
        {
            // Arrange
            this.MockHttpResponse(this.meetingsClient, JsonSerializer.Serialize(response));

            // Act
            var result = await this.meetingsClient.GetUserDetails();

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.NotNull(result);
            Assert.Equal(response.Name, result.Name);
        }
    }
}
