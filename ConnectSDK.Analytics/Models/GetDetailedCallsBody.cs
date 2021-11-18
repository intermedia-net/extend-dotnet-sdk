namespace ConnectSDK.Analytics.Models
{
    using System.Text.Json.Serialization;

    public class GetDetailedCallsBody
    {
        public GetDetailedCallsBody(string[] chargeable = null, string[] callAttributes = null)
        {
            this.Chargeable = chargeable;
            this.CallAttributes = callAttributes;
        }

        [JsonPropertyName("chargeable")]
        public string[] Chargeable { get; set; }

        [JsonPropertyName("callAttributes")]
        public string[] CallAttributes { get; set; }
    }
}
