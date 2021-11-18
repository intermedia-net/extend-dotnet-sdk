namespace ConnectSDK.Meetings.Models.V1
{
    using System.Text.Json.Serialization;

    public class DetailedMeetingInfoData
    {
        [JsonPropertyName("allowJoinRequests")]
        public bool AllowJoinRequests { get; set; }

        [JsonPropertyName("userID")]
        public string UserId { get; set; }

        [JsonPropertyName("hostGUID")]
        public string HostGuid { get; set; }

        [JsonPropertyName("hostEmail")]
        public string HostEmail { get; set; }

        [JsonPropertyName("meetingCode")]
        public string MeetingCode { get; set; }

        [JsonPropertyName("invitationID")]
        public string InvitationId { get; set; }

        [JsonPropertyName("sessionID")]
        public int SessionId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("startDate")]
        public string StartDate { get; set; }

        [JsonPropertyName("startTime")]
        public string StartTime { get; set; }

        [JsonPropertyName("timeZone")]
        public string TimeZone { get; set; }

        [JsonPropertyName("timeZoneId")]
        public string TimeZoneId { get; set; }

        [JsonPropertyName("meetingType")]
        public int MeetingType { get; set; }

        [JsonPropertyName("meetingTypeDisplay")]
        public string MeetingTypeDisplay { get; set; }

        [JsonPropertyName("meetingServerURI")]
        public string MeetingServerUri { get; set; }

        [JsonPropertyName("requiresPassword")]
        public bool RequiresPassword { get; set; }

        [JsonPropertyName("appLogoURL")]
        public string AppLogoUrl { get; set; }

        [JsonPropertyName("poweredBy")]
        public string PoweredBy { get; set; }

        [JsonPropertyName("upcomingEventTimes")]
        public object UpcomingEventTimes { get; set; }

        [JsonPropertyName("sessionStatusID")]
        public int SessionStatusId { get; set; }

        [JsonPropertyName("sessionStartTime")]
        public string SessionStartTime { get; set; }

        [JsonPropertyName("maxUser")]
        public int MaxUser { get; set; }

        [JsonPropertyName("maxVideoFeeds")]
        public int MaxVideoFeeds { get; set; }

        [JsonPropertyName("maxDuration")]
        public int MaxDuration { get; set; }

        [JsonPropertyName("endTime")]
        public string EndTime { get; set; }

        [JsonPropertyName("hostDisplayName")]
        public string HostDisplayName { get; set; }

        [JsonPropertyName("hostCulture")]
        public string HostCulture { get; set; }

        [JsonPropertyName("exitUrl")]
        public object ExitUrl { get; set; }

        [JsonPropertyName("liveSupportSubscriptionMessage")]
        public string LiveSupportSubscriptionMessage { get; set; }

        [JsonPropertyName("resellerId")]
        public int ResellerId { get; set; }

        [JsonPropertyName("supportURL")]
        public string SupportUrl { get; set; }

        [JsonPropertyName("locked")]
        public bool Locked { get; set; }

        [JsonPropertyName("meetingStarted")]
        public bool MeetingStarted { get; set; }

        [JsonPropertyName("launchApp")]
        public bool LaunchApp { get; set; }

        [JsonPropertyName("useRedesign")]
        public bool UseRedesign { get; set; }

        [JsonPropertyName("virtualAssistantEnabled")]
        public bool VirtualAssistantEnabled { get; set; }

        [JsonPropertyName("sfuEnabled")]
        public bool SfuEnabled { get; set; }

        [JsonPropertyName("calendar")]
        public object Calendar { get; set; }

        [JsonPropertyName("userSettings")]
        public DetailedMeetingInfoUserSettings UserSettings { get; set; }

        [JsonPropertyName("hostLoginUrl")]
        public string HostLoginUrl { get; set; }

        [JsonPropertyName("attendeeLoginUrl")]
        public string AttendeeLoginUrl { get; set; }

        [JsonPropertyName("parentMeetingAccessCode")]
        public object ParentMeetingAccessCode { get; set; }

        [JsonPropertyName("denoiseEnabled")]
        public bool DenoiseEnabled { get; set; }

        [JsonPropertyName("echoDetectionEnabled")]
        public bool EchoDetectionEnabled { get; set; }
    }
}
