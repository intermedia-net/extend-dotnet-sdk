namespace ConnectSDK.Meetings.Models.V1
{
    using System.Text.Json.Serialization;

    public class MeetingInfoData
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("meetingCode")]
        public string MeetingCode { get; set; }

        [JsonPropertyName("startTime")]
        public string StartTime { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("hostURL")]
        public string HostUrl { get; set; }

        [JsonPropertyName("attendeeURL")]
        public string AttendeeUrl { get; set; }

        [JsonPropertyName("meetingType")]
        public int MeetingType { get; set; }

        [JsonPropertyName("meetingTypeDisplay")]
        public string MeetingTypeDisplay { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("timeZone")]
        public string TimeZone { get; set; }

        [JsonPropertyName("phoneInfo")]
        public MeetingInfoPhoneInfo PhoneInfo { get; set; }
    }
}
