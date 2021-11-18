namespace ConnectSDK.Analytics.Models
{
    using System.Text.Json.Serialization;

    public class DetailedCalls
    {
        [JsonPropertyName("calls")]
        public DetailedCall[] Calls { get; set; }

        [JsonPropertyName("totalCalls")]
        public int TotalCalls { get; set; }
    }
}
