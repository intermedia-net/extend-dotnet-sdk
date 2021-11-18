namespace ConnectSDK.Analytics.Models
{
    using System;
    using System.Text.Json.Serialization;

    public class DetailedCall
    {
        [JsonPropertyName("billing")]
        public DetailedCallBilling Billing { get; set; }

        [JsonPropertyName("direction")]
        public string Direction { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("from")]
        public DetailedCallUser From { get; set; }

        [JsonPropertyName("group")]
        public string Group { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("globalCallId")]
        public string GlobalCallId { get; set; }

        [JsonPropertyName("start")]
        public DateTime Start { get; set; }

        [JsonPropertyName("to")]
        public DetailedCallUser To { get; set; }
    }
}
