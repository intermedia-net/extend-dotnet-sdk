namespace ConnectSDK.UnitTests.AnalyticsApi.Tests
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using Xunit;
    using Xunit.Abstractions;
    using ConnectSDK.Analytics;
    using ConnectSDK.Analytics.Models;
    using ConnectSDK.UnitTests.Common;

    public class AnalyticsClientTests : TestBase
    {
        // Subject of testing.
        private readonly IAnalyticsClient analyticsClient;

        public AnalyticsClientTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var tokenProvider = new AuthMock();
            this.analyticsClient = new AnalyticsClient(tokenProvider.GetToken);
        }

        [Theory, AutoData]
        internal async Task ShouldBeAbleToGetDetailedCalls(DateTime dateFrom, DateTime dateTo,string sortColumn, bool descending, int offset, int size, int accountId, GetDetailedCallsBody filters, DetailedCalls response)
        {
            // Arrange
            this.MockHttpResponse(this.analyticsClient,JsonSerializer.Serialize(response));

            // Act
            var result = await this.analyticsClient.GetDetailedCalls(dateFrom, dateTo, sortColumn, descending, offset, size, accountId, filters);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.NotEmpty(result.Calls);
            Assert.Equal(response.TotalCalls, result.TotalCalls);
        }

        [Theory, AutoData]
        internal async Task ShouldBeAbleToGetUserCalls(DateTime dateFrom, DateTime dateTo, int accountId, GetUserCallsBody getUserCallsBody, DetailedCalls response)
        {
            // Arrange
            this.MockHttpResponse(this.analyticsClient,JsonSerializer.Serialize(response));

            // Act
            var result = await this.analyticsClient.GetUserCalls(dateFrom, dateTo, accountId, getUserCallsBody);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.NotEmpty(result.Calls);
            Assert.Equal(response.TotalCalls, result.TotalCalls);
        }

        [Theory, AutoData]
        internal async Task ShouldBeAbleToGetUsageHistoryWithFilters(DateTime dateFrom, DateTime dateTo, UsageHistoryBody filters, string sortColumn, bool descending, int accountId, DetailedCalls response)
        {
            // Arrange
            this.MockHttpResponse(this.analyticsClient,JsonSerializer.Serialize(response));

            // Act
            var result = await this.analyticsClient.GetUsageHistory(dateFrom, dateTo, filters, sortColumn, descending, accountId);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.NotEmpty(result.Calls);
            Assert.Equal(response.TotalCalls, result.TotalCalls);
        }
        
        [Theory, AutoData]
        internal async Task ShouldBeAbleToGetUsageHistory(DateTime dateFrom, DateTime dateTo, string sortColumn, bool descending, int accountId, DetailedCalls response)
        {
            // Arrange
            this.MockHttpResponse(this.analyticsClient,JsonSerializer.Serialize(response));

            // Act
            var result = await this.analyticsClient.GetUsageHistory(dateFrom, dateTo,sortColumn,descending,accountId);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.NotEmpty(result.Calls);
            Assert.Equal(response.TotalCalls, result.TotalCalls);
        }

        [Theory, AutoData]
        internal async Task ShouldBeAbleToGetUsageHistoryForAllCalls(DateTime dateFrom, DateTime dateTo, string sortColumn, bool descending, int accountId, DetailedCalls response)
        {
            this.MockHttpResponse(this.analyticsClient, JsonSerializer.Serialize(response));

            // Act
            var result = await this.analyticsClient.GetUsageHistoryForAllCalls(dateFrom, dateTo, sortColumn, descending, accountId);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.NotEmpty(result.Calls);
            Assert.Equal(response.TotalCalls, result.TotalCalls);
        }
        

        [Theory, AutoData]
        internal async Task ShouldBeAbleToGetUsageHistoryForAllCallsWithFilters(DateTime dateFrom, DateTime dateTo,UsageHistoryBody filters, string sortColumn, bool descending, int accountId, DetailedCalls response)
        {
            this.MockHttpResponse(this.analyticsClient, JsonSerializer.Serialize(response));

            // Act
            var result = await this.analyticsClient.GetUsageHistoryForAllCalls(dateFrom, dateTo, filters, sortColumn, descending, accountId);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.NotEmpty(result.Calls);
            Assert.Equal(response.TotalCalls, result.TotalCalls);
        }

        [Theory, AutoData]
        internal async Task ShouldBeAbleToGetUsageHistoryForDirectCallsWithFilters(DateTime dateFrom, DateTime dateTo, UsageHistoryBody filters, string sortColumn, bool descending, int accountId, DetailedCalls response)
        {
            this.MockHttpResponse(this.analyticsClient, JsonSerializer.Serialize(response));

            // Act
            var result = await this.analyticsClient.GetUsageHistoryForDirectCalls(dateFrom, dateTo, filters, sortColumn, descending, accountId);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.NotEmpty(result.Calls);
            Assert.Equal(response.TotalCalls, result.TotalCalls);
        }
        
        [Theory, AutoData]
        internal async Task ShouldBeAbleToGetUsageHistoryForDirectCalls(DateTime dateFrom, DateTime dateTo, string sortColumn, bool descending, int accountId, DetailedCalls response)
        {
            this.MockHttpResponse(this.analyticsClient, JsonSerializer.Serialize(response));

            // Act
            var result = await this.analyticsClient.GetUsageHistoryForDirectCalls(dateFrom, dateTo, sortColumn, descending, accountId);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.NotEmpty(result.Calls);
            Assert.Equal(response.TotalCalls, result.TotalCalls);
        }


        [Theory, AutoData]
        internal async Task ShouldBeAbleToGetUsageHistoryForGroupCallsWithFilters(DateTime dateFrom, DateTime dateTo, UsageHistoryBody filters, string sortColumn, bool descending, int accountId, DetailedCalls response)
        {
            this.MockHttpResponse(this.analyticsClient, JsonSerializer.Serialize(response));
            // Act
            var result = await this.analyticsClient.GetUsageHistoryForGroupCalls(dateFrom, dateTo, filters, sortColumn, descending, accountId);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.NotEmpty(result.Calls);
            Assert.Equal(response.TotalCalls, result.TotalCalls);
        }

        [Theory, AutoData]
        internal async Task ShouldBeAbleToGetUsageHistoryForGroupCalls(DateTime dateFrom, DateTime dateTo, string sortColumn, bool descending, int accountId, DetailedCalls response)
        {
            this.MockHttpResponse(this.analyticsClient, JsonSerializer.Serialize(response));
            // Act
            var result = await this.analyticsClient.GetUsageHistoryForGroupCalls(dateFrom, dateTo, sortColumn, descending, accountId);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.NotEmpty(result.Calls);
            Assert.Equal(response.TotalCalls, result.TotalCalls);
        }

        
        [Theory, AutoData]
        internal async Task ShouldBeAbleToGetUserFilters(DateTime dateFrom, DateTime dateTo, int accountId, UserFilters[] response)
        {
            this.MockHttpResponse(this.analyticsClient, JsonSerializer.Serialize(response));
            // Act
            var result = await this.analyticsClient.GetUserFilters(dateFrom, dateTo, accountId);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.Equal(response[0].Name, response[0].Name);
        }
        
        
    }
}
