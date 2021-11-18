namespace ConnectSDK.Analytics.Models
{
    using System.Text.Json.Serialization;

    public class DetailedCallBilling
    {
        [JsonPropertyName("charge")]
        public double Charge { get; set; }

        [JsonPropertyName("chargedSeconds")]
        public int ChargedSeconds { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("freeMinutes")]
        public int FreeMinutes { get; set; }

        [JsonPropertyName("freeQuantity")]
        public int FreeQuantity { get; set; }

        [JsonPropertyName("invoicePeriod")]
        public string InvoicePeriod { get; set; }

        [JsonPropertyName("tax")]
        public double Tax { get; set; }

        [JsonPropertyName("usageType")]
        public string[] UsageType { get; set; }
    }
}
