namespace ConnectSDK.Voice.Calls
{
    using System;
    using System.Threading.Tasks;
    using Common;
    using ConnectSDK.Voice.Models.V2.Calls;

    internal class CallsClient : ICallsClient
    {
        private readonly INotificationHub notificationHub;
        private readonly IRequestFactory requestFactory = new RequestFactory();
        private readonly Func<string, Task<string>> getToken;

        public CallsClient(Func<string, Task<string>> getToken)
        {
            this.getToken = getToken;
            this.notificationHub = new NotificationHub(this.getToken);
        }

        public CallsClient(Func<string, Task<string>> getToken, ConnectSdkConfig config)
        {
            this.getToken = getToken;
            this.requestFactory.SetUserAgent(config.AppName, config.AppVersion);
            this.notificationHub = new NotificationHub(this.getToken, config);
        }

        public async Task<Hub> CreateSubscription(Action<CallEvent> eventHandler)
        {
            var hub = await this.notificationHub.CreateSubscription();

            await this.notificationHub.StartHubConnectionAsync(hub.DeliveryMethod.Uri.ToString(), eventHandler);
            return hub;
        }

        public async Task<Hub> RenewSubscription(string id)
        {
            if(id == null){ throw new ArgumentNullException(nameof(id)); }
            
            return await this.notificationHub.RenewSubscription(id);
        }

        public async Task DeleteSubscription(string id)
        {
            if(id == null){ throw new ArgumentNullException(nameof(id)); }
            
            await this.notificationHub.DeleteSubscription(id);
        }

        public async Task<MakeCallResponse> MakeCall(MakeCallBody body)
        {
            if(body == null){ throw new ArgumentNullException(nameof(body)); }
            
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceCalls);
            this.SetToken(token);

            var url = "voice/v2/calls";

            return await this.requestFactory.PostAsync<MakeCallResponse>(url, body);
        }

        public async Task<CallResponse> TerminateCall(string callId, string commandId = null)
        {
            if(callId == null){ throw new ArgumentNullException(nameof(callId)); }
            
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceCalls);
            this.SetToken(token);

            var url = $"voice/v2/calls/{callId}";

            if (string.IsNullOrEmpty(commandId) && Guid.TryParse(commandId, out var commandIdGuid))
            {
                url += $"?commandId={commandIdGuid:D}";
            }

            return await this.requestFactory.DeleteAsync<CallResponse>(url);
        }

        public async Task<CallResponse> CancelCall(string callId, CancelCallBody body = null)
        {
            if(callId == null){ throw new ArgumentNullException(nameof(callId)); }
            
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceCalls);
            this.SetToken(token);

            var url = $"voice/v2/calls/{callId}/cancel";

            return await this.requestFactory.PostAsync<CallResponse>(url, body);
        }

        public async Task<CallResponse> MergeCall(string callId, MergeCallBody body)
        {
            if(callId == null){ throw new ArgumentNullException(nameof(callId)); }
            
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceCalls);
            this.SetToken(token);

            var url = $"voice/v2/calls/{callId}/merge";

            return await this.requestFactory.PostAsync<CallResponse>(url, body);
        }

        public async Task<CallResponse> TransferCall(string callId, TransferCallBody body)
        {
            if(callId == null){ throw new ArgumentNullException(nameof(callId)); }
            
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceCalls);
            this.SetToken(token);

            var url = $"voice/v2/calls/{callId}/transfer";

            return await this.requestFactory.PostAsync<CallResponse>(url, body);
        }

        public async Task<DevicesResponse> GetDevices()
        {
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceCallingDevices);
            this.SetToken(token);

            var url = "voice/v2/devices";

            return await this.requestFactory.GetAsync<DevicesResponse>(url);
        }

        private void SetToken(string token)
        {
            this.requestFactory.SetToken(token);
        }
    }
}
