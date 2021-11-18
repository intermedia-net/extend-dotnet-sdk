namespace ConnectSDK.UnitTests.VoiceApi.Tests
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Threading.Tasks;
    using ConnectSDK.UnitTests.Common;
    using ConnectSDK.Voice;
    using Xunit;
    using Xunit.Abstractions;
    using ConnectSDK.Voice.Models.V2.Calls;
    
    public class EventHubTests : TestBase
    {
        private readonly IVoiceClient voiceClient;
        private readonly FakeNotificationHub testNotificationHub;

        public EventHubTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var tokenProvider = new AuthMock();
            this.voiceClient = new VoiceClient(tokenProvider.GetToken);
            this.testNotificationHub = new FakeNotificationHub(tokenProvider.GetToken);
            this.voiceClient.CallsClient
                .GetType()
                .GetField("notificationHub", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(this.voiceClient.CallsClient, this.testNotificationHub);
        }

        [Theory]
        [InlineData("3fa85f64-5717-4562-b3fc-2c963f66afa6", "https://mocks.dev/0f99e")]
        public async Task ShouldCreateEventHubSubscription(string subscriptionId, string deliveryUrl)
        {
            // Arrange
            var eventHandler = new CallEventHandler();
            var expDate = DateTime.UtcNow.AddHours(1);
            var response = $"{{\"deliveryMethod\": {{\"uri\": \"{deliveryUrl}\"}},\"id\": \"{subscriptionId}\",\"whenExpired\": \"{expDate:O}\"}}";
            MockHttpResponse(this.testNotificationHub, response, HttpStatusCode.Created);

            // Act
            await this.voiceClient.CallsClient.CreateSubscription(eventHandler.Process);

            // Assert
            Assert.True(this.testNotificationHub.IsConnected);
        }

        [Theory]
        [InlineData("3fa85f64-5717-4562-b3fc-2c963f66afa6", "https://mocks.dev/0f99e")]
        public async Task ShouldCreateEventHubSubscriptionAndReceiveEvent(string subscriptionId, string deliveryUrl)
        {
            // Arrange
            var eventHandler = new CallEventHandler();
            var expDate = DateTime.UtcNow.AddHours(1);
            var expectedCallEvent = new CallEvent()
            {
                CallDirection = "incoming",
                Caller = new Caller()
                {
                    DisplayName = "Tests",
                    PhoneNumber = "555"
                },
                CallId = Guid.NewGuid().ToString("D"),
                CallType = "internal",
                EventType = "ringing",
                WhenRaised = DateTime.UtcNow.ToString("O")
            };
            var response = $"{{\"deliveryMethod\": {{\"uri\": \"{deliveryUrl}\"}},\"id\": \"{subscriptionId}\",\"whenExpired\": \"{expDate:O}\"}}";
            MockHttpResponse(this.testNotificationHub, response, HttpStatusCode.Created);

            // Act
            await this.voiceClient.CallsClient.CreateSubscription(eventHandler.Process);
            this.testNotificationHub.SubmitEvent(expectedCallEvent);

            // Assert
            Assert.Single(eventHandler.ProcessedEvents);
            Assert.Same(expectedCallEvent, eventHandler.ProcessedEvents.Single());
        }

        [Theory]
        [InlineData("3fa85f64-5717-4562-b3fc-2c963f66afa6", "https://mocks.dev/0f99e")]
        public async Task ShouldRefreshEventHubSubscription(string subscriptionId, string deliveryUrl)
        {
            // Arrange
            var expDate = DateTime.UtcNow.AddHours(1);
            var response = $"{{\"deliveryMethod\": {{\"uri\": \"{deliveryUrl}\"}},\"id\": \"{subscriptionId}\",\"whenExpired\": \"{expDate:O}\"}}";
            MockHttpResponse(this.testNotificationHub, response);

            // Act
            var hub = await this.voiceClient.CallsClient.RenewSubscription(subscriptionId);

            // Assert
            Assert.Equal(subscriptionId, hub.Id);
        }

        [Theory]
        [InlineData("3fa85f64-5717-4562-b3fc-2c963f66afa6")]
        public async Task ShouldDeleteEventHubSubscription(string subscriptionId)
        {
            MockHttpResponse(this.testNotificationHub, new byte[] { }, HttpStatusCode.NoContent);

            await this.voiceClient.CallsClient.DeleteSubscription(subscriptionId);
        }
    }
}
