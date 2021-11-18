namespace ConnectSDK.Meetings.Models.V1
{
    using System.Text.Json.Serialization;

    public class DetailedMeetingInfoUserSettings
    {
        [JsonPropertyName("userPairs")]
        public object UserPairs { get; set; }
    }
}
