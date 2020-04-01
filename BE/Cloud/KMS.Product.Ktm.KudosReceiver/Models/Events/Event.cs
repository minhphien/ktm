using Newtonsoft.Json;

namespace KMS.Product.Ktm.KudosReceiver.Models.Events
{
    public partial class Event
    {
        [JsonProperty("bot_id")]
        public string BotId { get; set; }

        [JsonProperty("bot_profile")]
        public BotProfile BotProfile { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("event_ts")]
        public string EventTs { get; set; }

        [JsonProperty("team")]
        public string Team { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("ts")]
        public string Ts { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("user")]
        public string User { get; set; }
    }
}
