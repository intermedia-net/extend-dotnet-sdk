namespace ConnectSDK.Meetings.Models.V1
{
    using System.Text.Json.Serialization;

    public class UserDetailsExtended
    {
        [JsonPropertyName("subscriptionPlanName")]
        public string SubscriptionPlanName { get; set; }

        [JsonPropertyName("subscriptionPrice")]
        public double SubscriptionPrice { get; set; }
    }
}
