namespace ConnectSDK.Analytics.Models
{
    using System.Text.Json.Serialization;

    public class DetailedCallUserDevice
    {
        [JsonPropertyName("deviceType")]
        public string DeviceType { get; set; }
    }
}
