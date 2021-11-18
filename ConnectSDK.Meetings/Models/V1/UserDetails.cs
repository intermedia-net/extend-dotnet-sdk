namespace ConnectSDK.Meetings.Models.V1
{
    using System.Text.Json.Serialization;

    public class UserDetails
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phoneConfUser")]
        public int PhoneConfUser { get; set; }

        [JsonPropertyName("phoneConfCompanyID")]
        public int PhoneConfCompanyId { get; set; }

        [JsonPropertyName("analyticsUserKey")]
        public int AnalyticsUserKey { get; set; }

        [JsonPropertyName("numberFree")]
        public object NumberFree { get; set; }

        [JsonPropertyName("numberToll")]
        public string NumberToll { get; set; }

        [JsonPropertyName("presenterPIN")]
        public string PresenterPin { get; set; }

        [JsonPropertyName("attendeePIN")]
        public string AttendeePin { get; set; }

        [JsonPropertyName("accountID")]
        public object AccountId { get; set; }

        [JsonPropertyName("customField1")]
        public object CustomField1 { get; set; }

        [JsonPropertyName("customField2")]
        public object CustomField2 { get; set; }

        [JsonPropertyName("customField3")]
        public object CustomField3 { get; set; }

        [JsonPropertyName("customField4")]
        public object CustomField4 { get; set; }

        [JsonPropertyName("customField5")]
        public object CustomField5 { get; set; }

        [JsonPropertyName("guid")]
        public string Guid { get; set; }

        [JsonPropertyName("createdAtUnixTime")]
        public double CreatedAtUnixTime { get; set; }

        [JsonPropertyName("shortcutURL")]
        public string ShortcutUrl { get; set; }

        [JsonPropertyName("imageURL")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("sfuEnabled")]
        public bool SfuEnabled { get; set; }

        [JsonPropertyName("extended")]
        public UserDetailsExtended Extended { get; set; }

        [JsonPropertyName("notificationCulture")]
        public string NotificationCulture { get; set; }

        [JsonPropertyName("personalMeetingURL")]
        public string PersonalMeetingUrl { get; set; }
    }
}
