namespace ConnectSDK.Analytics.Models
{
    using System.Text.Json.Serialization;

    public class UsageHistoryBody
    {
        public UsageHistoryBody(int[] userIds = null, string[] from = null, string[] to = null, string[] callAttributes = null)
        {
            this.UserIds = userIds;
            this.From = from;
            this.To = to;
            this.CallAttributes = callAttributes;
        }

        [JsonPropertyName("userIds")]
        public int[] UserIds { get; set; }

        [JsonPropertyName("from")]
        public string[] From { get; set; }

        [JsonPropertyName("to")]
        public string[] To { get; set; }

        [JsonPropertyName("callAttributes")]
        public string[] CallAttributes { get; set; }
    }
}
