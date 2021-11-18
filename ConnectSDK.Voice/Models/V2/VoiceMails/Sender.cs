namespace ConnectSDK.Voice.Models.V2.VoiceMails
{
    using System.Text.Json.Serialization;

    public class Sender
    {
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
    }
}
