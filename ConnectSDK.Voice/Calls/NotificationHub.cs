namespace ConnectSDK.Voice.Calls
{
    using System;
    using System.Threading.Tasks;
    using Common;
    using ConnectSDK.Voice.Models.V2.Calls;
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.Extensions.Logging;

    public class NotificationHub : INotificationHub
    {
        private readonly IRequestFactory requestFactory = new RequestFactory();
        private readonly Func<string, Task<string>> getToken;

        public NotificationHub(Func<string, Task<string>> getToken)
        {
            this.getToken = getToken;
        }

        public NotificationHub(Func<string, Task<string>> getToken, ConnectSdkConfig config)
        {
            this.getToken = getToken;
            this.requestFactory.SetUserAgent(config.AppName, config.AppVersion);
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

            connection.On("OnEvent", eventHandler);

            await this.TryStartAsync(connection);
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
            var body = new { };
            return await this.requestFactory.PostAsync<Hub>(url, body);
        }

        private void SetToken(string token)
        {
            this.requestFactory.SetToken(token);
        }

        private async Task TryStartAsync(HubConnection connection)
        {
            try
            {
                await connection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
