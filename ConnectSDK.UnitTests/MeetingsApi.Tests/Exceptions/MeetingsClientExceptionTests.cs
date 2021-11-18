namespace ConnectSDK.UnitTests.MeetingsApi.Tests.Exceptions
{
    using System.Net;
    using System.Threading.Tasks;
    using ConnectSDK.UnitTests.Common;
    using Xunit;
    using Xunit.Abstractions;
    using ConnectSDK.Meetings;
    using ConnectSDK.Common.Exceptions;

    public class MeetingsClientExceptionTests : TestBase
    {
        // Subject of testing.
        private readonly IMeetingsClient meetingsClient;

        public MeetingsClientExceptionTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var tokenProvider = new AuthMock();
            this.meetingsClient = new MeetingsClient(tokenProvider.GetToken);
        }

        [Fact]
        internal async Task ShouldBeAbleToHandleInputParamExceptionWhenStartMeeting()
        {
            // Arrange
            var expectedMessage = new InputParametersException().Message;
            this.MockHttpResponse(this.meetingsClient, "", HttpStatusCode.BadRequest);

            // Act
            try
            {
                var result = await this.meetingsClient.StartNewMeetingWithDetails();

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
        internal async Task ShouldBeAbleToHandleUnauthorizedExceptionWhenStartMeeting()
        {
            // Arrange
            var expectedMessage = new AuthorizeException().Message;
            this.MockHttpResponse(this.meetingsClient, "", HttpStatusCode.Unauthorized);

            // Act
            try
            {
                var result = await this.meetingsClient.StartNewMeetingWithDetails();

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
        internal async Task ShouldBeAbleToHandleNotFoundExceptionWhenStartMeeting()
        {
            // Arrange
            var expectedMessage = new ItemNotFoundException().Message;
            var rawResponse = $"\"message\": \"Resource not found\"";
            this.MockHttpResponse(this.meetingsClient, rawResponse, HttpStatusCode.NotFound);

            // Act
            try
            {
                var result = await this.meetingsClient.StartNewMeetingWithDetails();

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
        internal async Task ShouldBeAbleToHandleInternalFaultExceptionWhenStartMeeting()
        {
            // Arrange
            var expectedMessage = new InternalFaultException().Message;
            this.MockHttpResponse(this.meetingsClient, "", HttpStatusCode.InternalServerError);

            // Act
            try
            {
                var result = await this.meetingsClient.StartNewMeetingWithDetails();

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
