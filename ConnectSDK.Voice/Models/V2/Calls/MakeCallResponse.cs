namespace ConnectSDK.Voice.Models.V2.Calls
{
    using System.Text.Json.Serialization;

    public class MakeCallResponse
    {
        [JsonPropertyName("callId")]
        public string CallId { get; set; }

        [JsonPropertyName("commandId")]
        public string CommandId { get; set; }
    }
}
