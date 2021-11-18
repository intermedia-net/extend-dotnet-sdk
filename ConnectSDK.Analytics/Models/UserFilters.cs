namespace ConnectSDK.Analytics.Models
{
    using System.Text.Json.Serialization;

    public class UserFilters
    {
        [JsonPropertyName("data")]
        public UserFiltersData Data { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
