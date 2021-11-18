namespace ConnectSDK.Voice.Models.V2.CallRecordings
{
    using System.Text.Json.Serialization;
    using ConnectSDK.Voice.Models.V2.Calls;

    public class CallRecording
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("fileName")]
        public string FileName { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("caller")]
        public Caller Caller { get; set; }

        [JsonPropertyName("whenCreated")]
        public string WhenCreated { get; set; }

        [JsonPropertyName("callId")]
        public string CallId { get; set; }

        [JsonPropertyName("direction")]
        public string Direction { get; set; }

        [JsonPropertyName("wasPaused")]
        public bool WasPaused { get; set; }
    }
}
