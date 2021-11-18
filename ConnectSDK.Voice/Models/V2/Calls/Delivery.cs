namespace ConnectSDK.Voice.Models.V2.Calls
{
    using System.Text.Json.Serialization;

    public class Delivery
    {
        [JsonPropertyName("uri")]
        public object Uri { get; set; }
    }
}
