namespace ConnectSDK.Analytics.Models
{
    using System.Text.Json.Serialization;

    public class UserFiltersData
    {
        [JsonPropertyName("values")]
        public object[] Values { get; set; }
    }
}
