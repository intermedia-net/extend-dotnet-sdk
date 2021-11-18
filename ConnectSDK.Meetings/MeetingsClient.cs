namespace ConnectSDK.Meetings
{
    using System;
    using System.Threading.Tasks;
    using ConnectSDK.Common;
    using ConnectSDK.Meetings.Models.V1;

    public class MeetingsClient : IMeetingsClient
    {
        private readonly IRequestFactory requestFactory = new RequestFactory();
        private readonly Func<string, Task<string>> getToken;

        public MeetingsClient(Func<string, Task<string>> getToken)
        {
            this.getToken = getToken;
        }

        public MeetingsClient(Func<string, Task<string>> getToken, ConnectSdkConfig config)
        {
            if(config == null){ throw new ArgumentNullException(nameof(config)); }
            
            this.getToken = getToken ?? throw new ArgumentNullException(nameof(getToken));
            this.requestFactory.SetUserAgent(config.AppName, config.AppVersion);
        }

        public async Task<UserDetails> GetUserDetails()
        {
            var token = await this.getToken(Constants.MeetingScopes.ApiUserVideoConference);
            this.SetToken(token);

            var url = "meetings/v1/user";

            return await this.requestFactory.GetAsync<UserDetails>(url);
        }

        public async Task<MeetingInfo> StartNewMeetingWithDetails()
        {
            var token = await this.getToken(Constants.MeetingScopes.ApiUserVideoConference);
            this.SetToken(token);

            var url = "meetings/v1/meeting/start/details";

            return await this.requestFactory.PostAsync<MeetingInfo>(url, null);
        }

        public async Task<DetailedMeetingInfo> GetMeetingDetails(string meetingCode)
        {
            if(meetingCode == null){ throw new ArgumentNullException(nameof(meetingCode)); }
            
            var token = await this.getToken(Constants.MeetingScopes.ApiUserVideoConference);
            this.SetToken(token);

            var url = $"meetings/v1/meeting/{meetingCode}";

            return await this.requestFactory.GetAsync<DetailedMeetingInfo>(url);
        }

        private void SetToken(string token)
        {
            this.requestFactory.SetToken(token);
        }
    }
}
