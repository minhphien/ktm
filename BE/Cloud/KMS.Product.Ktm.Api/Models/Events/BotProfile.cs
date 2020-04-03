using Newtonsoft.Json;

namespace KMS.Product.Ktm.Api.Models.Events
{
    public partial class BotProfile
    {
        [JsonProperty("app_id")]
        public string AppId { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("icons")]
        public Icons Icons { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("team_id")]
        public string TeamId { get; set; }

        [JsonProperty("updated")]
        public long Updated { get; set; }
    }
}