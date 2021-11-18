namespace ConnectSDK.UnitTests.AnalyticsApi.Tests.Exceptions
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Common;
    using Xunit;
    using Xunit.Abstractions;
    using Analytics;
    using ConnectSDK.Common.Exceptions;

    public class AnalyticsClientExceptionTests : TestBase
    {
        // Subject of testing.
        private readonly IAnalyticsClient analyticsClient;

        public AnalyticsClientExceptionTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var tokenProvider = new AuthMock();
            this.analyticsClient = new AnalyticsClient(tokenProvider.GetToken);
        }

        [Theory]
        [InlineData("2021-09-24", "2021-09-24")]
        internal async Task ShouldBeAbleToHandleInputParamExceptionWhenGetDetailedCalls(string dateFrom, string dateTo)
        {
            // Arrange
            var expectedMessage = new InputParametersException().Message;
            this.MockHttpResponse(this.analyticsClient, "", HttpStatusCode.BadRequest);

            // Act
            try
            {
                var result = await this.analyticsClient.GetDetailedCalls(DateTime.Parse(dateFrom), DateTime.Parse(dateTo));

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
        [InlineData("2021-09-24T09:40:00.000Z", "2021-09-24T11:30:00.000Z")]
        internal async Task ShouldBeAbleToHandleUnauthorizedExceptionWhenGetDetailedCalls(string dateFrom, string dateTo)
        {
            // Arrange
            var expectedMessage = new AuthorizeException().Message;
            this.MockHttpResponse(this.analyticsClient, "", HttpStatusCode.Unauthorized);

            // Act
            try
            {
                var result = await this.analyticsClient.GetDetailedCalls(DateTime.Parse(dateFrom), DateTime.Parse(dateTo));

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
        [InlineData("2021-09-24T09:40:00.000Z", "2021-09-24T11:30:00.000Z")]
        internal async Task ShouldBeAbleToHandleForbiddenExceptionWhenGetDetailedCalls(string dateFrom, string dateTo)
        {
            // Arrange
            var expectedMessage = new ForbiddenException().Message;
            this.MockHttpResponse(this.analyticsClient, "", HttpStatusCode.Forbidden);

            // Act
            try
            {
                var result = await this.analyticsClient.GetDetailedCalls(DateTime.Parse(dateFrom), DateTime.Parse(dateTo));

                // Assert
                Assert.True(false);
            }
            catch (ForbiddenException e)
            {
                // Assert
                Assert.Equal(expectedMessage, e.Message);
            }
        }

        [Theory]
        [InlineData("2021-09-24T09:40:00.000Z", "2021-09-24T11:30:00.000Z")]
        internal async Task ShouldBeAbleToHandleInternalFaultExceptionWhenGetDetailedCalls(string dateFrom, string dateTo)
        {
            // Arrange
            var expectedMessage = new InternalFaultException().Message;
            this.MockHttpResponse(this.analyticsClient, "", HttpStatusCode.InternalServerError);

            // Act
            try
            {
                var result = await this.analyticsClient.GetDetailedCalls(DateTime.Parse(dateFrom), DateTime.Parse(dateTo));

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