namespace ConnectSDK.Meetings.Models.V1
{
    using System.Text.Json.Serialization;

    public class DetailedMeetingInfo
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("data")]
        public DetailedMeetingInfoData Data { get; set; }
    }
}
