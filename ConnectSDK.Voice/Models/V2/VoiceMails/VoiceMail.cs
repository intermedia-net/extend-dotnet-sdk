namespace ConnectSDK.Voice.Models.V2.VoiceMails
{
    using System.Text.Json.Serialization;

    public class VoiceMail
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("sender")]
        public Sender Sender { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("whenCreated")]
        public string WhenCreated { get; set; }

        [JsonPropertyName("hasText")]
        public bool HasText { get; set; }
    }
}
