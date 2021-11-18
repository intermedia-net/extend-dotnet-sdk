namespace ConnectSDK.Meetings.Models.V1
{
    using System.Text.Json.Serialization;

    public class MeetingInfo
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("data")]
        public MeetingInfoData Data { get; set; }
    }
}
