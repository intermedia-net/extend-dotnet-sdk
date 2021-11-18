namespace ConnectSDK.Voice.Models.V2.Calls
{
    using System.Text.Json.Serialization;

    public class Hub
    {
        [JsonPropertyName("deliveryMethod")]
        public Delivery DeliveryMethod { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("whenExpired")]
        public string WhenExpired { get; set; }
    }
}
