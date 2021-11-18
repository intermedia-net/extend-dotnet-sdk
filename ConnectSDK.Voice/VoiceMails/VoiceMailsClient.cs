namespace ConnectSDK.Voice.VoiceMails
{
    using System;
    using System.Threading.Tasks;
    using System.Web;
    using Common;
    using ConnectSDK.Voice.Models.V2.VoiceMails;
    using ConnectSDK.Voice.Models.V2.VoiceMailsSettings;

    internal class VoiceMailsClient : IVoiceMailsClient
    {
        private readonly IRequestFactory requestFactory = new RequestFactory();
        private readonly Func<string, Task<string>> getToken;

        public VoiceMailsClient(Func<string, Task<string>> getToken)
        {
            this.getToken = getToken;
        }

        public VoiceMailsClient(Func<string, Task<string>> getToken, ConnectSdkConfig config)
        {
            this.getToken = getToken;
            this.requestFactory.SetUserAgent(config.AppName, config.AppVersion);
        }

        public async Task<VoiceMails> GetUserVoiceMails(int offset = 0, int countOnList = 20)
        {
            if(offset < 0){ throw new ArgumentException("Not valid value. Possible values are greater than zero", nameof(offset)); }
            if(countOnList <= 0){ throw new ArgumentException("Not valid value. Possible values are greater than zero", nameof(countOnList)); }
            
            
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceVoicemails);
            this.SetToken(token);

            var url = "voice/v2/voicemails";
            var urlParams = HttpUtility.ParseQueryString(string.Empty);
            
            urlParams.Add("offset", offset.ToString());
            urlParams.Add("countOnList", countOnList.ToString());

            var fullUrl = url + "?" + urlParams;

            return await this.requestFactory.GetAsync<VoiceMails>(fullUrl);
        }

        public async Task DeleteVoicemailRecords(Status? status = null)
        {
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceVoicemails);
            this.SetToken(token);

            var url = "voice/v2/voicemails/_all";

            var urlParams = HttpUtility.ParseQueryString(string.Empty);

            if (status == Status.Read)
            {
                urlParams.Add("status", "read");
            }
            else if (status == Status.Unread)
            {
                urlParams.Add("status", "unread");
            }

            var fullUrl = url + "?" + urlParams;

            await this.requestFactory.DeleteAsync(fullUrl);
        }

        public async Task UpdateVoicemailRecordsStatus(SelectedVoiceMailsBody statusBody)
        {
            if(statusBody == null){ throw new ArgumentNullException(nameof(statusBody)); }
            if(Array.Exists(statusBody.Ids, element => element <= 0)){ throw new ArgumentException("Not valid value. Possible values are greater than zero", nameof(statusBody.Ids)); }

            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceVoicemails);
            this.SetToken(token);

            var url = "/voice/v2/voicemails/_all/_metadata";

            await this.requestFactory.PostAsync(url, statusBody);
        }

        public async Task DeleteSelectedVoicemailRecords(SelectedIdsBody selectedIdsBody)
        {
            if(selectedIdsBody == null){ throw new ArgumentNullException(nameof(selectedIdsBody)); }
            if(Array.Exists(selectedIdsBody.Ids, element => element <= 0)){ throw new ArgumentException("Not valid value. Possible values are greater than zero", nameof(selectedIdsBody.Ids)); }

            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceVoicemails);
            this.SetToken(token);

            var url = "/voice/v2/voicemails/_selected";

            await this.requestFactory.DeleteAsync(url, selectedIdsBody);
        }

        public async Task UpdateSelectedVoicemailRecordsStatus(SelectedVoiceMailsBody statusBody)
        {
            if(statusBody == null){ throw new ArgumentNullException(nameof(statusBody)); }
            if(Array.Exists(statusBody.Ids, element => element <= 0)){ throw new ArgumentException("Not valid value. Possible values are greater than zero", nameof(statusBody.Ids)); }
            
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceVoicemails);
            this.SetToken(token);

            var url = "/voice/v2/voicemails/_selected/_metadata";

            await this.requestFactory.PostAsync(url, statusBody);
        }

        public async Task<VoiceMailTotal> GetUserVoicemailsTotal(Status? status = null)
        {
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceVoicemails);
            this.SetToken(token);

            var url = "/voice/v2/voicemails/_total";
            var urlParams = HttpUtility.ParseQueryString(string.Empty);

            if (status == Status.Read)
            {
                urlParams.Add("status", "read");
            }
            else if (status == Status.Unread)
            {
                urlParams.Add("status", "unread");
            }

            var fullUrl = url + "?" + urlParams;

            return await this.requestFactory.GetAsync<VoiceMailTotal>(fullUrl);
        }

        public async Task<VoiceMail> GetVoicemailRecord(string id)
        {
            if(id == null){ throw new ArgumentNullException(nameof(id)); }
            
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceVoicemails);
            this.SetToken(token);

            var url = $"/voice/v2/voicemails/{id}";

            return await this.requestFactory.GetAsync<VoiceMail>(url);
        }

        public async Task<Transcript> GetVoicemailTranscript(string id)
        {
            if(id == null){ throw new ArgumentNullException(nameof(id)); }
            
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceVoicemails);
            this.SetToken(token);

            var url = $"/voice/v2/voicemails/{id}/_transcript";

            return await this.requestFactory.GetAsync<Transcript>(url);
        }

        public async Task<byte[]> GetVoicemailContent(string id, Format format)
        {
            if(id == null){ throw new ArgumentNullException(nameof(id)); }
            
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceVoicemails);
            this.SetToken(token);

            var url = $"/voice/v2/voicemails/{id}/_content";
            var urlParams = HttpUtility.ParseQueryString(string.Empty);

            switch (format)
            {
                case Format.Mp3:
                    urlParams.Add("format", "mp3");
                    break;
                case Format.Ogg:
                    urlParams.Add("format", "ogg");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), "Not supported content format");
            }

            var fullUrl = url + "?" + urlParams;

            return await this.requestFactory.GetAsync<byte[]>(fullUrl);
        }

        private void SetToken(string token)
        {
            this.requestFactory.SetToken(token);
        }
    }
}
