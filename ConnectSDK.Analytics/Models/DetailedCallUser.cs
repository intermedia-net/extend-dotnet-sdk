namespace ConnectSDK.Analytics.Models
{
    using System.Text.Json.Serialization;

    public class DetailedCallUser
    {
        [JsonPropertyName("device")]
        public DetailedCallUserDevice Device { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("userUniqueId")]
        public string UserUniqueId { get; set; }
    }
}
