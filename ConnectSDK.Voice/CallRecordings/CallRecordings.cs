namespace ConnectSDK.Voice.CallRecordings
{
    using System;
    using System.Threading.Tasks;
    using System.Web;
    using Common;
    using ConnectSDK.Voice.Models.V2.CallRecordings;

    internal class CallRecordingsClient : ICallRecordingsClient
    {
        private readonly IRequestFactory requestFactory = new RequestFactory();
        private readonly Func<string, Task<string>> getToken;

        public CallRecordingsClient(Func<string, Task<string>> getToken)
        {
            this.getToken = getToken;
        }

        public CallRecordingsClient(Func<string, Task<string>> getToken, ConnectSdkConfig config)
        {
            this.getToken = getToken;
            this.requestFactory.SetUserAgent(config.AppName, config.AppVersion);
        }

        public async Task<CallRecordings> GetCallRecordings(
            string uuid,
            int? offset = null,
            int? count = null)
        {
            if(uuid == null){ throw new ArgumentNullException(nameof(uuid)); }
            
            
            var token = await this.getToken(Constants.VoiceScopes.ApiServiceVoiceCallRecordings);
            this.SetToken(token);

            var url = $"voice/v2/customers/_me/users/{uuid}/call-recordings";

            var urlParams = HttpUtility.ParseQueryString(string.Empty);

            if (offset != null)
            {
                urlParams.Add("offset", offset.ToString());
            }

            if (count != null)
            {
                urlParams.Add("count", count.ToString());
            }

            var fullUrl = url + "?" + urlParams;

            return await this.requestFactory.GetAsync<CallRecordings>(fullUrl);
        }

        public async Task<byte[]> GetCallRecordingContent(string uuid, int id)
        {
            if(uuid == null){ throw new ArgumentNullException(nameof(uuid)); }
            if(id <= 0){ throw new ArgumentException("Not valid value. Possible values are greater than zero", nameof(id)); }
            var token = await this.getToken(Constants.VoiceScopes.ApiServiceVoiceCallRecordings);
            this.SetToken(token);

            var url = $"voice/v2/customers/_me/users/{uuid}/call-recordings/{id}/_content";

            return await this.requestFactory.GetAsync<byte[]>(url);
        }

        public async Task<byte[]> GetCallRecordingArchive(string uuid, int[] ids, string format = "zip")
        {
            if(uuid == null){ throw new ArgumentNullException(nameof(uuid)); }
            if(Array.Exists(ids, element => element <= 0)){ throw new ArgumentException("Not valid value. Possible values are greater than zero", nameof(ids)); }
            
            var token = await this.getToken(Constants.VoiceScopes.ApiServiceVoiceCallRecordings);
            this.SetToken(token);

            var url = $"voice/v2/customers/_me/users/{uuid}/call-recordings/_selected/_content?format={format}";

            var body = new { ids };

            return await this.requestFactory.PostAsync<byte[]>(url, body);
        }

        private void SetToken(string token)
        {
            this.requestFactory.SetToken(token);
        }
    }
}
