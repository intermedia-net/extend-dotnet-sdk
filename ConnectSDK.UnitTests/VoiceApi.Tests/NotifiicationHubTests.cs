namespace ConnectSDK.UnitTests.VoiceApi.Tests
{
    using System;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;
    using ConnectSDK.UnitTests.Common;
    using ConnectSDK.Voice;
    using ConnectSDK.Voice.Calls;
    using Xunit;
    using Xunit.Abstractions;
    
    
    public class NotificationHubTests : TestBase
    {
        private readonly INotificationHub notificationHub;

        public NotificationHubTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var tokenProvider = new AuthMock();
            this.notificationHub = new NotificationHub(tokenProvider.GetToken);
            
        }
        [Theory]
        [InlineData("3fa85f64-5717-4562-b3fc-2c963f66afa6", "https://mocks.dev/0f99e")]
        public async Task ShouldBeAbleToCreateSubscription(string subscriptionId, string deliveryUrl)
        {
            // Arrange
            var eventHandler = new CallEventHandler();
            var expDate = DateTime.UtcNow.AddHours(1);
            var response = $"{{\"deliveryMethod\": {{\"uri\": \"{deliveryUrl}\"}},\"id\": \"{subscriptionId}\",\"whenExpired\": \"{expDate:O}\"}}";
            MockHttpResponse(this.notificationHub, response);

            // Act
            var result =  await this.notificationHub.CreateSubscription();

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.Equal(subscriptionId, result.Id);
        }
        
        [Theory]
        [InlineData("3fa85f64-5717-4562-b3fc-2c963f66afa6")]
        public async Task ShouldBeAbleToDeleteSubscription(string subscriptionId)
        {
            var rawResponse = JsonSerializer.Serialize(Guid.NewGuid().ToString("N"));
            MockHttpResponse(this.notificationHub, rawResponse);

            // Act
            await this.notificationHub.DeleteSubscription(subscriptionId);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
        }
        
        [Theory]
        [InlineData("3fa85f64-5717-4562-b3fc-2c963f66afa6", "https://mocks.dev/0f99e")]
        public async Task ShouldBeAbleToRenewSubscription(string subscriptionId, string deliveryUrl)
        {
            // Arrange
            var eventHandler = new CallEventHandler();
            var expDate = DateTime.UtcNow.AddHours(1);
            var response = $"{{\"deliveryMethod\": {{\"uri\": \"{deliveryUrl}\"}},\"id\": \"{subscriptionId}\",\"whenExpired\": \"{expDate:O}\"}}";
            MockHttpResponse(this.notificationHub, response);

            // Act
            var result =  await this.notificationHub.RenewSubscription(subscriptionId);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.Equal(subscriptionId, result.Id);
        }
    }
}
