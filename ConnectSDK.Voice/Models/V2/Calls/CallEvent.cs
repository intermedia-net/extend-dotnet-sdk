namespace ConnectSDK.Voice.Models.V2.Calls
{
    using System.Text.Json.Serialization;

    public sealed class CallEvent
    {
        [JsonPropertyName("eventType")]
        public string EventType { get; set; }

        [JsonPropertyName("callDirection")]
        public string CallDirection { get; set; }

        [JsonPropertyName("callType")]
        public string CallType { get; set; }

        [JsonPropertyName("callId")]
        public string CallId { get; set; }

        [JsonPropertyName("caller")]
        public Caller Caller { get; set; }

        [JsonPropertyName("whenRaised")]
        public string WhenRaised { get; set; }
    }
}
