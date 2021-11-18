namespace ConnectSDK.Voice.Models.V2.CallRecordings
{
    using System.Text.Json.Serialization;

    public class CallRecordings
    {
        [JsonPropertyName("records")]
        public CallRecording[] Records { get; set; }
    }
}
