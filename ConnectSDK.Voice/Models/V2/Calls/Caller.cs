namespace ConnectSDK.Voice.Models.V2.Calls
{
    using System.Text.Json.Serialization;

    public class Caller
    {
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
    }
}
