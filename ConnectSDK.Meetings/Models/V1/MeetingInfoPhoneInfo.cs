namespace ConnectSDK.Meetings.Models.V1
{
    using System.Text.Json.Serialization;

    public class MeetingInfoPhoneInfo
    {
        [JsonPropertyName("attendeePIN")]
        public string AttendeePin { get; set; }

        [JsonPropertyName("did")]
        public string Did { get; set; }

        [JsonPropertyName("presenterPIN")]
        public string PresenterPin { get; set; }
    }
}
