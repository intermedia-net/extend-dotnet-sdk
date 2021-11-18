namespace ConnectSDK.UnitTests.VoiceApi.Tests
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.Extensions.Logging;
    using ConnectSDK.Common;
    using ConnectSDK.Voice.Calls;
    using ConnectSDK.Voice.Models.V2.Calls;
    using ConnectSDK.Voice;
    public class FakeNotificationHub : INotificationHub
    {
        private readonly IRequestFactory requestFactory = new RequestFactory();
        private readonly Func<string, Task<string>> getToken;

        private Action<CallEvent> eventHandler = null;
        
        public bool IsConnected { get; private set; }

        public FakeNotificationHub(Func<string, Task<string>> getToken)
        {
            this.getToken = getToken;
        }

        public void SubmitEvent(CallEvent callEvent)
        {
            if (this.IsConnected && this.eventHandler != null)
            {
                this.eventHandler(callEvent);
            }
        }

        public async Task StartHubConnectionAsync(string uri, Action<CallEvent> eventHandler)
        {
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceCalls);
            this.SetToken(token);

            var connection = new HubConnectionBuilder()
                .WithUrl(
                    uri, options =>
                    {
                        options.AccessTokenProvider = () => Task.FromResult(token);
                    })
                .ConfigureLogging(
                    logging =>
                    {
                        logging.AddConsole();
                        logging.SetMinimumLevel(LogLevel.Debug);
                    })
                .Build();

            this.eventHandler = eventHandler;
            connection.On("OnEvent", eventHandler);

            this.IsConnected = true;
        }

        public async Task<Hub> CreateSubscription()
        {
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceCalls);
            this.SetToken(token);

            var url = "/voice/v2/subscriptions";
            var body = new
            {
                Events = new[] { "*" },
                Ttl = TimeSpan.FromMinutes(30).ToString(@"hh\:mm\:ss")
            };

            return await this.requestFactory.PostAsync<Hub>(url, body);
        }

        public async Task DeleteSubscription(string id)
        {
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceCalls);
            this.SetToken(token);

            var url = $"/voice/v2/subscriptions/{id}";

            await this.requestFactory.DeleteAsync(url);
        }

        public async Task<Hub> RenewSubscription(string id)
        {
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceCalls);
            this.SetToken(token);

            var url = $"/voice/v2/subscriptions/{id}/renew";

            return await this.requestFactory.PostAsync<Hub>(url, "{}");
        }

        private void SetToken(string token)
        {
            this.requestFactory.SetToken(token);
        }
    }
}
